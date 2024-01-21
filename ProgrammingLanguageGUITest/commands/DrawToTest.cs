using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest {
    [TestClass]
    public class DrawToTest {
        private Drawer drawer = new Drawer(new PictureBox());

        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            DrawTo command = new DrawTo("DRAWTO 100 100", drawer);

            try {
                command.ValidateCommand();
            } catch (Exception) {
                Assert.Fail();
            }
        }

        [TestMethod]
        [DataRow("100", "INVALID", "Provided arguments are not valid numbers.")]
        [DataRow("-100", "100", "Provided coordinate arguments must not be negative.")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string argumentOne,
            string argumentTwo,
            string expectedExceptionMessage) {
            DrawTo command = new DrawTo($"DRAWTO {argumentOne} {argumentTwo}", drawer);
            
            Exception ex = Assert.ThrowsException<ArgumentException>(() => command.ValidateCommand());

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}