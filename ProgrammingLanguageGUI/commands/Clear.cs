using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    public class Clear : DrawCommand {

        public Clear(params string[] arguments) : base(arguments) {
            numberOfArguments = 0;
        }

        public override void Execute(Drawer drawer) {
            drawer.Clear();
        }
    }
}
