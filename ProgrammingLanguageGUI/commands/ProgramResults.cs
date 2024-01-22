using ProgrammingLanguageGUI.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageGUI.commands {
    public class ProgramResults {
        private Dictionary<Command, int> commands = new Dictionary<Command, int>();
        private List<CommandException> exceptions = new List<CommandException>();

        public ProgramResults(Dictionary<Command, int> commands, List<CommandException> exceptions) { 
            this.commands = commands;
            this.exceptions = exceptions;
        }

        public Dictionary<Command, int> GetCommands() {  return commands; }
        public List<CommandException> GetExceptions() {  return exceptions; }

    }
}
