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
    }
}
