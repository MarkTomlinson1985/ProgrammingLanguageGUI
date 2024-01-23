namespace ProgrammingLanguageGUI.drawer {
    public class Drawer {
        private readonly Graphics drawingBoxGraphics;
        private readonly Cursor cursor;
        private readonly Bitmap baseBitmap;
        private Color backgroundColour;

        public Drawer(PictureBox drawingBox) {
            cursor = DrawerFactory.CreateCursor();
            baseBitmap = DrawerFactory.CreateBitmap(drawingBox.Width, drawingBox.Height);
            backgroundColour = drawingBox.BackColor;

            using (Graphics bitmapGraphics = Graphics.FromImage(cursor.Bitmap)) {
                bitmapGraphics.Clear(Color.Transparent);
                bitmapGraphics.DrawEllipse(Pens.Red, 0, 0, 5, 5);
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
            Draw(baseGraphics => baseGraphics.DrawLine(Pens.White, cursor.X, cursor.Y, toX, toY));
            cursor.X = toX;
            cursor.Y = toY;
        }

        public void DrawCircle(int radius) {
            Draw(baseGraphics => baseGraphics.DrawEllipse(Pens.White, cursor.X - (radius / 2), cursor.Y - (radius / 2), radius, radius));
        }

        public void Clear() {
            drawingBoxGraphics.Clear(backgroundColour);
        }

        public void Reset() {
            MoveTo(0, 0);
        }

        public void DrawRectangle(int width, int height) {
            Draw(baseGraphics => baseGraphics.DrawRectangle(Pens.White, cursor.X - (width / 2), cursor.Y - (height / 2), width, height));
        }

        public void Draw(Action<Graphics> drawAction) {
            drawingBoxGraphics.Clear(backgroundColour);

            Graphics baseGraphics = Graphics.FromImage(baseBitmap);
            drawAction(baseGraphics);

            drawingBoxGraphics.DrawImage(baseBitmap, 0, 0);
            drawingBoxGraphics.DrawImage(cursor.Bitmap, cursor.X - (cursor.Width / 4), cursor.Y - (cursor.Height / 4));
        }
    }
}
