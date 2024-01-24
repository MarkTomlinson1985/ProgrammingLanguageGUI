using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands {
    public class Empty : Command {
        public override void Execute(Drawer drawer, VariableManager variableManager) {
            throw new CommandException("Execute called on Empty command.");
        }
    }
}
