using System.Configuration;
using System.Windows.Forms;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;

namespace SPSD.WinFormsApp
{
    

    internal static class Program
    {
        // Mutex，用于确保只运行一个实例
        private static Mutex mutex = new Mutex(true, "OnlyRun");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("程序已经在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SysetemOperations.Exit();
                }
            }
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            // "lsprepost4.12"
            closeProc("lsprepost");

            // 注册全局异常捕获事件
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Initialize();
        }

        // 处理未捕获的异常并记录日志
        private static void Application_ThreadException(object? sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // 处理异常，如记录日志、显示错误消息等
            Exception exception = e.Exception;
            // ToDo: 处理异常的逻辑代码
            //Log.Error(exception, "全局多线程异常捕捉：");

            MessageBox.Show("发生错误：" + exception.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            SysetemOperations.Exit();
            //RestartApplication();
        }

        // 处理未捕获的异常并记录日志
        private static void CurrentDomain_UnhandledException(object? sender, UnhandledExceptionEventArgs e)
        {
            // 处理异常，如记录日志、显示错误消息等
            Exception? exception = e.ExceptionObject as Exception;
            // ToDo: 处理异常的逻辑代码
            //Log.Error(exception, "全局异常捕捉：");

            MessageBox.Show("发生错误：" + exception?.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            SysetemOperations.Exit();

            //RestartApplication();
        }


        private static void Initialize()
        {
            IConfigurationBuilder cfgBuilder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json");
            IConfiguration configuration = cfgBuilder.Build();
            string? apiHost = configuration.GetValue<string?>("ApiHost");
            if (string.IsNullOrEmpty(apiHost))
            {
                throw new ArgumentException("请配置apiHost的参数");
            }
            StartApiServer(apiHost);

            Application.Run(new MainForm(configuration));
        }

        private static void StartApiServer(string apiHost)
        {
            Task.Run(() =>
            {
                var builder = WebApplication.CreateBuilder();
                builder.Services.AddControllers();

                //builder.Services.AddDbContext<AppDbContext>(options =>
                //    options.UseMySql(AppBasicConfig.ConnectionString, ServerVersion.AutoDetect(AppBasicConfig.ConnectionString)));
                builder.Services.AddCors();
                var app = builder.Build();

                app.UseCors(options =>
                {
                    options.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                    //.AllowCredentials();
                }); 
                app.UseDefaultFiles();//使用静态文件
                // 启用静态文件中间件
                app.UseStaticFiles();
                app.MapGet("/", () => "Hello World!");
                app.MapControllers();
                app.Run(apiHost);
            });
        }


        private static void closeProc(string ProcName)
        {
            //bool result = false;
            //System.Collections.ArrayList procList = new System.Collections.ArrayList();
            //string tempName = "";

            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                //tempName = thisProc.ProcessName;
                //procList.Add(tempName);
                //if (tempName == ProcName)
                if (thisProc.ProcessName.StartsWith(ProcName))
                {
                    if (!thisProc.CloseMainWindow())
                        thisProc.Kill(); //当发送关闭窗口命令无效时强行结束进程                    
                    //result = true;
                }
            }
           // return result;
        }
    }
}