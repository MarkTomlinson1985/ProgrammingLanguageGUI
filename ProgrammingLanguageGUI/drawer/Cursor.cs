using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageGUI.drawer {
    public class Cursor : PictureBox {
        private int xPosition = 0;
        private int yPosition = 0;
        public int X {
            get { return xPosition; }
            set { xPosition = value; }
        }
        public int Y {
            get { return yPosition; }
            set { yPosition = value; }
        }

        public Cursor() { }
    }
}
