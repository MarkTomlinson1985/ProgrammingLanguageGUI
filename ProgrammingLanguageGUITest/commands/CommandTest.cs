using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.commands {
    [TestClass]
    public class CommandTest {

        [TestMethod]
        public void ValidateCommandShouldSucceedWithCorrectNumberOfArguments() {
            Command command = new BaseCommand("");

            try {
                command.ValidateCommand();
            }
            catch (Exception) {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithIncorrectNumberOfArguments() {
            Command command = new BaseCommand("INVALID COMMAND");

            Assert.ThrowsException<CommandArgumentException>(() => command.ValidateCommand());
        }

        // Derived class that uses default implementation of ValidateCommand()
        private class BaseCommand : Command {
            public BaseCommand(string command) : base(command) { }

            public override void Execute(Drawer drawer) {
                throw new NotImplementedException();
            }
        }

    }
}