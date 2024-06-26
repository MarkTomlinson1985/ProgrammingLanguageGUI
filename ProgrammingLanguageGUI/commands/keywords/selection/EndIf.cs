﻿using ProgrammingLanguageGUI.commands.keywords;
using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands
{
    public class EndIf : Command {
        public EndIf(params string[] arguments) : base(arguments) {
            numberOfArguments = 0;
        }

        public override void Execute(Drawer drawer, VariableManager variableManager) {
            ValidateCommand(variableManager);
        }

        public override void ValidateCommand(VariableManager variableManager) {
            base.ValidateCommand(variableManager);
        }

        public override string ToString() {
            return "ENDIF";
        }
    }
}
