using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.keywords.loop {
    public class If(params string[] arguments) : ConditionalCommand(arguments), ISelection, IInlineCommand {
        private Command inlineCommand = Empty;

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);

            if (arguments.Length == 3) {
                return;
            }

            string additionalCommand = string.Join(" ", arguments.Skip(3).ToArray());

            if (string.IsNullOrEmpty(additionalCommand)) {
                throw new CommandArgumentException("Invalid command defined in selection statement.");
            }

            Command? command = CommandFactory.BuildCommand(additionalCommand);
            if (command == null) {
                throw new CommandArgumentException("Invalid command defined in selection statement.");
            }

            if (command is ConditionalCommand) {
                throw new CommandArgumentException("Unsupported command defined in selection statement.");
            }

            inlineCommand = command;   
        }

        public Command Retrieve() {
            return inlineCommand;
        }

        public bool HasInlineCommand() {
            return !(inlineCommand is Empty);
        }

        public override string ToString() {
            return $"IF {string.Join(" ", arguments)}";
        }
    }
}
