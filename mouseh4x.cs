using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

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

        public ThreadStart clickRef = new ThreadStart(CLICKALLTHEBUTTONS);
        public Thread clickThread;
        public int clickId;

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_HOTKEY = 0x0312;
        public const int HTCLIENT = 0x1;
        public const int HTCAPTION = 0x2;
        
        protected override void WndProc(ref Message m)
        {
            switch(m.Msg)
            {
                case WM_HOTKEY:
                    int id = m.WParam.ToInt32();
                    switch (id)
                    {
                        case 0: // hotkey b
                            if (this.clickId == 0)
                            {
                                this.clickThread = new Thread(clickRef);
                                this.clickThread.Name = "clickThread";
                                this.clickId = this.clickThread.ManagedThreadId;
                                this.clickThread.IsBackground = true;
                                this.clickThread.Start();
                            }
                            break;
                        case 1: // hotkey c
                            if (this.clickId != 0)
                            {
                                this.clickThread.Abort();
                                this.clickId = 0;
                            }
                            break;
                        case 2:
                            showAboutBox();
                            break;
                    }
                break;

                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT)
                    {
                        m.Result = (IntPtr)HTCAPTION;
                    }
                    return;
            }
            base.WndProc(ref m);
        }

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
            // Clicks the current position for 1 000 000 times, mkay.
            for (int i = 0; i < 1000000; i++)
            {
                leftMouseClick(yolo.X, yolo.Y);
                Thread.Sleep(10);
            }
        }

        public static void showAboutBox()
        {
            MessageBox.Show("This program clicks your mouse for 1 000 000 times at the current position.\nPress b to begin clicking, c to cancel, F1 to bring up this dialog.\nAlso, please not that b,c and f1 are hijacked by the program when it's running.\n\nMade by devvis 2k13 - Released under the terms of the MIT-license.", "About", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public mainWnd()
        {
            InitializeComponent();
            this.clickId = 0;
            RegisterHotKey(this.Handle, 0, 0x0000, 0x42); // hotkey b
            RegisterHotKey(this.Handle, 1, 0x0000, 0x43); // hotkey c
            RegisterHotKey(this.Handle, 2, 0x0000, 0x70); // hotkey f1
        }
    }
}
