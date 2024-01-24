using ProgrammingLanguageGUI.commands.keywords;

namespace ProgrammingLanguageGUITest.tests.commands {
    /// <summary>
    /// Tests relating to the Var class.
    /// </summary>
    [TestClass]
    public class VariableManagerTest {
        private VariableManager variableManager;

        [TestInitialize]
        public void Initialize() {
            variableManager = new VariableManager();
        }

        /// <summary>
        /// Tests that the VariableManager class correctly stores, checks and retrieves variables.
        /// </summary>
        [TestMethod]
        public void VariableManagerShouldStoreAndRetrieveVariables() {
            string variableName = "variable";
            string variableValue = "100";
            
            variableManager.AddVariable(variableName, variableValue);

            Assert.IsTrue(variableManager.HasVariable(variableName));
            Assert.AreEqual(variableValue, variableManager.GetVariable(variableName));

            Assert.IsFalse(variableManager.HasVariable("Invalid"));
        }
    }
}