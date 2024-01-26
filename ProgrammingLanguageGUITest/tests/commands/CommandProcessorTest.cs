using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands
{
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
        /// Tests that the ParseCommand method returns a Command with the correct
        /// derived type with a valid command argument with a comment.
        /// </summary>
        [TestMethod]
        public void ParseCommandShouldReturnCommandWithValidCommandAndComment() {
            Assert.IsInstanceOfType(processor.ParseCommand("CIRCLE // This is a comment"), typeof(Circle));
        }

        /// <summary>
        /// Tests that the ParseCommand method returns a Command with the correct
        /// derived type with a valid command argument with a comment.
        /// </summary>
        [TestMethod]
        public void ParseCommandShouldReturnEmptyCommandWithComment() {
            Assert.IsInstanceOfType(processor.ParseCommand("// This is a comment"), typeof(Empty));
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

            Assert.IsTrue(results.GetCommands().Keys.First().GetType().Equals(typeof(Move)));
            Assert.IsTrue(results.GetCommands().Keys.ElementAt(1).GetType().Equals(typeof(Circle)));

            Assert.IsTrue(results.GetCommands().Values.First() == 1);
            Assert.IsTrue(results.GetCommands().Values.ElementAt(1) == 2);
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
            string input = "MOVE 100 100\nDRAWT 100 100\nCIRCLE 50";
            ProgramResults results = processor.ParseProgram(input);

            Assert.IsTrue(results.GetCommands().Keys.First().GetType().Equals(typeof(Move)));
            Assert.IsTrue(results.GetCommands().Keys.ElementAt(1).GetType().Equals(typeof(Circle)));

            Assert.IsTrue(results.GetCommands().Values.First() == 1);
            Assert.IsTrue(results.GetCommands().Values.ElementAt(1) == 3);

            Assert.IsTrue(results.GetExceptions().Count() == 1);
            Assert.IsTrue(results.GetExceptions().First().Message.Equals("Line 2: Command DRAWT not recognised."));
        }
    }
}