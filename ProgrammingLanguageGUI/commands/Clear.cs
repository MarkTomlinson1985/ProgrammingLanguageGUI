using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands {
    public class Clear : Command {

        public Clear(string command) : base(command) {
            numberOfArguments = 1;
        }

        public override void Execute(Drawer drawer) {
            drawer.Clear();
        }
    }
}
