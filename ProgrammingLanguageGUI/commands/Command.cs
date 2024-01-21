namespace ProgrammingLanguageGUI.commands {
    abstract class Command : ICommand {
        protected string command;
        protected int numberOfArguments;

        public Command(string command) {
            this.command = command;
            numberOfArguments = 0;
        }

        public abstract void Execute();
        
        public virtual void ValidateCommand() {
            string[] argumentArray = new string[command.Split(" ").Length];

            if (argumentArray.Length != numberOfArguments) {
                throw new ArgumentException(
                    "Number of arguments incorrect. Expected: " + numberOfArguments + " - Actual: " + argumentArray.Length);
            }
        }
    }
}
