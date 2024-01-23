using ProgrammingLanguageGUI.commands;

namespace ProgrammingLanguageGUI.drawer {
    public class Drawer {
        private readonly Graphics drawingBoxGraphics;
        private readonly Cursor cursor;
        private Bitmap baseBitmap;
        private System.Drawing.Pen pen;
        private Color backgroundColour;

        public Drawer(PictureBox drawingBox) {
            cursor = DrawerFactory.CreateCursor();
            baseBitmap = DrawerFactory.CreateBitmap(drawingBox.Width, drawingBox.Height);
            backgroundColour = drawingBox.BackColor;
            pen = new System.Drawing.Pen(Color.White);

            using (Graphics bitmapGraphics = Graphics.FromImage(cursor.Bitmap)) {
                bitmapGraphics.Clear(Color.Transparent);
                bitmapGraphics.DrawEllipse(pen, 0, 0, 5, 5);
            }

            drawingBoxGraphics = drawingBox.CreateGraphics();
            drawingBoxGraphics.DrawImage(cursor.Bitmap, 0, 0);
        }

        public void MoveTo(int toX, int toY) {
            cursor.X = toX;
            cursor.Y = toY;
            Draw(baseGraphics => { });
        }

        public void DrawLine(int toX, int toY) {
            Draw(baseGraphics => {
                baseGraphics.DrawLine(pen, cursor.X, cursor.Y, toX, toY);
                MoveTo(toX, toY);
            });
        }

        public void DrawCircle(int radius) {
            Draw(baseGraphics => baseGraphics.DrawEllipse(pen, cursor.X - (radius / 2), cursor.Y - (radius / 2), radius, radius));
        }

        public void Clear() {
            drawingBoxGraphics.Clear(backgroundColour);
            baseBitmap = DrawerFactory.CreateBitmap(baseBitmap.Width, baseBitmap.Height);
            ChangePenColour(Color.White);
        }

        public void Reset() {
            MoveTo(0, 0);
        }

        public void DrawRectangle(int width, int height) {
            Draw(baseGraphics => baseGraphics.DrawRectangle(pen, cursor.X - (width / 2), cursor.Y - (height / 2), width, height));
        }

        public void DrawTriangle(int width, int height) {
            int originalX = cursor.X;
            int originalY = cursor.Y;
            Draw(baseGraphics => {
                MoveTo(cursor.X, cursor.Y - (height / 2));
                DrawLine(cursor.X + (width / 2), cursor.Y + height);
                DrawLine(cursor.X - width, cursor.Y);
                DrawLine(cursor.X + (width / 2), cursor.Y - height);
            }, false);
            MoveTo(originalX, originalY);
        }

        public void ChangePenColour(Color colour) {
            pen.Color = colour;
            using (Graphics bitmapGraphics = Graphics.FromImage(cursor.Bitmap))
            {
                bitmapGraphics.Clear(Color.Transparent);
                bitmapGraphics.DrawEllipse(pen, 0, 0, 5, 5);
            }
            MoveTo(cursor.X, cursor.Y);
        }

        public void Draw(Action<Graphics> drawAction) {
            Draw(drawAction, true);
        }

        public void Draw(Action<Graphics> drawAction, bool paintCursor) {
            drawingBoxGraphics.Clear(backgroundColour);

            Graphics baseGraphics = Graphics.FromImage(baseBitmap);
            drawAction(baseGraphics);

            drawingBoxGraphics.DrawImage(baseBitmap, 0, 0);

            if (paintCursor) {
                drawingBoxGraphics.DrawImage(cursor.Bitmap, cursor.X - (cursor.Width / 4), cursor.Y - (cursor.Height / 4));
            }
        }
    }
}
