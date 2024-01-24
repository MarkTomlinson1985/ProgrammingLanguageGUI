namespace ProgrammingLanguageGUI.commands.keywords.loop {
    internal interface IInlineCommand {
        Command Retrieve();
        bool HasInlineCommand();
    }
}
