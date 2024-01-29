using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.drawing.transform;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.keywords.loop {
    /// Derived command class. Contains methods to validate and execute the command, and custom
    /// toString impelmentation for reverse engineering commands back into text.
    /// </summary>
    public class Rotate : TransformCommand, IInlineCommand {
        private Command inlineCommand = Empty;
        public int originX;
        public int originY;

        public Rotate(params string[] arguments) : base(arguments) { }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            inlineCommand.ValidateCommand(variableManager);
            drawer.Rotate(inlineCommand, originX, originY);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            try {
                originX = int.Parse(GetVariableOrValue(arguments[0], variableManager));
                originY = int.Parse(GetVariableOrValue(arguments[1], variableManager));

            } catch (FormatException) {
                throw new CommandArgumentException("Provided rotation origin co-ordinates are not a valid number.");
            }

            string additionalCommand = string.Join(" ", arguments.Skip(2).ToArray());

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
            return $"ROTATE {string.Join(" ", arguments)}";
        }
    }
}
