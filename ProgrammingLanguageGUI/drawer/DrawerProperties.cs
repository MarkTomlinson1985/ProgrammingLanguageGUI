namespace ProgrammingLanguageGUI.drawer {

    public class DrawerProperties {
        public bool FillMode { get; set; } = false;
        public bool MultiColours { get; set; } = false;
        public bool DrawerEnabled { get; set; } = true;
        public bool SwitchLayer { get; set; } = false;

        public void Reset() {
            FillMode = false;
            MultiColours = false;
            DrawerEnabled = true;
            SwitchLayer = false;
        }
    }
}
