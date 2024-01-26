using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.commands.keywords.loop;
using ProgrammingLanguageGUI.commands.keywords.method;

namespace ProgrammingLanguageGUITest.tests.commands
{
    /// <summary>
    /// Tests relating to the CommandFactory factory class.
    /// </summary>
    [TestClass]
    public class CommandFactoryTest {
        private VariableManager variableManager = new VariableManager();

        /// <summary>
        /// Tests that the BuildCommand method returns a Command with the correct
        /// derived type with a valid command type.
        /// </summary>
        [TestMethod]
        [DataRow("MOVE", typeof(Move))]
        [DataRow("DRAWTO", typeof(DrawTo))]
        [DataRow("CIRCLE", typeof(Circle))]
        [DataRow("CLEAR", typeof(Clear))]
        [DataRow("RESET", typeof(Reset))]
        [DataRow("RECTANGLE", typeof(Rectangle))]
        [DataRow("TRIANGLE", typeof(Triangle))]
        [DataRow("PEN", typeof(Pen))]
        [DataRow("FILL", typeof(Fill))]
        [DataRow("VAR", typeof(Var))]
        [DataRow("WHILE", typeof(While))]
        [DataRow("ENDLOOP", typeof(EndLoop))]
        [DataRow("IF", typeof(If))]
        [DataRow("ENDIF", typeof(EndIf))]
        [DataRow("METHOD", typeof(Method))]
        [DataRow("ENDMETHOD", typeof(EndMethod))]
        [DataRow("myMethod()", typeof(CallMethod))]
        [DataRow("POLYGON", typeof(Polygon))]
        public void CommandFactoryShouldReturnCommandWithValidCommandType(string command, Type expectedType) {
            Assert.IsInstanceOfType(CommandFactory.BuildCommand(command), expectedType);
        }

        /// <summary>
        /// Tests that the BuildCommand method returns null with an invalid/not implemented
        /// command type.
        /// </summary>
        [TestMethod]
        public void CommandFactoryShouldReturnNullWithInvalidCommandType() {
            Assert.IsNull(CommandFactory.BuildCommand("INVALID 100"));
        }
    }
}