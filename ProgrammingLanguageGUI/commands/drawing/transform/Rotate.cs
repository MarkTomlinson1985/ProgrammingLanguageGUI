﻿using ProgrammingLanguageGUI.commands.drawing;
using ProgrammingLanguageGUI.commands.drawing.transform;
using ProgrammingLanguageGUI.drawer;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.keywords.loop {
    public class Rotate : TransformCommand, IInlineCommand {
        private Command inlineCommand = Empty;

        public Rotate(params string[] arguments) : base(arguments) { }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
            inlineCommand.ValidateCommand(variableManager);
            drawer.Rotate(inlineCommand, variableManager);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            string additionalCommand = string.Join(" ", arguments);

            if (string.IsNullOrEmpty(additionalCommand)) {
                throw new CommandArgumentException("Invalid command defined in transform statement.");
            }

            Command? command = CommandFactory.BuildCommand(additionalCommand);
            if (command == null) {
                throw new CommandArgumentException("Invalid command defined in transform statement.");
            }

            if (command is not IRotatable) {
                throw new CommandArgumentException("Unsupported command defined in transform statement.");
            }

            inlineCommand = command;   
        }

        public Command Retrieve() {
            return inlineCommand;
        }

        public bool HasInlineCommand() {
            return !(inlineCommand is Empty);
        }
    }
}