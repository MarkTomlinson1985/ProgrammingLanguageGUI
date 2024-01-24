using ProgrammingLanguageGUI.commands.keywords.method;
using ProgrammingLanguageGUI.exception;

namespace ProgrammingLanguageGUI.commands.keywords
{
    public class VariableManager {
        Dictionary<string, string> variables = new Dictionary<string, string>();
        List<Method> methods = new List<Method>();

        public void AddVariable(string name, string value) {
            variables[name] = value;
        }

        public string GetVariable(string name) {
            return variables[name];
        }

        public bool HasVariable(string name) {
            return variables.ContainsKey(name);
        }

        public void RemoveVariable(string name) {
            variables.Remove(name);
        }

        public void AddMethod(Method method) {
            methods.Add(method);
        }

        public bool HasMethod(string name) {
            return methods.FindIndex(method => method.MethodName.Equals(name)) != -1;
        }

        public Method GetMethod(string name) {
            Method? method = methods.Find(method => method.MethodName.Equals(name));
            
            if (method == null) {
                throw new CommandNotFoundException($"Method {method} not declared.");
            }

            return method;
        }
    }
}
