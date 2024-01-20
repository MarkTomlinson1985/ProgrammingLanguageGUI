
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;

namespace ProgrammingLanguageGUI
{
    partial class Application
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            drawingPanel = new Panel();
            runPanel = new Panel();
            playButton = new Button();
            textEditor = new RichTextBox();
            terminalTabPanel = new Panel();
            outputText = new RichTextBox();
            lineText = new TextBox();
            runPanel.SuspendLayout();
            SuspendLayout();
            // 
            // drawingPanel
            // 
            drawingPanel.BackColor = SystemColors.ControlDarkDark;
            drawingPanel.BorderStyle = BorderStyle.FixedSingle;
            drawingPanel.Location = new Point(643, 48);
            drawingPanel.Name = "drawingPanel";
            drawingPanel.Size = new Size(620, 555);
            drawingPanel.TabIndex = 1;
            // 
            // runPanel
            // 
            runPanel.BackColor = Color.FromArgb(84, 86, 109);
            runPanel.Controls.Add(playButton);
            runPanel.ForeColor = SystemColors.ControlText;
            runPanel.Location = new Point(17, 3);
            runPanel.Name = "runPanel";
            runPanel.Size = new Size(1246, 39);
            runPanel.TabIndex = 2;
            // 
            // playButton
            // 
            playButton.BackColor = Color.FromArgb(84, 86, 109);
            playButton.FlatStyle = FlatStyle.Flat;
            playButton.ForeColor = Color.FromArgb(224, 224, 224);
            playButton.Location = new Point(572, 7);
            playButton.Name = "playButton";
            playButton.Size = new Size(48, 29);
            playButton.TabIndex = 0;
            playButton.Text = "Run";
            playButton.UseVisualStyleBackColor = false;
            // 
            // textEditor
            // 
            textEditor.AcceptsTab = true;
            textEditor.BackColor = Color.FromArgb(69, 69, 84);
            textEditor.BorderStyle = BorderStyle.None;
            textEditor.ForeColor = Color.FromArgb(232, 232, 232);
            textEditor.Location = new Point(57, 48);
            textEditor.Name = "textEditor";
            textEditor.Size = new Size(580, 555);
            textEditor.TabIndex = 3;
            textEditor.Text = "";
            textEditor.TextChanged += TextEditor_TextChanged;
            // 
            // terminalTabPanel
            // 
            terminalTabPanel.BackColor = Color.FromArgb(84, 86, 109);
            terminalTabPanel.BorderStyle = BorderStyle.FixedSingle;
            terminalTabPanel.Location = new Point(17, 609);
            terminalTabPanel.Name = "terminalTabPanel";
            terminalTabPanel.Size = new Size(1246, 26);
            terminalTabPanel.TabIndex = 3;
            // 
            // outputText
            // 
            outputText.BackColor = Color.FromArgb(69, 69, 84);
            outputText.BorderStyle = BorderStyle.None;
            outputText.ForeColor = Color.FromArgb(232, 232, 232);
            outputText.Location = new Point(17, 641);
            outputText.Name = "outputText";
            outputText.Size = new Size(1246, 221);
            outputText.TabIndex = 4;
            outputText.Text = "Output text\n";
            // 
            // lineText
            // 
            lineText.BackColor = Color.FromArgb(69, 69, 84);
            lineText.BorderStyle = BorderStyle.None;
            lineText.ForeColor = Color.FromArgb(232, 232, 232);
            lineText.Location = new Point(17, 48);
            lineText.Multiline = true;
            lineText.Name = "lineText";
            lineText.Size = new Size(34, 555);
            lineText.TabIndex = 5;
            lineText.Text = "1";
            lineText.TextAlign = HorizontalAlignment.Right;
            // 
            // Application
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(58, 60, 78);
            ClientSize = new Size(1275, 874);
            Controls.Add(lineText);
            Controls.Add(outputText);
            Controls.Add(terminalTabPanel);
            Controls.Add(textEditor);
            Controls.Add(runPanel);
            Controls.Add(drawingPanel);
            Name = "Application";
            Text = "Programming GUI";
            runPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel drawingPanel;
        private Panel runPanel;
        private RichTextBox textEditor;
        private Panel terminalTabPanel;
        private RichTextBox outputText;
        private TextBox lineText;
        private Button playButton;
    }
}
