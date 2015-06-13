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

        public ThreadStart clickRef = new ThreadStart(CLICKALLTHEBUTTONS);
        public Thread clickThread;
        public int clickId;

        
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                if (id == 0)
                {
                    if (this.clickId == 0)
                    {
                        this.clickThread = new Thread(clickRef);
                        this.clickThread.Name = "clickThread";
                        this.clickId = this.clickThread.ManagedThreadId;
                        this.clickThread.IsBackground = true;
                        this.clickThread.Start();
                        
                    }
                }
                else if (id == 1)
                {
                    if (this.clickId != 0)
                    {
                        this.clickThread.Abort();
                        this.clickId = 0;
                    }
                }
                else if (id == 2)
                {
                    MessageBox.Show("To start clicking, press B.\nTo stop clicking, press C.\nNote that Mouse Clicker will hijack\nC, B and F1 in a global context.\nSo you cannot type b or c while running this program.", this.Text);
                }
                else
                {
                    MessageBox.Show("Something is clearly not right..", this.Text);
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
            while (true)
            {
                leftMouseClick(yolo.X, yolo.Y);
                Thread.Sleep(10);
            }
        }

        public mainWnd()
        {
            InitializeComponent();
            this.clickId = 0;
            RegisterHotKey(this.Handle, 0, 0x0000, 0x42);
            RegisterHotKey(this.Handle, 1, 0x0000, 0x43);
            RegisterHotKey(this.Handle, 2, 0x0000, 0x70);
        }
    }
}
