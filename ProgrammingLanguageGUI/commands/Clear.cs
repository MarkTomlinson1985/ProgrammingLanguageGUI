using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    public class Clear : Command {

        public Clear(string command, Drawer drawer) : base(command, drawer) {
            numberOfArguments = 1;
        }

        public override void Execute() {
            drawer.Clear();
        }
    }
}
