using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.runner;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest {
    /// <summary>
    /// Tests relating to the Runner class.
    /// Runner class methods return a string output for use in terminal output.
    /// </summary>
    [TestClass]
    public class RunnerTest {
        private Runner runner;
        private CommandProcessor processor = new CommandProcessor();
        private Drawer drawer = new Drawer(new PictureBox());

        [TestInitialize]
        public void Initialize() {
            runner = new Runner(processor, drawer);
        }

        /// <summary>
        /// Tests that the RunCommand method returns a success message with a valid command argument.
        /// </summary>
        [TestMethod]
        public void RunCommandShouldReturnSuccessOutput() {
            Assert.AreEqual("Command run successfully", runner.RunCommand("CIRCLE 50"));
        }

        /// <summary>
        /// Tests that the RunCommand method returns expected exception messages with invalid command argument.
        /// </summary>
        [TestMethod]
        [DataRow("INVALID 100", "Command INVALID not recognised.")]
        [DataRow("CIRCLE 50 50", "Number of arguments incorrect. Expected: 1 - Actual: 2")]
        public void RunCommandShouldReturnExceptionOutputForInvalidCommand(string input, string expectedMessage) {
            Assert.AreEqual(expectedMessage, runner.RunCommand(input));
        }

        /// <summary>
        /// Tests that the RunProgram method returns a success message with a valid program argument.
        /// </summary>
        [TestMethod]
        public void RunProgramShouldReturnSuccessOutput() {
            string program = "CIRCLE 50\nMOVE 100 100\nDRAWTO 200 200";
            Assert.AreEqual("Program executed successfully.", runner.RunProgram(program));
        }

        /// <summary>
        /// Tests that the RunProgram method returns multiple expected exception messages with invalid program argument.
        /// </summary>
        [TestMethod]
        public void RunProgramShouldReturnExceptionOutputWithInvalidCommands() {
            string program = "CIRCLE 50\nINVALID 100\nDRAWTO -100 100";
            string expectedMessage = "Line 2: Command INVALID not recognised.\nLine 3: Provided coordinate arguments must not be negative.\n";
            Assert.AreEqual(expectedMessage, runner.RunProgram(program));
        }

    }
}