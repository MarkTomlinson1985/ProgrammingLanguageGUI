using ProgrammingLanguageGUI.drawer;
using System.Drawing;

namespace ProgrammingLanguageGUITest.tests.file {
    /// <summary>
    /// Tests relating to the DrawerFactory class.
    /// </summary>
    [TestClass]
    public class DrawerFactoryTest {
        /// <summary>
        /// Tests that the CreateCursor method returns a Cursor with the correct
        /// width, height and bitmap values.
        /// </summary>
        [TestMethod]
        public void CreateCursorShouldReturnCursor() {
            Cursor cursor = DrawerFactory.CreateCursor();
            Assert.AreEqual(10, cursor.Width);
            Assert.AreEqual(10, cursor.Height);
            Assert.IsNotNull(cursor.Bitmap);
        }

        /// <summary>
        /// Tests that the CreateBitmap method returns with an invalid/not implemented
        /// command type.
        /// </summary>
        [TestMethod]
        public void CreateBitmapShouldReturnBitmap() {
            Bitmap bitmap = DrawerFactory.CreateBitmap(20, 30);
            Assert.IsNotNull(bitmap);
            Assert.AreEqual(20, bitmap.Width);
            Assert.AreEqual(30, bitmap.Height);
        }

        /// <summary>
        /// Tests that the CreateBitmap method returns with an invalid/not implemented
        /// command type.
        /// </summary>
        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(10)]
        public void CreateBitmapsShouldReturnBitmaps(int amount) {
            Bitmap[] bitmaps = DrawerFactory.CreateBitmaps(20, 30, amount);
            Assert.IsNotNull(bitmaps);
            Assert.AreEqual(amount, bitmaps.Length);
        }

    }
}