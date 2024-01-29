namespace ProgrammingLanguageGUI.drawer {
    /// <summary>
    /// Factory class for providing objects used in the Drawer class.
    /// </summary>
    public class DrawerFactory {

        /// <summary>
        /// Creates a Cursor object with a default width and height.
        /// </summary>
        /// <returns></returns>
        public static Cursor CreateCursor() {
            Cursor cursor = new Cursor();
            cursor.Width = 10;
            cursor.Height = 10;
            cursor.Bitmap = CreateBitmap(cursor.Width, cursor.Height);
            return cursor;
        }

        /// <summary>
        /// Creates a bitmap of a given width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap CreateBitmap(int width, int height) {
            return new Bitmap(width, height);
        }

        /// <summary>
        /// Creates two bitmaps of a given width and heightfor use in double-buffering.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap[] CreateDoubleBuffer(int width, int height) {
            return CreateBitmaps(width, height, 2);
        }

        /// <summary>
        /// Creates the given number of bitmaps of a given width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static Bitmap[] CreateBitmaps(int width, int height, int amount) {
            Bitmap[] bitmaps = new Bitmap[amount];
            for (int i = 0;  i < amount; i++) {
                bitmaps[i] = CreateBitmap(width, height);
            }

            return bitmaps;
        }

        /// <summary>
        /// Creates a DrawerProperties object.
        /// </summary>
        /// <returns></returns>
        public static DrawerProperties CreateDrawerProperties() {
            return new DrawerProperties();
        }

        /// <summary>
        /// Creates a pen with a default colour.
        /// </summary>
        /// <returns></returns>
        public static Pen CreatePen() {
            return new Pen(Color.White);
        }

        /// <summary>
        /// Creates an array of Colour objects of the given size.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Color[] CreateColours(int length) {
            return new Color[length];
        }
    }
}
