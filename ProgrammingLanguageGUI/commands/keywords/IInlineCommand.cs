namespace ProgrammingLanguageGUI.commands.keywords.loop {
    /// <summary>
    /// Interface for commands that implement in-line commands.
    /// e.g Inline IF statements (IF a < b CIRCLE 50)
    /// </summary>
    internal interface IInlineCommand {
        Command Retrieve();
        bool HasInlineCommand();
    }
}
