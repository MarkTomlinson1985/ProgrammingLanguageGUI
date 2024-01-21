namespace ProgrammingLanguageGUI.drawer {
    public class Drawer {
        private Graphics drawingBoxGraphics;
        private Cursor cursor;
        private Bitmap baseBitmap;
        private Bitmap cursorBitmap;
        private Color backgroundColour;

        public Drawer(PictureBox drawingBox) {
            cursor = new Cursor();
            cursor.Width = 10;
            cursor.Height = 10;
            cursorBitmap = new Bitmap(cursor.Width, cursor.Height);
            baseBitmap = new Bitmap(drawingBox.Width, drawingBox.Height);
            backgroundColour = drawingBox.BackColor;

            using (Graphics bitmapGraphics = Graphics.FromImage(cursorBitmap)) {
                bitmapGraphics.Clear(Color.Transparent);
                bitmapGraphics.DrawEllipse(Pens.Red, 0, 0, 5, 5);
            }

            drawingBoxGraphics = drawingBox.CreateGraphics();
            drawingBoxGraphics.DrawImage(cursorBitmap, 0, 0);
        }

        public void MoveTo(int toX, int toY) {
            drawingBoxGraphics.Clear(backgroundColour);
            drawingBoxGraphics.DrawImage(baseBitmap, 0, 0);
            drawingBoxGraphics.DrawImage(cursorBitmap, toX - (cursor.Width / 4), toY - (cursor.Height / 4));
            cursor.X = toX;
            cursor.Y = toY;
        }

        public void DrawLine(int toX, int toY) {
            drawingBoxGraphics.Clear(backgroundColour);

            Graphics baseGraphics = Graphics.FromImage(baseBitmap);
            baseGraphics.DrawLine(new Pen(Color.Black), cursor.X, cursor.Y, toX, toY);

            drawingBoxGraphics.DrawImage(baseBitmap, 0, 0);
            drawingBoxGraphics.DrawImage(cursorBitmap, toX - (cursor.Width / 4), toY - (cursor.Height / 4));
            cursor.X = toX;
            cursor.Y = toY;
        }

        public void DrawCircle(int radius) {
            drawingBoxGraphics.Clear(backgroundColour);

            Graphics baseGraphics = Graphics.FromImage(baseBitmap);
            baseGraphics.DrawEllipse(Pens.Black, cursor.X - (radius / 2), cursor.Y - (radius / 2), radius, radius);

            drawingBoxGraphics.DrawImage(baseBitmap, 0, 0);
            drawingBoxGraphics.DrawImage(cursorBitmap, cursor.X - (cursor.Width / 4), cursor.Y - (cursor.Height / 4));
        }

        public void Clear() {
            drawingBoxGraphics.Clear(backgroundColour);
        }
    }
}
