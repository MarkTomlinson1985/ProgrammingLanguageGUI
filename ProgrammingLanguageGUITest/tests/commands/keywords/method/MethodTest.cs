using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.commands.keywords.method;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands.keywords.loop {
    /// <summary>
    /// Tests relating to the Method class.
    /// </summary>
    [TestClass]
    public class MethodTest {
        Drawer drawer = new Drawer(new System.Windows.Forms.PictureBox());
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid Method command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        public void WhileCommandShouldEvaluateCorrectlyWithValidArguments() {
            Method command = new Method("myMethod");

            try {
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        public void WhileCommandShouldExecuteWithParameters() {
            Method command = new Method("myMethod(name,value)");

            try {
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }

            Assert.IsTrue(command.Arguments.Contains("name"));
            Assert.IsTrue(command.Arguments.Contains("value"));
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Method object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        [DataRow(" ", "Invalid method name.")]
        [DataRow("100", "Invalid method name '100': numeric value.")]
        [DataRow("myMethod(100,name)", "Invalid parameter identifier: numeric value.")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string arguments, string expectedExceptionMessage) {
            Method command = new Method(arguments.Split(" "));

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Method object is
        /// created and validated with no arguments.
        /// </summary>
        public void ValidateCommandShouldThrowArgumentExceptionWithNoArugments() {
            Method command = new Method();

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));

            Assert.AreEqual("Number of arguments incorrect. Provide at least 1 arguments for method declaration.", ex.Message);
        }


    }
}