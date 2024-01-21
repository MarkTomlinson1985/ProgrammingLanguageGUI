using ProgrammingLanguageGUI.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageGUI.commands {
    public class ProgramResults {
        private Dictionary<Command, int> commands = new Dictionary<Command, int>();
        private Dictionary<CommandException, int> exceptions = new Dictionary<CommandException, int>();

        public ProgramResults(Dictionary<Command, int> commands, Dictionary<CommandException, int> exceptions) { 
            this.commands = commands;
            this.exceptions = exceptions;
        }

        public Dictionary<Command, int> GetCommands() {  return commands; }
        public Dictionary<CommandException, int> GetExceptions() {  return exceptions; }

    }
}
