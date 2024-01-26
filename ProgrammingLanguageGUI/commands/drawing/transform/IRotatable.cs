using ProgrammingLanguageGUI.drawer;

namespace ProgrammingLanguageGUI.commands.drawing.transform {
    public interface IRotatable {
        public Point[] GetPoints();
        public void ExecuteTransform(Drawer drawer, int layer);
    }
}
