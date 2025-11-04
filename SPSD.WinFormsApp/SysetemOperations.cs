using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp
{
    public class SysetemOperations
    {
        /// <summary>
        /// 退出程序
        /// </summary>
        public static void Exit()
        {
            //参考;https://blog.csdn.net/qq15577969/article/details/89880290
            //通知winform消息循环退出，会在所有前台线程退出后，才真正的退出应用。（先停止线程，然后终止进程）
            //Application.Exit();
            //立即终止当前进程，应用程序即强制退出，返回exitcode给操作系统。（直接终止进程）
            Environment.Exit(0);
        }

        /// <summary>
        /// 重新启动应用程序
        /// </summary>
        public static void ReStart()
        {
            //Application.Exit();
            //System.Diagnostics.Process.Start(Application.ExecutablePath);
            System.Diagnostics.Process.Start(Application.ExecutablePath, "Reboot");
            Environment.Exit(0);
        }
    }
}
