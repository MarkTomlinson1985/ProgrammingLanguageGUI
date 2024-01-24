using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.method;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;
using System.Data.Common;

namespace ProgrammingLanguageGUITest.tests.commands.keywords.loop {
    /// <summary>
    /// Tests relating to the CallMethod class.
    /// </summary>
    [TestClass]
    public class CallMethodTest {
        Drawer drawer = new Drawer(new System.Windows.Forms.PictureBox());
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid CallMethod command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldSucceedWithValidArguments() {
            Method method = new Method("myMethod");
            CallMethod command = (CallMethod) CommandFactory.BuildCommand("myMethod()");

            try {
                method.Execute(drawer, variableManager);
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        [TestMethod]
        [DataRow("myMethod(50)", "a")]
        [DataRow("test(50,50)", "a", "b")]
        public void ValidateCommandShouldSucceedWithValidArguments(string arguments, params string[] variableNames) {
            Method method = new Method(arguments.Split("(")[0] + "(" + string.Join(",", variableNames) + ")");
            CallMethod command = (CallMethod)CommandFactory.BuildCommand(arguments);
            
            try {
                method.Execute(drawer, variableManager);
                command.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws CommandNotFoundException when a CallMethod object is
        /// created and validated with undeclared method name.
        /// </summary>
        [TestMethod]
        public void ValidateCommandShouldThrowCommandNotFoundExceptionForUndeclaredMethod() {
            CallMethod command = (CallMethod)CommandFactory.BuildCommand("myMethod()");

            Exception ex = Assert.ThrowsException<CommandNotFoundException>(() => command.Execute(drawer, variableManager));
            
            Assert.AreEqual("Method not declared: 'myMethod'.", ex.Message);
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a CallMethod object is
        /// created and validated against a declared Method with no parameters.
        /// </summary>
        [DataRow("Invalid method parameters provided for method test; expected: 1 - provided 0.", "a")]
        [DataRow("Invalid method parameters provided for method test; expected: 2 - provided 0.", "a", "b")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArgument(string expectedExceptionMessage, params string[] variableNames) {
            Method method = new Method("myMethod" + "(" + string.Join(",", variableNames) + ")");
            CallMethod command = (CallMethod)CommandFactory.BuildCommand("myMethod()");

            try {
                method.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a CallMethod object is
        /// created and validated against a declared Method with parameters.
        /// </summary>
        [TestMethod]
        [DataRow("test(50)", "Invalid method parameters provided for method test; expected: 2 - provided 1.", "a", "b")]
        [DataRow("test(50,50)", "Invalid method parameters provided for method test; expected: 1 - provided 2.", "a")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(string arguments, string expectedExceptionMessage, params string[] variableNames) {
            Method method = new Method(arguments.Split("(")[0] + "(" + string.Join(",", variableNames) + ")");
            CallMethod command = (CallMethod)CommandFactory.BuildCommand(arguments);

            try {
                method.Execute(drawer, variableManager);
            } catch (Exception) {
                Assert.Fail();
            }

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(drawer, variableManager));

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }
    }
}