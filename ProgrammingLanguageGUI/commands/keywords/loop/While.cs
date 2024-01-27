using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands.keywords.loop {
    public class While(params string[] arguments) : ConditionalCommand(arguments) {
        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
        }

        public override string ToString() {
            return $"WHILE {string.Join(" ", arguments)}";
        }
    }
}
