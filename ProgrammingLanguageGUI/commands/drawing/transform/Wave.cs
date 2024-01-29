using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.drawing.transform;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.keywords.loop {
    /// Derived command class. Contains methods to validate and execute the command, and custom
    /// toString impelmentation for reverse engineering commands back into text.
    /// </summary>
    public class Wave : TransformCommand, IInlineCommand {
        private Command inlineCommand = Empty;

        public Wave(params string[] arguments) : base(arguments) { }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            inlineCommand.ValidateCommand(variableManager);
            drawer.Wave(inlineCommand);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            string additionalCommand = string.Join(" ", arguments);

            if (string.IsNullOrEmpty(additionalCommand)) {
                throw new CommandArgumentException("Invalid command defined in transform statement.");
            }

            Command? command = CommandFactory.BuildCommand(additionalCommand);
            if (command == null) {
                throw new CommandArgumentException("Invalid command defined in transform statement.");
            }

            if (command is not IRotatable) {
                throw new CommandArgumentException("Unsupported command defined in transform statement.");
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
            return $"WAVE {string.Join(" ", arguments)}";
        }
    }
}
