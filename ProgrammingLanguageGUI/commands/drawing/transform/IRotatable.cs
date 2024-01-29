using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands.drawing.transform {
    /// Interface used for commands that support rotation transform.
    /// Implements a custom execute method and getter for retrieving
    /// Point details of the command.
    /// </summary>
    public interface IRotatable {
        public Point[] GetPoints();
        public void ExecuteTransform(Drawer drawer, int layer);
    }
}
