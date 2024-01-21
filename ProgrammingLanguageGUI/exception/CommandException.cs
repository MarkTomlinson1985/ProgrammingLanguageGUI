using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageGUI.exception {
    public class CommandException : SystemException {
        public CommandException(string message) : base (message) {}
    }
}
