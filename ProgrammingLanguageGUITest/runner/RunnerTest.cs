using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using ProgrammingLanguageGUI.runner;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest {
    [TestClass]
    public class RunnerTest {
        private Runner runner;
        private CommandProcessor processor;
        private Drawer drawer = new Drawer(new PictureBox());

        [TestInitialize]
        public void Initialize() {
            processor = new CommandProcessor(drawer);
            runner = new Runner(processor);
        }

        [TestMethod]
        public void RunCommandShouldReturnSuccessOutput() {
            Assert.AreEqual("Command run successfully", runner.RunCommand("CIRCLE 50"));
        }

        [TestMethod]
        [DataRow("INVALID 100", "Command INVALID not recognised.")]
        [DataRow("CIRCLE 50 50", "Number of arguments incorrect. Expected: 2 - Actual: 3")]
        public void RunCommandShouldReturnExceptionOutputForInvalidCommand(string input, string expectedMessage) {
            Assert.AreEqual(expectedMessage, runner.RunCommand(input));
        }

        [TestMethod]
        public void RunProgramShouldReturnSuccessOutput() {
            string program = "CIRCLE 50\nMOVE 100 100\nDRAWTO 200 200";
            Assert.AreEqual("Program executed successfully.", runner.RunProgram(program));
        }

        [TestMethod]
        public void RunProgramShouldReturnExceptionOutputWithInvalidCommands() {
            string program = "CIRCLE 50\nINVALID 100\nDRAWTO -100 100";
            string expectedMessage = "Line 2: Command INVALID not recognised.\nLine 3: Provided coordinate arguments must not be negative.\n";
            Assert.AreEqual(expectedMessage, runner.RunProgram(program));
        }

    }
}