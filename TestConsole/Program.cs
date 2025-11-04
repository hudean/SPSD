using System.Runtime.InteropServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using SPSD.WebApi;

namespace TestConsole
{
    internal class Program
    {
        // 导入必要的 Win32 API 函数
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        // 委托定义
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        static void Main(string[] args)
        {

            ConsoleAppHelper.RunConsoleApp2();


            Console.WriteLine("ok");
            Console.ReadKey();


            var box = new BoxHelper.Box
            {
                charge_i = 150,        // 装药箱体的x方向长度
                charge_j = 200,         // 装药箱体的y方向长度
                charge_k = 150,         // 装药箱体的z方向长度
                nocharge_j = 80,       // 非装药箱体的y方向长度
                opening_i = 5,        // 箱体开口x方向长度
                opening_j = 10        // 箱体开口y方向长度
            };

            var medx = new BoxHelper.Edx
            {
                edx = 3,               // 网格尺寸cm
                edx_open =3 / 3.0       // 开坑网格尺寸cm，网格尺寸的三分之一
            };

            BoxHelper.WriteFile(box,medx);

            Console.ReadKey();

            //****************
            //TgHerper.CreateTgFile();
            double number = 123.45; // 示例数据
            string formattedString = number.ToString().PadLeft(10);

            Console.WriteLine("Hello, World!");
            //Cpp2CSharp2.Test();
            List<string> list = new List<string>();
            // 调用EnumWindows，列出所有可见窗口的标题
            //EnumWindows((hWnd, lParam) =>
            //{
            //    StringBuilder windowText = new StringBuilder(256);
            //    GetWindowText(hWnd, windowText, windowText.Capacity);

            //    var title = windowText.ToString();
            //    //Console.WriteLine("Window Title: " + title);
            //    list.Add(title);
            //    return true; // 继续枚举
            //}, IntPtr.Zero);

            EnumWindows((hWnd, lParam) =>
            {
                // 检查窗口是否可见
                if (IsWindowVisible(hWnd))
                {
                    StringBuilder windowTitle = new StringBuilder(256);
                    // 获取窗口标题
                    GetWindowText(hWnd, windowTitle, windowTitle.Capacity);

                    // 输出窗口句柄和窗口标题
                    if (windowTitle.Length > 0)
                    {
                        Console.WriteLine($"Window Handle: {hWnd}, Title: {windowTitle}");
                    }
                }
                return true; // 继续枚举
            }, IntPtr.Zero);

            list = list.Distinct().ToList();
            foreach (string item in list)
            {
                Console.WriteLine("Window Title: " + item);
            }
            Console.ReadKey();
        }
    }
}
