namespace ProgrammingLanguageGUI.commands.keywords {
    public class VariableManager {
        Dictionary<string, string> variables = new Dictionary<string, string>();

        public void AddVariable(string name, string value) {
            variables[name] = value;
        }

        public string GetVariable(string name) {
            return variables[name];
        }

        public bool HasVariable(string name) {
            return variables.ContainsKey(name);
        }
    }
}
