using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestConsole
{
    public static class ConsoleAppHelper
    {

        public static void RunConsoleApp()
        {
            //string filePath = @"C:\Users\mw\Desktop\console\ConsoleApp.exe"; // 替换为你的控制台应用程序的路径
            string filePath = @"D:\Program Files\ANSYS Inc\v231\ansys\bin\winx64\LSDYNA_dp.exe";
            string kFilePath = @"D:\test\2dwaterJet.k";
            string arguments = "i=" + kFilePath + " NCPU=" + 4.ToString() + " MEMORY=2g";

            string errorStatus = "E r r o r   t e r m i n a t i o n";
            string successStatus = "N o r m a l    t e r m i n a t i o n";

            // 创建一个新的进程
            using (Process process = new Process())
            {
                // 设置启动信息
                process.StartInfo.FileName = filePath; // 替换为你要执行的程序路径
                process.StartInfo.UseShellExecute = false; // 不使用操作系统外壳来启动进程
                process.StartInfo.RedirectStandardOutput = true; // 重定向标准输出流
                process.StartInfo.RedirectStandardError = true;  // 重定向错误输出流（如果需要）
                //process.StartInfo.CreateNoWindow = true; // 不显示控制台窗口
                process.StartInfo.CreateNoWindow = false; // 显示控制台窗口
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = @"D:\test";
                // 设置事件处理程序来读取标准输出
                process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (e.Data != null)
                        Console.WriteLine("输出: " + e.Data);  // 实时输出到控制台
                });

                // 设置事件处理程序来读取错误输出
                process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (e.Data != null)
                        Console.WriteLine("错误: " + e.Data);  // 实时错误输出到控制台
                });

                // 启动进程
                process.Start();

                // 启动异步读取
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // 等待进程结束
                process.WaitForExit();

                // 手动释放资源，确保进程的正确清理
                process.Close();
            }// 退出using块时会自动调用Dispose方法，释放资源
        }

        public static void RunConsoleApp2()
        {
            // lsdyna_dp.exe
            // D:\\Program Files\\ANSYS Inc\\v231\\ansys\\bin\\winx64\\LSDYNA231.exe
            // 替换为你的控制台应用程序的路径
            string filePath = @"D:\Program Files\ANSYS Inc\v231\ansys\bin\winx64\LSDYNA_dp.exe";
            //string filePath = @"C:\Users\mw\Desktop\console\ConsoleApp.exe"; 
            string kFilePath = @"D:\test\2dwaterJet.k";
            string arguments = "i=" + kFilePath + " NCPU=" + 4.ToString() + " MEMORY=2g";

            string errorStatus = "E r r o r   t e r m i n a t i o n";
            string successStatus = "N o r m a l    t e r m i n a t i o n";
            ProcessStartInfo s = new ProcessStartInfo()
            {
                FileName = filePath, // 替换为你要执行的程序路径
                UseShellExecute = false, // 不使用操作系统外壳来启动进程
                RedirectStandardOutput = true, // 重定向标准输出流
                RedirectStandardError = true,  // 重定向错误输出流（如果需要）
                CreateNoWindow = false, // 显示控制台窗口
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = arguments,
                WorkingDirectory = @"D:\test", // 设置工作目录
            };

            var process =  Process.Start(s);
            if (process != null) 
            {
                var sr = process.StandardOutput;
                while (!sr.EndOfStream)
                {
                    var str = sr.ReadLine()?.Trim();
                    Console.WriteLine(str);
                    if (!string.IsNullOrEmpty(str))
                    {
                        //470 t 1.7977E-02 dt 3.80E-05 write d3plot file            08/21/25 23:47:15
                        if (str.Contains("dt"))
                        {
                            //var res = str.Substring(7, 10);
                            //Console.WriteLine("时间：" + res);
                            var strs = str.Split(' ');
                            if (strs.Length > 2)
                            {
                                //double doubleNumber = 1.23E+7;
                                //Console.WriteLine("时间：" + strs[2]);
                                
                            }
                        }
                        else if (str.Contains(successStatus))
                        {
                            Console.WriteLine("完成");
                        }
                        else if (str.Contains(errorStatus))
                        {
                            Console.WriteLine("中止，出现错误");
                        }
                    }
                   
                }
                process.WaitForExit();
                process.Close();
            }
        }
    }
}


//i=D:\test3\output.k NCPU=4 MEMORY=2g
//i=D:\test3\output.k NCPU=4 MEMORY=2g
//D:\test3
//D:\test3
//D:\Program Files\ANSYS Inc\v231\ansys\bin\winx64\LSDYNA_dp.exe
//D:\Program Files\ANSYS Inc\v231\ansys\bin\winx64\LSDYNA_dp.exe