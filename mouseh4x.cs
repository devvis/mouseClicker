using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace mouseClicker
{
    public partial class mainWnd : Form
    {
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                if (id == 0)
                {
                    CLICKALLTHEBUTTONS();
                }
                else if (id == 1)
                {
                    Environment.Exit(1337);
                }
                else
                {
                    MessageBox.Show("wut");
                }

            }

            base.WndProc(ref m);
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
            
        }


        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }

        public static void leftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }
        public static void rightMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, xpos, ypos, 0, 0);
        }

        public static void CLICKALLTHEBUTTONS()
        {

            Point yolo = GetCursorPosition();
            // Clicks the current position for 1000 times, mkay.
            for (int i = 0; i < 1000; i++)
            {
                leftMouseClick(yolo.X, yolo.Y);
                System.Threading.Thread.Sleep(10);
            }
            MessageBox.Show("I doen.");
        }

        public mainWnd()
        {
            InitializeComponent();
            RegisterHotKey(this.Handle, 0, 0x0000, 0x42);
            RegisterHotKey(this.Handle, 1, 0x0000, 0x43);
        }
    }
}
