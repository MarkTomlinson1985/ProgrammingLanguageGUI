using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.drawing.transform;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.exception;
using ProgrammingLanguageGUI.runner;

namespace ProgrammingLanguageGUI.drawer {
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

            using (Graphics bitmapGraphics = Graphics.FromImage(cursor.Bitmap)) {
                bitmapGraphics.Clear(Color.Transparent);
                bitmapGraphics.DrawEllipse(pen, 0, 0, 5, 5);
            }

            drawingBoxGraphics = drawingBox.CreateGraphics();
            drawingBoxGraphics.DrawImage(cursor.Bitmap, 0, 0);

            Thread multiColourThread = new Thread(() => {
                while (!ThreadManager.TERMINATE_THREADS) {

                    lock (bitmapLock) {

                        if (DrawerProperties.SwitchLayer) {
                            RedrawImageOnLayer(0);
                            drawingBox.BeginInvoke(new Action(() => drawingBox.Image = baseLayers[1]));

                        } else {
                            RedrawImageOnLayer(1);
                            drawingBox.BeginInvoke(new Action(() => drawingBox.Image = baseLayers[0]));
                        }
                        DrawerProperties.SwitchLayer = !DrawerProperties.SwitchLayer;
                    }
                    Thread.Sleep(1000);
                }
            });

            Thread transformThread = new Thread(() => {
                int count = 0;
                while (!ThreadManager.TERMINATE_THREADS) {

                    lock (bitmapLock) {

                        if (count % 2 == 0) {
                            RedrawTransform(0, count);
                            drawingBox.BeginInvoke(new Action(() => drawingBox.Image = baseLayers[1]));
                        } else {
                            RedrawTransform(1, count);
                            drawingBox.BeginInvoke(new Action(() => drawingBox.Image = baseLayers[0]));
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

        private void RedrawImageOnLayer(int layer) {
            Graphics graphics = Graphics.FromImage(baseLayers[layer]);
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(multiColourLayers[layer], 0, 0);
            graphics.DrawImage(cursor.Bitmap, cursor.X - (cursor.Width / 4), cursor.Y - (cursor.Height / 4));
            graphics.DrawImage(drawingLayer, 0, 0);
            graphics.DrawImage(transformLayers[currentTransformLayer], 0, 0);
        }

        private void RedrawTransform(int baseLayer, int transformLayer) {
            Graphics graphics = Graphics.FromImage(baseLayers[baseLayer]);
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(multiColourLayers[DrawerProperties.SwitchLayer ? 0 : 1], 0, 0);
            graphics.DrawImage(cursor.Bitmap, cursor.X - (cursor.Width / 4), cursor.Y - (cursor.Height / 4));
            graphics.DrawImage(drawingLayer, 0, 0);
            graphics.DrawImage(transformLayers[transformLayer], 0, 0);
        }

        public void MoveTo(int toX, int toY) {
            if (DrawerProperties.DrawerEnabled) {
                cursor.X = toX;
                cursor.Y = toY;
            }

            Draw(baseGraphics => { });
        }

        public void DrawLine(int toX, int toY) {
            Draw(baseGraphics => {
                baseGraphics.DrawLine(pen, cursor.X, cursor.Y, toX, toY);});

            if (DrawerProperties.DrawerEnabled) {
                cursor.X = toX;
                cursor.Y = toY;
            }
        }

        public void DrawCircle(int radius) {
            if (DrawerProperties.FillMode) {
                Draw(baseGraphics => baseGraphics.FillEllipse(new SolidBrush(pen.Color), cursor.X - (radius / 2), cursor.Y - (radius / 2), radius, radius));
            } else {
                Draw(baseGraphics => baseGraphics.DrawEllipse(pen, cursor.X - (radius / 2), cursor.Y - (radius / 2), radius, radius));
            }
        }

        public void Clear() {
            drawingBoxGraphics.Clear(backgroundColour);
            drawingLayer = DrawerFactory.CreateBitmap(baseLayers[0].Width, baseLayers[0].Height);
            multiColourLayers = DrawerFactory.CreateDoubleBuffer(baseLayers[0].Width, baseLayers[0].Height);
            baseLayers = DrawerFactory.CreateDoubleBuffer(baseLayers[0].Width, baseLayers[0].Height);
            transformLayers = DrawerFactory.CreateBitmaps(baseLayers[0].Width, baseLayers[0].Height, TRANSFORM_LAYERS);
        }

        public void Reset() {
            Clear();
            DrawerProperties.Reset();
            pen.Color = Color.White;
            MoveTo(0, 0);
        }

        public void DrawRectangle(int width, int height) {
            if (DrawerProperties.FillMode) {
                Draw(baseGraphics => baseGraphics.FillRectangle(new SolidBrush(pen.Color), cursor.X - (width / 2), cursor.Y - (height / 2), width, height));
            } else {
                Draw(baseGraphics => baseGraphics.DrawRectangle(pen, cursor.X - (width / 2), cursor.Y - (height / 2), width, height));
            }
        }

        public void DrawPolygon(Point[] points) {
            Point[] pointsWithOrigin = new Point[] { new(cursor.X, cursor.Y) }.Concat(points).ToArray();
            Draw(baseGraphics => baseGraphics.DrawPolygon(pen, pointsWithOrigin));
        }

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

        public void ChangePenColour(Color colour) {
            DrawerProperties.MultiColours = false;
            pen.Color = colour;
            using (Graphics bitmapGraphics = Graphics.FromImage(cursor.Bitmap)) {
                bitmapGraphics.Clear(Color.Transparent);
                bitmapGraphics.DrawEllipse(pen, 0, 0, 5, 5);
            }
            MoveTo(cursor.X, cursor.Y);
        }

        public void ChangePenMultiColour(Color colourOne, Color colourTwo) {
            multiColours[0] = colourOne;
            multiColours[1] = colourTwo;
            DrawerProperties.MultiColours = true;
        }

        public void SetFillMode(bool enabled) {
            DrawerProperties.FillMode = enabled;
        }

        public void Rotate(Command command, VariableManager variableManager) {
            if (command is IRotatable rotatable) {
                int degreesRotation = 360 / TRANSFORM_LAYERS;

                int originX = cursor.X;
                int originY = cursor.Y;
                Point[] points = rotatable.GetPoints();
                double angleInRadians = degreesRotation * Math.PI / 180;

                for (int i = 0; i < TRANSFORM_LAYERS; i++) {
                    for (int j = 0; j < points.Length; j++) {     
                        double rotatedX = (points[j].X - originX) * Math.Cos(angleInRadians) - (points[j].Y - originY) * Math.Sin(angleInRadians) + originX;
                        double rotatedY = (points[j].X - originX) * Math.Sin(angleInRadians) + (points[j].Y - originY) * Math.Cos(angleInRadians) + originY;
                        points[j] = new Point((int) rotatedX, (int) rotatedY);
                    }

                    rotatable.ExecuteTransform(this, i);
                }
            }
        }

        public void TransformPolygon(Point[] points, int layer) {
            Point[] pointsWithOrigin = new Point[] { new(cursor.X, cursor.Y) }.Concat(points).ToArray();

            lock (bitmapLock) {
                Graphics graphics = Graphics.FromImage(transformLayers[layer]);
                graphics.DrawPolygon(pen, pointsWithOrigin);
            }
        }

        public void DrawTransform(Action<Graphics> drawAction) {

        }

        public void Draw(Action<Graphics> drawAction) {
            if (!DrawerProperties.DrawerEnabled) {
                return;
            }

            lock (bitmapLock) {
                if (DrawerProperties.MultiColours) {
                    Graphics layerOneGraphics = Graphics.FromImage(multiColourLayers[0]);
                    Graphics layerTwoGraphics = Graphics.FromImage(multiColourLayers[1]);

                    pen.Color = multiColours[0];
                    drawAction(layerOneGraphics);
                    pen.Color = multiColours[1];
                    drawAction(layerTwoGraphics);
                    return;
                }

                Graphics drawingLayerGraphics = Graphics.FromImage(drawingLayer);
                drawAction(drawingLayerGraphics);
            }
        }
    }
}
