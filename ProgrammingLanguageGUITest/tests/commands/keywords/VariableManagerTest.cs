using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.method;
using ProgrammingLanguageGUI.drawer;
using System.Windows.Forms;

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

            variableManager.RemoveVariable(variableName);
            Assert.IsFalse(variableManager.HasVariable(variableName));
        }

        /// <summary>
        /// Tests that the VariableManager class correctly stores, checks and retrieves methods.
        /// </summary>
        [TestMethod]
        public void VariableManagerShouldStoreAndRetrieveMethods() {
            Method method = new Method("myMethod(width,height)");
            // Method added as part of Method execute method.
            method.Execute(new Drawer(new PictureBox()), variableManager);

            Assert.IsTrue(variableManager.HasMethod("myMethod"));
            Assert.AreEqual(method, variableManager.GetMethod("myMethod"));

            Assert.IsFalse(variableManager.HasMethod("Invalid"));
        }
    }
}