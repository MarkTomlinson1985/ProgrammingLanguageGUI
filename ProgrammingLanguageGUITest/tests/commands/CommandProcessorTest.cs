using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands {
    /// <summary>
    /// Tests relating to the CommandProcessor class.
    /// </summary>
    [TestClass]
    public class CommandProcessorTest {
        private CommandProcessor processor;

        [TestInitialize]
        public void Initialize() {
            processor = new CommandProcessor();
        }

        /// <summary>
        /// Tests that the ParseCommand method returns a Command with the correct
        /// derived type with a valid command argument.
        /// </summary>
        [TestMethod]
        public void ParseCommandShouldReturnCommandWithValidCommand() {
            Assert.IsInstanceOfType(processor.ParseCommand("CIRCLE"), typeof(Circle));
        }

        /// <summary>
        /// Tests that the ParseCommand method throws a CommandNotFoundException when provided with
        /// an unknown/not implemented command argument.
        /// </summary>
        [TestMethod]
        public void ParseCommandShouldThrowExceptionForInvalidCommand() {
            Exception ex = Assert.ThrowsException<CommandNotFoundException>(
                () => processor.ParseCommand("INVALID 100 100"));

            Assert.AreEqual("Command INVALID not recognised.", ex.Message);
        }

        /// <summary>
        /// Tests that the ParseProgram method returns a ProgramResults object with the correct
        /// Commands and line numbers associated with those commands.
        /// Test requires that a custom Equals method be defined for tested Commands such that
        /// two different Command objects of the same type with the same values would be considered equal.
        /// </summary>
        [TestMethod]
        public void ParseProgramShouldReturnProgramResultsWithCommands() {
            string input = "MOVE 100 100\nCIRCLE 50";
            ProgramResults results = processor.ParseProgram(input);
            foreach (Command command in results.GetCommands().Keys) { command.ValidateCommand(); }

            Dictionary<Command, int> expectedCommands = new Dictionary<Command, int>() {
                { new Move("100", "100"), 1 },
                { new Circle("50"), 2 }
            };

            foreach (Command command in expectedCommands.Keys) { command.ValidateCommand(); }

            Assert.IsTrue(expectedCommands.Keys.All(expectedKey => results.GetCommands().Keys.Any(resultKey => expectedKey.Equals(resultKey))));
        }

        /// <summary>
        /// Tests that the ParseProgram method returns a ProgramResults object with the correct
        /// exceptions from invalid commands.
        /// Only commands that are not implemented will result in exceptions at this stage of the
        /// program flow. Known commands with invalid parameters are validated later in the flow.
        /// </summary>
        [TestMethod]
        public void ParseProgramShouldReturnExceptionsWithCommandsNotFound() {
            string input = "MOV 100 100\nCIRCLE 50 50\nDRAWTO 100 -100\nINVALID 50";
            ProgramResults results = processor.ParseProgram(input);

            List<CommandException> expectedExceptions = new List<CommandException>() {
                new CommandNotFoundException("Line 1: Command MOV not recognised."),
                new CommandNotFoundException("Line 4: Command INVALID not recognised.")};

            Assert.IsTrue(expectedExceptions.All(expectedException => results.GetExceptions().Any(resultException => expectedException.Message.Equals(resultException.Message))));
        }

        /// <summary>
        /// Tests that the ParseProgram method returns a ProgramResults object with both valid Commands
        /// and exceptions. Combination of the above tests.
        /// </summary>
        [TestMethod]
        public void ParseProgramShouldReturnProgramResultsWithCommandsAndExceptions() {
            string input = "MOVE 100 100\nCIRCLE 50\nDRAWT 100 100";
            ProgramResults results = processor.ParseProgram(input);
            foreach (Command command in results.GetCommands().Keys) { command.ValidateCommand(); }

            Dictionary<Command, int> expectedCommands = new Dictionary<Command, int>() {
                { new Move("100", "100"), 1 },
                { new Circle("50"), 2 }
            };

            foreach (Command command in expectedCommands.Keys) { command.ValidateCommand(); }

            Assert.IsTrue(expectedCommands.Keys.All(expectedKey => results.GetCommands().Keys.Any(resultKey => expectedKey.Equals(resultKey))));

            List<CommandException> expectedExceptions = new List<CommandException>() {
                new CommandNotFoundException("Line 3: Command DRAWT not recognised.")};

            Assert.IsTrue(expectedExceptions.All(expectedException => results.GetExceptions().Any(resultException => expectedException.Message.Equals(resultException.Message))));
        }
    }
}