using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest.commands
{
    [TestClass]
    public class CommandTest {
        private Drawer drawer = new Drawer(new PictureBox());

        [TestMethod]
        public void ValidateCommandShouldSucceedWithCorrectNumberOfArguments()
        {
            Command command = new BaseCommand("", drawer);

            try
            {
                command.ValidateCommand();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithIncorrectNumberOfArguments()
        {
            Command command = new BaseCommand("INVALID COMMAND", drawer);

            Assert.ThrowsException<ArgumentException>(() => command.ValidateCommand());
        }

        // Derived class that uses default implementation of ValidateCommand()
        private class BaseCommand : Command
        {
            public BaseCommand(string command, Drawer drawer) : base(command, drawer) { }

            public override void Execute()
            {
                throw new NotImplementedException();
            }
        }

    }
}