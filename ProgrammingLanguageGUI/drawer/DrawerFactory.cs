namespace ProgrammingLanguageGUI.drawer {
    public class DrawerFactory {

        public static Cursor CreateCursor() {
            Cursor cursor = new Cursor();
            cursor.Width = 10;
            cursor.Height = 10;
            cursor.Bitmap = CreateBitmap(cursor.Width, cursor.Height);
            return cursor;
        }

        public static Bitmap CreateBitmap(int width, int height) {
            return new Bitmap(width, height);
        }

        public static Bitmap[] CreateDoubleBuffer(int width, int height) {
            return CreateBitmaps(width, height, 2);
        }

        public static Bitmap[] CreateBitmaps(int width, int height, int amount) {
            Bitmap[] bitmaps = new Bitmap[amount];
            for (int i = 0;  i < amount; i++) {
                bitmaps[i] = CreateBitmap(width, height);
            }

            return bitmaps;
        }

        public static DrawerProperties CreateDrawerProperties() {
            return new DrawerProperties();
        }

        public static Pen CreatePen() {
            return new Pen(Color.White);
        }

        public static Color[] CreateColours(int length) {
            return new Color[length];
        }
    }
}
