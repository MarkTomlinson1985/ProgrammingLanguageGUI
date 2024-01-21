using ProgrammingLanguageGUI.commands;

namespace ProgrammingLanguageGUI
{
    public partial class Application : Form {
        CommandProcessor commandProcessor;

        public Application() {
            InitializeComponent();
            commandProcessor = new CommandProcessor();
        }

    }
}
