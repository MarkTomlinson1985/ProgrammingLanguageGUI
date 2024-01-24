using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.runner;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest.tests.runner {
    /// <summary>
    /// Tests relating to the Runner class.
    /// Runner class methods return a string output for use in terminal output.
    /// </summary>
    [TestClass]
    public class RunnerTest {
        private Runner runner;
        private static VariableManager variableManager;
        private CommandProcessor processor = new CommandProcessor();
        private Drawer drawer = new Drawer(new PictureBox());

        [TestInitialize]
        public void Initialize() {
            variableManager = new VariableManager();
            runner = new Runner(processor, drawer, variableManager);
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

        // Add runner tests for methods.

        /// <summary>
        /// Tests that the RunProgram method returns a success message with a valid program argument.
        /// </summary>
        [TestMethod]
        [DataRow("CIRCLE 50\nMOVE 100 100\nDRAWTO 200 200")]
        [DataRow("CIRCLE 50\nMOVE 100 100\nVAR a = 1\nWHILE a < 10\nCIRCLE a\nVAR a = a + 1\nENDLOOP")]
        [DataRow("CIRCLE 50\nMOVE 100 100\nIF 10 == 10 CIRCLE 50\nIF 10 != 10\nCIRCLE 50\nENDIF")]
        [DataRow("CIRCLE 50\nMOVE 100 100\nMETHOD myMethod\nMOVE 100 100\nENDMETHOD\nmyMethod()")]
        [DataRow("CIRCLE 50\nMOVE 100 100\nMETHOD myMethod(xPos,yPos)\nMOVE xPos yPos\nENDMETHOD\nmyMethod(200,200)")]
        public void RunProgramShouldReturnSuccessOutput(string program) {
            Assert.AreEqual("Program executed successfully.", runner.RunProgram(program));
        }


        /// <summary>
        /// Tests that the RunProgram method returns multiple expected exception messages with invalid program argument.
        /// </summary>
        [TestMethod]
        public void RunProgramShouldReturnExceptionOutputWithInvalidCommands() {
            string program = "CIRCLE 50\nINVALID 100\nDRAWTO -100 100\nWHILE 100 == 100\nCIRCLE 100\nIF 10 == 10\nCIRCLE 50";
            string expectedMessage = "Line 2: Command INVALID not recognised.\nLine 3: Provided coordinate arguments must not be negative.\nLine 4: Loop command has no defined end.\nLine 6: If block has no defined end.\n";
            Assert.AreEqual(expectedMessage, runner.RunProgram(program));
        }

    }
}