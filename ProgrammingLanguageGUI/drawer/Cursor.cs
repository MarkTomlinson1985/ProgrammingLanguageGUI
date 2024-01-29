namespace ProgrammingLanguageGUI.drawer {
    /// <summary>
    /// Derived PictureBox class. Stored Bitmap and positional data for
    /// on-screen cursor.
    /// </summary>
    public class Cursor : PictureBox {
        private Bitmap bitmap;
        public Bitmap Bitmap { 
            get { return bitmap; }
            set { bitmap = value; }}
        private int xPosition = 0;
        private int yPosition = 0;
        public int X {
            get { return xPosition; }
            set { xPosition = value; }
        }
        public int Y {
            get { return yPosition; }
            set { yPosition = value; }
        }

    }
}
