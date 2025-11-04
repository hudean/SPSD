using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp
{
    public static class LsPrePost
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsProc ewp, int lParam);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, EnumChildWindowsProc ewp, int lParam);

        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
        public delegate bool EnumChildWindowsProc(IntPtr hWnd, int lParam);

        private static readonly string targetText = "LS-PrePost";

        public static List<string> FindTargetWindows()
        {
            var result = new List<string>();
            var processedWindows = new List<IntPtr>(); // 用于记录已处理的窗口句柄
            EnumWindows((hWnd, lParam) =>
            {
                if (processedWindows.Contains(hWnd))
                {
                    return true; // 如果已处理过，跳过
                }
                StringBuilder windowText = new StringBuilder(256);
                GetWindowText(hWnd, windowText, windowText.Capacity);
                //Console.WriteLine(windowText.ToString());
                if (windowText.ToString().Contains(targetText))
                {
                    processedWindows.Add(hWnd); // 添加已处理的窗口句柄
                    result.AddRange(FindChildWindows(hWnd));
                }
                return true;
            }, 0);

            return result;
        }

        public static List<string> FindChildWindows(IntPtr hWndParent)
        {
            var result = new List<string>();
            var processedWindows = new List<IntPtr>(); // 用于记录已处理的子窗口句柄
            EnumChildWindows(hWndParent, (hWnd, lParam) =>
            {
                if (processedWindows.Contains(hWnd))
                {
                    return true; // 如果已处理过，跳过
                }
                StringBuilder windowText = new StringBuilder(256);
                GetWindowText(hWnd, windowText, windowText.Capacity);

                if (windowText.ToString().Contains(">"))
                {
                    processedWindows.Add(hWnd); // 添加已处理的子窗口句柄
                    uint handleValue = (uint)hWnd.ToInt32(); // 将句柄转换为无符号整数
                    string handleHex = handleValue.ToString("X"); // 将句柄格式化为十六进制字符串
                    result.Add($"0x{handleHex}");
                }

                return true;
            }, 0);

            return result;
        }


    }

    public static class WindowManager_WarheadSimlink
    {
        // 声明用于发送消息的Windows API函数
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        // Windows消息常量
        const int WM_SETTEXT = 0x000C;
        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int VK_RETURN = 0x0D; // Enter键的虚拟键码

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsProc ewp, int lParam);

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, EnumWindowsProc ewp, int lParam);


        public static void SendTextToWindow(string handleStr, string text)
        {
            IntPtr handle = new IntPtr(Convert.ToInt32(handleStr, 16)); // 将16进制字符串转换为IntPtr

            // 发送消息到指定窗口
            SendMessage(handle, WM_SETTEXT, 0, text);
            // 模拟按下回车键
            SendMessage(handle, WM_KEYDOWN, VK_RETURN, IntPtr.Zero);
            SendMessage(handle, WM_KEYUP, VK_RETURN, IntPtr.Zero);
        }
    }
}
