using ProgrammingLanguageGUI.commands;

namespace ProgrammingLanguageGUITest {
    [TestClass]
    public class CommandProcessorTest {
        private CommandProcessor processor;

        [TestInitialize]
        public void Initialize() {
            processor = new CommandProcessor();
        }

        [TestMethod]
        [DataRow("MOVE 100 100", typeof(Move))]
        public void ParseCommandShouldReturnCommandWithValidCommand(string command, Type expectedType) {
            Assert.IsInstanceOfType(processor.ParseCommand(command), expectedType);
        }

        [TestMethod]
        public void ParseCommandShouldThrowExceptionForInvalidCommand() { 
            Exception ex = Assert.ThrowsException<NotImplementedException>(
                () => processor.ParseCommand("INVALID 100 100"));

            Assert.AreEqual("Command INVALID not recognised.", ex.Message);
        }

    }
}