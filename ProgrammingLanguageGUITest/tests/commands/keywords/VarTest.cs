using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands {
    /// <summary>
    /// Tests relating to the Var class.
    /// </summary>
    [TestClass]
    public class VarTest {
        VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests the creation and validation of a valid Var command. Any exception will result
        /// in a failure assertion.
        /// </summary>
        [TestMethod]
        [DataRow("variable = 1")]
        [DataRow("variable = 1 + 2")]
        [DataRow("variable = 10 - 3")]
        [DataRow("variable = 10 * 2")]
        [DataRow("variable = 10 / 5")]
        public void ValidateCommandShouldSucceedWithValidArguments(string arguments) {
            Var command = new Var(arguments.Split(" "));

            try {
                command.Execute(variableManager);
            } catch (Exception) {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Tests that the ValidateCommand method throws specific exceptions and messages when a Var object is
        /// created and validated with invalid arguments.
        /// </summary>
        [TestMethod]
        [DataRow("variable =", "Number of arguments incorrect. Provide at least 3 arguments for variable assignment.")]
        [DataRow("  = 100", "Variable name invalid.")]
        [DataRow("variable - 100", "'=' operator not found. Usage var <variableName> = <value>.")]
        [DataRow("variable = 100 100", "Invalid number of arguments for variable assignment.")]
        [DataRow("variable = 100 * 100 + 10", "Invalid number of arguments for variable assignment.")]
        [DataRow("variable = 100 ^ 5", "Invalid operator in variable assignment.")]
        [DataRow("variable = INVALID", "Provided variable assignment is not a valid number.")]
        public void ValidateCommandShouldThrowArgumentExceptionWithInvalidArguments(
            string arguments, string expectedExceptionMessage) {
            Var command = new Var(arguments.Split(" "));

            Exception ex = Assert.ThrowsException<CommandArgumentException>(() => command.Execute(variableManager));

            Assert.AreEqual(expectedExceptionMessage, ex.Message);
        }

    }
}