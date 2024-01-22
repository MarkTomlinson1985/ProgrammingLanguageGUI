using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.commands {
    /// <summary>
    /// Tests relating to the Command abstract class.
    /// As this class is abstract but contains logic that should be tested in the 
    /// ValidateCommand method, a BaseCommand class is used that can be instantiated
    /// and uses this default implementation of the ValidateCommand method.
    /// </summary>
    [TestClass]
    public class CommandTest {

        /// <summary>
        /// Tests the creation and validation of a Command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithCorrectNumberOfArguments() {
            Command command = new BaseCommand();

            try {
                command.ValidateCommand();
            }
            catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Command object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldThrowArgumentExceptionWithIncorrectNumberOfArguments() {
            Command command = new BaseCommand("INVALID");

            Assert.ThrowsException<CommandArgumentException>(() => command.ValidateCommand());
        }

        /// <summary>
        /// Derived class that uses default implementation of ValidateCommand()
        /// </summary>
        private class BaseCommand : Command {
            public BaseCommand(params string[] arguments) : base(arguments) { }

            public override void Execute(Drawer drawer) {
                throw new NotImplementedException();
            }
        }

    }
}