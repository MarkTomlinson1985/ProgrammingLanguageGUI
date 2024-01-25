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
            return [CreateBitmap(width, height), CreateBitmap(width, height)];
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
