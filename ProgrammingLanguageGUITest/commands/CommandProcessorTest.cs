using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest {
    [TestClass]
    public class CommandProcessorTest {
        private CommandProcessor processor;
        private static Drawer drawer = new Drawer(new PictureBox());

        [TestInitialize]
        public void Initialize() {
            processor = new CommandProcessor(drawer);
        }

        [TestMethod]
        [DataRow("MOVE 100 100", typeof(Move))]
        [DataRow("DRAWTO 100 100", typeof(DrawTo))]
        [DataRow("CIRCLE 100", typeof(Circle))]
        [DataRow("CLEAR", typeof(Clear))]
        public void ParseCommandShouldReturnCommandWithValidCommand(string command, Type expectedType) {
            Assert.IsInstanceOfType(processor.ParseCommand(command), expectedType);
        }

        [TestMethod]
        public void ParseCommandShouldThrowExceptionForInvalidCommand() { 
            Exception ex = Assert.ThrowsException<CommandNotFoundException>(
                () => processor.ParseCommand("INVALID 100 100"));

            Assert.AreEqual("Command INVALID not recognised.", ex.Message);
        }

        [TestMethod]
        public void ParseProgramShouldReturnProgramResultsWithCommands() {
            string input = "MOVE 100 100\nCIRCLE 50";
            ProgramResults results = processor.ParseProgram(input);
            foreach (Command command in results.GetCommands().Keys) { command.ValidateCommand(); }

            Dictionary<Command, int> expectedCommands = new Dictionary<Command, int>() {
                { new Move("MOVE 100 100", drawer), 1 },
                { new Circle("CIRCLE 50", drawer), 2 }
            };

            foreach (Command command in expectedCommands.Keys) { command.ValidateCommand(); }

            Assert.IsTrue(expectedCommands.Keys.All(expectedKey => results.GetCommands().Keys.Any(resultKey => expectedKey.Equals(resultKey))));
        }

        [TestMethod]
        public void ParseProgramShouldReturnExceptionsWithCommandsNotFound() {
            string input = "MOV 100 100\nCIRCLE 50 50\nDRAWTO 100 -100\nINVALID 50";
            ProgramResults results = processor.ParseProgram(input);

            List<CommandException> expectedExceptions = new List<CommandException>() {
                new CommandNotFoundException("Line 1: Command MOV not recognised."),
                new CommandNotFoundException("Line 4: Command INVALID not recognised.")};

            Assert.IsTrue(expectedExceptions.All(expectedException => results.GetExceptions().Any(resultException => expectedException.Message.Equals(resultException.Message))));
        }

        [TestMethod]
        public void ParseProgramShouldReturnProgramResultsWithCommandsAndExceptions() {
            string input = "MOVE 100 100\nCIRCLE 50\nDRAWT 100 100";
            ProgramResults results = processor.ParseProgram(input);
            foreach (Command command in results.GetCommands().Keys) { command.ValidateCommand(); }

            Dictionary<Command, int> expectedCommands = new Dictionary<Command, int>() {
                { new Move("MOVE 100 100", drawer), 1 },
                { new Circle("CIRCLE 50", drawer), 2 }
            };

            foreach (Command command in expectedCommands.Keys) { command.ValidateCommand(); }

            Assert.IsTrue(expectedCommands.Keys.All(expectedKey => results.GetCommands().Keys.Any(resultKey => expectedKey.Equals(resultKey))));

            List<CommandException> expectedExceptions = new List<CommandException>() {
                new CommandNotFoundException("Line 3: Command DRAWT not recognised.")};

            Assert.IsTrue(expectedExceptions.All(expectedException => results.GetExceptions().Any(resultException => expectedException.Message.Equals(resultException.Message))));
        }
    }
}