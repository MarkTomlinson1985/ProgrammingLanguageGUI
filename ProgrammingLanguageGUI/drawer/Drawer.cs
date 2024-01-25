using System.Diagnostics;
using System.Windows.Forms;

namespace ProgrammingLanguageGUI.drawer {
    public class Drawer {
        private readonly Graphics drawingBoxGraphics;
        private readonly Cursor cursor;
        private Bitmap[] baseBitmap;
        private Pen pen;
        private Color backgroundColour;
        private bool fillModeEnabled;
        private bool disableDrawer;
        public bool DisableDrawer { set { disableDrawer = value; } }
        private Color multiColourOne;
        private Color multiColourTwo;
        private bool multiColourPen;
        private Bitmap drawingLayer;
        private Bitmap multiLayerOne;
        private Bitmap multiLayerTwo;
        private bool showLayerOne;

        public Drawer(PictureBox drawingBox) {
            cursor = DrawerFactory.CreateCursor();
            baseBitmap = DrawerFactory.CreateDoubleBuffer(drawingBox.Width, drawingBox.Height);
            backgroundColour = drawingBox.BackColor;
            pen = new Pen(Color.White);
            disableDrawer = false;
            drawingLayer = DrawerFactory.CreateBitmap(drawingBox.Width, drawingBox.Height);
            multiLayerOne = DrawerFactory.CreateBitmap(drawingBox.Width, drawingBox.Height);
            multiLayerTwo = DrawerFactory.CreateBitmap(drawingBox.Width, drawingBox.Height);

            using (Graphics bitmapGraphics = Graphics.FromImage(cursor.Bitmap)) {
                bitmapGraphics.Clear(Color.Transparent);
                bitmapGraphics.DrawEllipse(pen, 0, 0, 5, 5);
            }

            drawingBoxGraphics = drawingBox.CreateGraphics();
            drawingBoxGraphics.DrawImage(cursor.Bitmap, 0, 0);

            Thread multiColourThread = new Thread(() => {
                while (true) {
                    drawingBoxGraphics.Clear(backgroundColour);
                    
                    if (showLayerOne) {
                        Graphics baseGraphics = Graphics.FromImage(baseBitmap[0]);
                        baseGraphics.DrawImage(multiLayerOne, 0, 0);
                        baseGraphics.DrawImage(cursor.Bitmap, cursor.X - (cursor.Width / 4), cursor.Y - (cursor.Height / 4));
                        baseGraphics.DrawImage(drawingLayer, 0, 0);
                        drawingBox.Image = baseBitmap[1];

                    } else {
                        Graphics baseGraphics = Graphics.FromImage(baseBitmap[1]);
                        baseGraphics.DrawImage(multiLayerTwo, 0, 0);
                        baseGraphics.DrawImage(cursor.Bitmap, cursor.X - (cursor.Width / 4), cursor.Y - (cursor.Height / 4));
                        baseGraphics.DrawImage(drawingLayer, 0, 0);
                        drawingBox.Image = baseBitmap[0];
                    }

                    showLayerOne = !showLayerOne;
                    Thread.Sleep(1000);
                }
            });

            multiColourThread.Start();
        }

        public void MoveTo(int toX, int toY) {
            cursor.X = toX;
            cursor.Y = toY;
            Draw(baseGraphics => { });
        }

        public void DrawLine(int toX, int toY) {
            Draw(baseGraphics => {
                baseGraphics.DrawLine(pen, cursor.X, cursor.Y, toX, toY);});
            cursor.X = toX;
            cursor.Y = toY;
        }

        public void DrawCircle(int radius) {
            if (fillModeEnabled) {
                Draw(baseGraphics => baseGraphics.FillEllipse(new SolidBrush(pen.Color), cursor.X - (radius / 2), cursor.Y - (radius / 2), radius, radius));
            } else {
                Draw(baseGraphics => baseGraphics.DrawEllipse(pen, cursor.X - (radius / 2), cursor.Y - (radius / 2), radius, radius));
            }
        }

        public void Clear() {
            drawingBoxGraphics.Clear(backgroundColour);
            drawingLayer = DrawerFactory.CreateBitmap(baseBitmap[0].Width, baseBitmap[0].Height);
            multiLayerOne = DrawerFactory.CreateBitmap(baseBitmap[0].Width, baseBitmap[0].Height);
            multiLayerTwo = DrawerFactory.CreateBitmap(baseBitmap[0].Width, baseBitmap[0].Height);
            baseBitmap = DrawerFactory.CreateDoubleBuffer(baseBitmap[0].Width, baseBitmap[0].Height);
            pen.Color = Color.White;
        }

        public void Reset() {
            Clear();
            fillModeEnabled = false;
            multiColourPen = false;
            pen.Color = Color.White;
            MoveTo(0, 0);
        }

        public void DrawRectangle(int width, int height) {
            if (fillModeEnabled) {
                Draw(baseGraphics => baseGraphics.FillRectangle(new SolidBrush(pen.Color), cursor.X - (width / 2), cursor.Y - (height / 2), width, height));
            } else {
                Draw(baseGraphics => baseGraphics.DrawRectangle(pen, cursor.X - (width / 2), cursor.Y - (height / 2), width, height));
            }
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
            multiColourPen = false;
            pen.Color = colour;
            using (Graphics bitmapGraphics = Graphics.FromImage(cursor.Bitmap)) {
                bitmapGraphics.Clear(Color.Transparent);
                bitmapGraphics.DrawEllipse(pen, 0, 0, 5, 5);
            }
            MoveTo(cursor.X, cursor.Y);
        }

        public void ChangePenMultiColour(Color colourOne, Color colourTwo) {
            multiColourOne = colourOne;
            multiColourTwo = colourTwo;
            multiColourPen = true;
        }

        public void SetFillMode(bool enabled) {
            fillModeEnabled = enabled;
        }

        public void Draw(Action<Graphics> drawAction) {
            if (disableDrawer) {
                return;
            }

            if (!multiColourPen) {
                Graphics drawingLayerGraphics = Graphics.FromImage(drawingLayer);
                drawAction(drawingLayerGraphics);
                return;
            }

            if (multiColourPen) {
                Graphics layerOneGraphics = Graphics.FromImage(multiLayerOne);
                Graphics layerTwoGraphics = Graphics.FromImage(multiLayerTwo);

                pen.Color = multiColourOne;
                drawAction(layerOneGraphics);
                pen.Color = multiColourTwo;
                drawAction(layerTwoGraphics);
            } 

        }
    }
}
