namespace ProgrammingLanguageGUI.runner {
    /// <summary>
    /// Record class containing the results of syntax checking a program.
    /// Utilizes builder pattern for construction the SyntaxResults object.
    /// </summary>
    public class SyntaxResults {
        private string[] syntaxErrors;
        private int[] lineNumbers;

        public string[] SyntaxErrors { get { return syntaxErrors; } set { syntaxErrors = value; } }
        public int[] LineNumbers { get { return lineNumbers; } set { lineNumbers = value; } }
        public bool HasErrors { get { return syntaxErrors != null && syntaxErrors.Length > 0; } }

        public SyntaxResults(string[] syntaxErrors, int[] lineNumbers) {
            this.syntaxErrors = syntaxErrors;
            this.lineNumbers = lineNumbers;
        }

        public static SyntaxResultsBuilder Builder() {
            return new SyntaxResultsBuilder();
        }

        public class SyntaxResultsBuilder {
            private string[] syntaxErrors;
            private int[] lineNumbers;

            public SyntaxResultsBuilder LineNumbers(int[] lineNumbers) {
                this.lineNumbers = lineNumbers;
                return this;
            }

            public SyntaxResultsBuilder SyntaxErrors(string[] syntaxErrors) {
                this.syntaxErrors = syntaxErrors;
                return this;
            }

            public SyntaxResults Build() {
                return new SyntaxResults(syntaxErrors, lineNumbers);
            }
        }
    }
}
