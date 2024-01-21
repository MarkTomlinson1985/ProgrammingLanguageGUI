using ProgrammingLanguageGUI.commands;
using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI
{
    public partial class Application : Form {
        Drawer drawer;
        CommandProcessor commandProcessor;


        public Application() {
            InitializeComponent();
            drawer = new Drawer(drawingBox);
            commandProcessor = new CommandProcessor(drawer);
        }


    }
}
