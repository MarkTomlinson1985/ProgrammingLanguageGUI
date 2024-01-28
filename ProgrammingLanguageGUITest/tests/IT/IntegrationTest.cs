using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.runner;
using System.Windows.Forms;

namespace ProgrammingLanguageGUITest.tests.runner {

    /// <summary>
    /// Integration tests.
    /// These tests utilize full pre-written programs to test the system end-to-end.
    /// </summary>
    [TestClass]
    public class IntegrationTest {
        private static string DEFAULT_PATH = "C:\\Users\\m_tom\\source\\repos\\ProgrammingLanguageGUI\\ProgrammingLanguageGUI\\resources\\examples\\";
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
        /// Tests that the application returns a success message with a valid command argument.
        /// </summary>
        [TestMethod]
        [DataRow("clock")]
        [DataRow("if")]
        [DataRow("looptest")]
        [DataRow("methodtest")]
        [DataRow("moon")]
        [DataRow("multicolour")]
        [DataRow("nestedloop")]
        [DataRow("polygon")]
        [DataRow("variables")]
        public void RunCommandShouldReturnSuccessOutput(string filename) {
            string program = File.ReadAllText(DEFAULT_PATH + filename + ".txt");
            Assert.AreEqual("Program executed successfully.", runner.RunProgram(program));
        }
    }

        
}