namespace ProgrammingLanguageGUI.exception {
    /// <summary>
    /// Derived command exception class relating to commands with invalid arguments.
    /// </summary>
    public class CommandArgumentException : CommandException {
        public CommandArgumentException(string message) : base(message) { }
    }
}
