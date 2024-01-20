using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageGUI {
    partial class Application {

        private static readonly string[] separator = ["\n"];

        private void TextEditor_TextChanged(object sender, EventArgs e) {
            int numberOfLines = textEditor.Text.Split("\n").Length;
            int lineNumbersLength = lineText.Text.Split(separator, StringSplitOptions.None).Length - 1;

            if (lineNumbersLength != numberOfLines) {
                lineText.Text = "";
                for (int i = 0; i < numberOfLines; i++) {
                    lineText.AppendText((i + 1).ToString());
                    lineText.AppendText(Environment.NewLine);
                }
            }
        }

    }
}
