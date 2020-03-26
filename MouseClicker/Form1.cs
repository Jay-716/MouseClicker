using System;
using System.Threading;
using System.Windows.Forms;

namespace MouseClicker {
    public partial class MyForm : Form {
        private int LaterTime { get; set; }
        private int SleepTimePerClick { get; set; }
        private int TotalTime { get; set; }

        public MyForm() {
            InitializeComponent();
            LaterTime = 5;
            SleepTimePerClick = 50;
            TotalTime = 5;
            numericUpDown1.Value = 5;
            numericUpDown2.Value = 50;
            numericUpDown3.Value = 5;
            comboBox1.SelectedIndex = 0;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            LaterTime = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
            SleepTimePerClick = Convert.ToInt32(numericUpDown2.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e) {
            TotalTime = Convert.ToInt32(numericUpDown3.Value);
        }

        private void buttonStart_Click(object sender, EventArgs e) {
            Action<MyForm> action = new Action<MyForm>(MouseClickStart);
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            comboBox1.Enabled = false;
            buttonStart.Enabled = false;
            action.BeginInvoke(this, null, null);
        }

        private void MouseClickStart(MyForm myForm) {
            int ClickType = comboBox1.SelectedIndex;
            int Count = TotalTime * 1000 / SleepTimePerClick;
            Action<int, int> action;
            switch (ClickType) {
                case 0:
                    action = new Action<int, int>(Mouse.MouseLeftClick); break;
                case 1:
                    action = new Action<int, int>(Mouse.MouseRightClick); break;
                case 2:
                    action = new Action<int, int>(Mouse.MouseMiddleClick); break;
                default: throw new Exception("ClickType Argument Error");
            }
            Thread.Sleep(LaterTime * 1000);
            action.Invoke(Count, SleepTimePerClick);
            myForm.numericUpDown1.Enabled = true;
            myForm.numericUpDown2.Enabled = true;
            myForm.numericUpDown3.Enabled = true;
            myForm.comboBox1.Enabled = true;
            myForm.buttonStart.Enabled = true;
        }
    }

    class Mouse {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        //模拟鼠标滚轮滚动操作，必须配合dwData参数
        const int MOUSEEVENTF_WHEEL = 0x0800;

        public static void MouseLeftClick(int Count, int SleepTimePerClick) {
            for (int i = 0; i < Count; i++) {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Thread.Sleep(SleepTimePerClick);
            }
        }

        public static void MouseRightClick(int Count, int SleepTimePerClick) {
            for (int i = 0; i < Count; i++) {
                mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                Thread.Sleep(SleepTimePerClick);
            }
        }

        public static void MouseMiddleClick(int Count, int SleepTImerPerClick) {
            for (int i = 0; i < Count; i++) {
                mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                Thread.Sleep(SleepTImerPerClick);
            }
        }
    }
}
