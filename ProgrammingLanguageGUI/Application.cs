using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.runner;

namespace ProgrammingLanguageGUI
{
    public partial class Application : Form {
        Drawer drawer;
        CommandProcessor commandProcessor;
        Runner runner;


        public Application() {
            InitializeComponent();
            drawer = new Drawer(drawingBox);
            commandProcessor = new CommandProcessor(drawer);
            runner = new Runner(commandProcessor);
        }
    }
}
