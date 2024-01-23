using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUITest.tests.commands
{
    /// <summary>
    /// Tests relating to the CommandFactory factory class.
    /// </summary>
    [TestClass]
    public class CommandFactoryTest
    {

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
        public void CommandFactoryShouldReturnCommandWithValidCommandType(string command, Type expectedType)
        {
            Assert.IsInstanceOfType(CommandFactory.BuildCommand(command), expectedType);
        }

        /// <summary>
        /// Tests that the BuildCommand method returns null with an invalid/not implemented
        /// command type.
        /// </summary>
        [TestMethod]
        public void CommandFactoryShouldReturnNullWithInvalidCommandType()
        {
            Assert.IsNull(CommandFactory.BuildCommand("INVALID"));
        }
    }
}