using System.Runtime.InteropServices;

namespace ProgrammingLanguageGUI {
    /// <summary>
    /// Shamelessly taken from StackOverflow. See https://stackoverflow.com/questions/192413/how-do-you-prevent-a-richtextbox-from-refreshing-its-display.
    /// Credit goes to users JohnnyM and puch4tek for solutions.
    /// Derived RichTextBox class that can disable repainting of the control.
    /// Stops 'flickering' when using Selections within the text box for recolouring text etc.
    /// </summary>
    public class ProgramEditor : RichTextBox {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETREDRAW = 0xB;
        const int WM_USER = 0x400;
        const int EM_GETEVENTMASK = WM_USER + 59;
        const int EM_SETEVENTMASK = WM_USER + 69;

        private IntPtr eventMask;

        public void StopRepaint() {
            // Stop redrawing:
            SendMessage(this.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
            // Stop sending of events:
            eventMask = SendMessage(this.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);
        }

        public void StartRepaint() {
            // turn on events
            SendMessage(this.Handle, EM_SETEVENTMASK, 0, eventMask);
            // turn on redrawing
            SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            // this forces a repaint, which for some reason is necessary in some cases.
            this.Invalidate();
        }
    }
}
