using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands
{
    public class EndLoop : FunctionCommand {
        public EndLoop(params string[] arguments) : base(arguments) {
            numberOfArguments = 0;
        }

        public override void Execute(VariableManager variableManager) {
            ValidateCommand(variableManager);
        }

        protected override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);
        }
    }
}
