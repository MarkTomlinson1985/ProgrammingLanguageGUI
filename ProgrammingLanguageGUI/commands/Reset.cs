using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    public class Reset : DrawCommand {

        public Reset(params string[] arguments) : base(arguments) {
            numberOfArguments = 0;
        }

        public override void Execute(Drawer drawer) {
            drawer.Reset();
        }
    }
}
