using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp
{
    public class WindowManager2
    {
        // 导入 Win32 API 函数 SetParent 和 FindWindow
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_SHOWNORMAL = 1;


        public static void EmbedThirdPartyWindow(string filePath,nint handle)
        {
            //string filePath = @"D:\software\LS-PrePost-2025R1(4.12)\lsprepost4.12.exe";
            if (File.Exists(filePath))
            {
                // 启动第三方应用
                Process thirdPartyApp = Process.Start(filePath);
                thirdPartyApp.WaitForInputIdle();

                // 找到该应用的窗口句柄
                IntPtr appHandle = thirdPartyApp.MainWindowHandle;

                // 将其嵌入到当前WinForms窗体中
                //SetParent(appHandle, this.Handle);
                SetParent(appHandle, handle);

                // 显示第三方窗口
                ShowWindow(appHandle, SW_SHOWNORMAL);
            }

        }
    }
}
