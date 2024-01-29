namespace ProgrammingLanguageGUI.drawer {
    /// <summary>
    /// Class for holding the current state of drawer-related variables.
    /// </summary>
    public class DrawerProperties {
        public bool FillMode { get; set; } = false;
        public bool MultiColours { get; set; } = false;
        public bool DrawerEnabled { get; set; } = true;
        public bool SwitchLayer { get; set; } = false;
        public bool TransformEnabled { get; set; } = false;

        /// <summary>
        /// Resets the state of the properties to their default values.
        /// </summary>
        public void Reset() {
            FillMode = false;
            MultiColours = false;
            DrawerEnabled = true;
            SwitchLayer = false;
            TransformEnabled = false;
        }
    }
}
