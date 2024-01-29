using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.drawing.transform;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.runner;

namespace ProgrammingLanguageGUI.drawer {
    /// <summary>
    /// Responsible for mangaging drawing operations and bitmaps to render
    /// commands onto the screen.
    /// </summary>
    public class Drawer {
        private static int TRANSFORM_LAYERS = 100;

        private readonly object bitmapLock = new();
        private readonly Graphics drawingBoxGraphics;
        private readonly Cursor cursor;
        private readonly Pen pen;
        private readonly Color[] multiColours;
        public DrawerProperties DrawerProperties { get; }
        private Color backgroundColour;
        private Bitmap[] baseLayers;
        private Bitmap[] multiColourLayers;
        private Bitmap[] transformLayers;
        private Bitmap drawingLayer;
        private int currentTransformLayer;

        public Drawer(PictureBox drawingBox) {
            DrawerProperties = DrawerFactory.CreateDrawerProperties();
            cursor = DrawerFactory.CreateCursor();
            baseLayers = DrawerFactory.CreateDoubleBuffer(drawingBox.Width, drawingBox.Height);
            pen = DrawerFactory.CreatePen();
            drawingLayer = DrawerFactory.CreateBitmap(drawingBox.Width, drawingBox.Height);
            multiColourLayers = DrawerFactory.CreateDoubleBuffer(drawingBox.Width, drawingBox.Height);
            multiColours = DrawerFactory.CreateColours(multiColourLayers.Length);
            transformLayers = DrawerFactory.CreateBitmaps(drawingBox.Width, drawingBox.Height, TRANSFORM_LAYERS);
            backgroundColour = drawingBox.BackColor;
            
            using Graphics bitmapGraphics = Graphics.FromImage(cursor.Bitmap);
            bitmapGraphics.Clear(Color.Transparent);
            bitmapGraphics.DrawEllipse(pen, 0, 0, 5, 5);

            drawingBoxGraphics = drawingBox.CreateGraphics();
            drawingBoxGraphics.DrawImage(cursor.Bitmap, 0, 0);

            // Thread for displaying 'flashing' objects. Draws to one of two base bitmaps,
            // first one then the other. Uses double buffering to prevent screen-tearing.
            Thread multiColourThread = new Thread(() => {
                while (!ThreadManager.TERMINATE_THREADS) {
                    if (DrawerProperties.DrawerEnabled) {
                        if (DrawerProperties.SwitchLayer) {
                            RedrawImageOnLayer(0);
                            drawingBox.BeginInvoke(new Action(() => {
                                lock (bitmapLock) {
                                    drawingBox.Image = baseLayers[1];
                                }}));

                        } else {
                            RedrawImageOnLayer(1);
                            drawingBox.BeginInvoke(new Action(() => {
                                lock (bitmapLock) {
                                    drawingBox.Image = baseLayers[0];
                                }}));
                        }
                        DrawerProperties.SwitchLayer = !DrawerProperties.SwitchLayer;
                    }         
                    
                    Thread.Sleep(1000);
                }
            });

            // More frequently firing thread for displaying trasform operations. Uses a number of
            // bitmaps, drawing each one to one of two base bitmaps in turn, creating a visual
            // 'movement' effect. Uses double buffering to prevent screen-tearing.
            Thread transformThread = new Thread(() => {
                int count = 0;
                while (!ThreadManager.TERMINATE_THREADS) {
                    if (DrawerProperties.DrawerEnabled && DrawerProperties.TransformEnabled) {
                        if (count % 2 == 0) {
                            RedrawTransform(0, count);
                            drawingBox.BeginInvoke(new Action(() => { 
                                lock (bitmapLock) { 
                                    drawingBox.Image = baseLayers[1]; 
                                }}));
                        } else {
                            RedrawTransform(1, count);
                            drawingBox.BeginInvoke(new Action(() => {
                                lock (bitmapLock) {
                                    drawingBox.Image = baseLayers[0];
                                }
                            }));
                        }
                        currentTransformLayer = count;

                        if (count == transformLayers.Length - 1) {
                            count = 0;
                        } else {
                            count++;
                        }
                    }
                        
                    Thread.Sleep(50);
                    
                }
            });

            multiColourThread.Start();
            transformThread.Start();
        }

        /// <summary>
        /// Clears the provided base layer bitmap and redraws the specific lower-level
        /// bitmaps onto it.
        /// </summary>
        /// <param name="layer">The index of the layer to redraw.</param>
        private void RedrawImageOnLayer(int layer) {
            lock (bitmapLock) {
                using Graphics graphics = Graphics.FromImage(baseLayers[layer]);
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(cursor.Bitmap, cursor.X - (cursor.Width / 4), cursor.Y - (cursor.Height / 4));
                graphics.DrawImage(drawingLayer, 0, 0);
                graphics.DrawImage(multiColourLayers[layer], 0, 0);
                graphics.DrawImage(transformLayers[currentTransformLayer], 0, 0);
            }
        }

        /// <summary>
        /// Clears the provided base layer bitmap and redraws the specific lower-level
        /// bitmaps onto it. Redraws a specific 'transform layer' bitmap.
        /// </summary>
        /// <param name="baseLayer"></param>
        /// <param name="transformLayer"></param>
        private void RedrawTransform(int baseLayer, int transformLayer) {
            lock (bitmapLock) {
                using Graphics graphics = Graphics.FromImage(baseLayers[baseLayer]);
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(cursor.Bitmap, cursor.X - (cursor.Width / 4), cursor.Y - (cursor.Height / 4));
                graphics.DrawImage(drawingLayer, 0, 0);
                graphics.DrawImage(multiColourLayers[DrawerProperties.SwitchLayer ? 0 : 1], 0, 0);
                graphics.DrawImage(transformLayers[transformLayer], 0, 0);
            }
        }

        /// <summary>
        /// Moves the on-screen cursor to the provided x and y co-ordinates.
        /// Subsequent commands will use this position as a point of origin if
        /// appropriate.
        /// </summary>
        /// <param name="toX"></param>
        /// <param name="toY"></param>
        public void MoveTo(int toX, int toY) {
            if (DrawerProperties.DrawerEnabled) {
                cursor.X = toX;
                cursor.Y = toY;
            }

            Draw(baseGraphics => { });
        }

        /// <summary>
        /// Draws a line from the current cursor position to the provided x and y co-ordinates.
        /// </summary>
        /// <param name="toX"></param>
        /// <param name="toY"></param>
        public void DrawLine(int toX, int toY) {
            Draw(baseGraphics => {
                baseGraphics.DrawLine(pen, cursor.X, cursor.Y, toX, toY);
            });

            if (DrawerProperties.DrawerEnabled) {
                cursor.X = toX;
                cursor.Y = toY;
            }
        }

        /// <summary>
        /// Draws a circle with the given radius.
        /// </summary>
        /// <param name="radius"></param>
        public void DrawCircle(int radius) {
            if (DrawerProperties.FillMode) {
                Draw(baseGraphics => baseGraphics.FillEllipse(new SolidBrush(pen.Color), cursor.X - (radius / 2), cursor.Y - (radius / 2), radius, radius));
            } else {
                Draw(baseGraphics => baseGraphics.DrawEllipse(pen, cursor.X - (radius / 2), cursor.Y - (radius / 2), radius, radius));
            }
        }

        /// <summary>
        /// Clears the drawing area.
        /// </summary>
        public void Clear() {
            drawingBoxGraphics.Clear(backgroundColour);
            drawingLayer = DrawerFactory.CreateBitmap(baseLayers[0].Width, baseLayers[0].Height);
            multiColourLayers = DrawerFactory.CreateDoubleBuffer(baseLayers[0].Width, baseLayers[0].Height);
            baseLayers = DrawerFactory.CreateDoubleBuffer(baseLayers[0].Width, baseLayers[0].Height);
            transformLayers = DrawerFactory.CreateBitmaps(baseLayers[0].Width, baseLayers[0].Height, TRANSFORM_LAYERS);
        }

        /// <summary>
        /// Clears the drawing area and resets the drawer properties to their default
        /// values.
        /// </summary>
        public void Reset() {
            Clear();
            DrawerProperties.Reset();
            pen.Color = Color.White;
            MoveTo(0, 0);
        }

        /// <summary>
        /// Draws a rectangle of the provided width and height. Draws around the current 
        /// cursor position.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DrawRectangle(int width, int height) {
            if (DrawerProperties.FillMode) {
                Draw(baseGraphics => baseGraphics.FillRectangle(new SolidBrush(pen.Color), cursor.X - (width / 2), cursor.Y - (height / 2), width, height));
            } else {
                Draw(baseGraphics => baseGraphics.DrawRectangle(pen, cursor.X - (width / 2), cursor.Y - (height / 2), width, height));
            }
        }

        /// <summary>
        /// Draws a polygon with the provided points as reference.
        /// </summary>
        /// <param name="points"></param>
        public void DrawPolygon(Point[] points) {
            Point[] pointsWithOrigin = new Point[] { new(cursor.X, cursor.Y) }.Concat(points).ToArray();
            Draw(baseGraphics => baseGraphics.DrawPolygon(pen, pointsWithOrigin));
        }

        /// <summary>
        /// Draws a triangle of the given width and height around the current cursor position.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DrawTriangle(int width, int height) {
            int originalX = cursor.X;
            int originalY = cursor.Y;
            Draw(baseGraphics => {
                MoveTo(cursor.X, cursor.Y - (height / 2));
                DrawLine(cursor.X + (width / 2), cursor.Y + height);
                DrawLine(cursor.X - width, cursor.Y);
                DrawLine(cursor.X + (width / 2), cursor.Y - height);
                MoveTo(originalX, originalY);
            });
        }

        /// <summary>
        /// Change the current pen colour for use in subsequent drawing operations.
        /// </summary>
        /// <param name="colour"></param>
        public void ChangePenColour(Color colour) {
            DrawerProperties.MultiColours = false;
            pen.Color = colour;
            lock (bitmapLock) {
                using Graphics bitmapGraphics = Graphics.FromImage(cursor.Bitmap);
                bitmapGraphics.Clear(Color.Transparent);
                bitmapGraphics.DrawEllipse(pen, 0, 0, 5, 5);                
            }
            MoveTo(cursor.X, cursor.Y);
        }

        /// <summary>
        /// Changes the current pen to use multiple colours in subsequent drawing operations.
        /// </summary>
        /// <param name="colourOne"></param>
        /// <param name="colourTwo"></param>
        public void ChangePenMultiColour(Color colourOne, Color colourTwo) {
            multiColours[0] = colourOne;
            multiColours[1] = colourTwo;
            DrawerProperties.MultiColours = true;
        }

        /// <summary>
        /// Sets fill mode to on/off.
        /// </summary>
        /// <param name="enabled"></param>
        public void SetFillMode(bool enabled) {
            DrawerProperties.FillMode = enabled;
        }

        /// <summary>
        /// Calculates the rotation of a rotatable object based on number of 
        /// transform layers. Executes a draw operation on each transform layer with
        /// the calculated points.
        /// </summary>
        /// <param name="command"></param>
        public void Rotate(Command command) {
            if (command is IRotatable rotatable) {
                double degreesRotation = 360.0 / TRANSFORM_LAYERS;

                int originX = cursor.X;
                int originY = cursor.Y;
                Point[] points = rotatable.GetPoints();
                double angleInRadians = degreesRotation * Math.PI / 180;

                for (int i = 0; i < TRANSFORM_LAYERS; i++) {
                    for (int j = 0; j < points.Length; j++) {
                        double rotatedX = (points[j].X - originX) * Math.Cos(angleInRadians) - (points[j].Y - originY) * Math.Sin(angleInRadians) + originX;
                        double rotatedY = (points[j].X - originX) * Math.Sin(angleInRadians) + (points[j].Y - originY) * Math.Cos(angleInRadians) + originY;
                        points[j] = new Point((int)rotatedX, (int)rotatedY);
                    }

                    rotatable.ExecuteTransform(this, i);
                }
            }
        }

        /// <summary>
        /// Draws a polygon to the given transform layer.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="layer"></param>
        public void TransformPolygon(Point[] points, int layer) {
            Point[] pointsWithOrigin = new Point[] { new(cursor.X, cursor.Y) }.Concat(points).ToArray();
            DrawTransform(graphics => graphics.DrawPolygon(pen, pointsWithOrigin), layer);
        }

        /// <summary>
        /// Performs a given draw action to a specifc transform layer.
        /// Enables transform thread operations if not already enabled.
        /// </summary>
        /// <param name="drawAction"></param>
        /// <param name="layer"></param>
        public void DrawTransform(Action<Graphics> drawAction, int layer) {
            DrawerProperties.TransformEnabled = true;
            lock (bitmapLock) {
                using Graphics graphics = Graphics.FromImage(transformLayers[layer]);
                drawAction(graphics);
            }
        }

        /// <summary>
        /// Executes a given draw action to a graphics layer. Uses multi-colour
        /// layers if multi-coloured mode is enabled.
        /// </summary>
        /// <param name="drawAction"></param>
        public void Draw(Action<Graphics> drawAction) {
            if (!DrawerProperties.DrawerEnabled) {
                return;
            }

            lock (bitmapLock) {
                if (DrawerProperties.MultiColours) {
                    using Graphics layerOneGraphics = Graphics.FromImage(multiColourLayers[0]);
                    using Graphics layerTwoGraphics = Graphics.FromImage(multiColourLayers[1]);

                    pen.Color = multiColours[0];
                    drawAction(layerOneGraphics);
                    pen.Color = multiColours[1];
                    drawAction(layerTwoGraphics);
                    return;
                }

                using Graphics drawingLayerGraphics = Graphics.FromImage(drawingLayer);
                drawAction(drawingLayerGraphics);
            }
        }
    }
}
