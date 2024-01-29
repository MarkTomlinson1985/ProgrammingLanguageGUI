using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    /// <summary>
    /// Represents an 'empty' Command.
    /// </summary>
    public class Empty : Command {

        /// <summary>
        /// Empty command should not be executed in normal program flow. Throws
        /// an exception if this is the case.
        /// </summary>
        /// <param name="drawer"></param>
        /// <param name="variableManager"></param>
        /// <exception cref="CommandException"></exception>
        public override void Execute(Drawer drawer, VariableManager variableManager) {
            throw new CommandException("Execute called on Empty command.");
        }
    }
}
