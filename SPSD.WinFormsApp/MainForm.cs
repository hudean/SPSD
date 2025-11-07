using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SPSD.WinFormsApp.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SPSD.WinFormsApp
{
    public partial class MainForm : Form
    {
        // 导入 Win32 API 函数 SetParent 和 FindWindow
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int Width, int Height, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        public const int SW_SHOWNORMAL = 1;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOACTIVATE = 0x0010;

        //public MainForm()
        //{
        //    InitializeComponent();
        //    Init();
        //}

        private readonly IConfiguration _configuration;

        public MainForm(IConfiguration configuration)
        {
            InitializeComponent();
            //if (DateTime.Now > DateTime.Parse("2026-03-03 00:00:00"))
            //{
            //    throw new Exception("未授权");
            //}
            this.StartPosition = FormStartPosition.CenterScreen;
            _lsPrePostExePath = configuration.GetValue<string>("LsPrePostExePath")!;
            _tgExePath = configuration.GetValue<string>("TgExePath")!;
            _dynaExePath = configuration.GetValue<string>("DynaExePath")!;
            _webHost = configuration.GetValue<string>("WebHost")!;
            _configuration = configuration;
            Init();
        }

        #region prvite field

        private IntPtr _appHandle;

        // 声明外部程序的进程对象
        private Process? thirdPartyApp;

        //private string LsPrePostPath = @"D:\software\LS-PrePost-2025R1(4.12)\lsprepost4.12.exe";

        private readonly string _lsPrePostExePath;// = "D:\\software\\LS-PrePost-2025R1(4.12)\\lsprepost4.12.exe";

        private readonly string _tgExePath;// = "C:\\TrueGrid\\tg.exe";

        private readonly string _dynaExePath;

        private readonly string _webHost;

        #endregion prvite field

        #region form event methods

        private async void MainForm_Load(object sender, EventArgs e)
        {
            // 设置窗体的 AutoScaleMode 属性来避免缩放：
            this.AutoScaleMode = AutoScaleMode.None;
            //禁用 DPI 缩放：
            //this.AutoScaleDimensions = new SizeF(1.0f, 1.0f);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(1920, 0);  // 确保窗体位于第一个显示器的右边

            //自动滚动
            //this.AutoScroll = true;
            this.Width = 1920;
            this.Height = 1080;
            this.ClientSize = new Size(1920, 1080);  // 设置实际显示区域大小
            //调整窗体最大化行为
            //this.WindowState = FormWindowState.Maximized;
            // 窗体居中
            this.StartPosition = FormStartPosition.CenterScreen;
            //CenterFormOnScreen();

            LocalEventBus.EventShowForm += async (request) => await EventShowFormAsync(request);

            LocalEventBus.EventCreateTgFile += async (request) => await CreateTgFileEventAsync(request);

            await webView.EnsureCoreWebView2Async(null);
            //webView.EnsureCoreWebView2Async(null).GetAwaiter().GetResult();
            //webView.EnsureCoreWebView2Async(null).ConfigureAwait(false).GetAwaiter();
            //接收webview发送的数据
            webView.CoreWebView2.WebMessageReceived += ReceivedProcess;
            await ClearBrowserCache();
        }

        private async Task ClearBrowserCache()
        {
            if (webView.CoreWebView2 != null)
            {
                await webView.CoreWebView2.Profile.ClearBrowsingDataAsync();
                //Console.WriteLine("缓存已清理");
            }
        }

        public void CenterFormOnScreen()
        {
            Screen screen = Screen.FromPoint(this.Location);
            int screenWidth = screen.WorkingArea.Width;
            int screenHeight = screen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;
            int x = screen.Bounds.X + (screenWidth - formWidth) / 2;
            int y = screen.Bounds.Y + (screenHeight - formHeight) / 2;
            this.Location = new Point(x, y);
        }

        // 在窗体大小变化时更新第三方程序窗口的大小
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (thirdPartyApp != null && !thirdPartyApp.HasExited)
            {
                IntPtr appHandle = thirdPartyApp.MainWindowHandle;
                AdjustThirdPartyWindowSizeAndPosition(appHandle);
            }
        }

        // 在窗体位置变化时更新第三方程序窗口的位置
        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (thirdPartyApp != null && !thirdPartyApp.HasExited)
            {
                IntPtr appHandle = thirdPartyApp.MainWindowHandle;
                AdjustThirdPartyWindowSizeAndPosition(appHandle);
            }
        }

        // 确保在WinForms窗体关闭时关闭第三方应用程序
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 检查第三方程序是否存在
            //if (!thirdPartyApp.HasExited)
            //{
            //    thirdPartyApp.Kill(); // 强制关闭第三方程序
            //}

            // 检查第三方程序是否仍在运行
            if (thirdPartyApp != null && !thirdPartyApp.HasExited)
            {
                try
                {
                    thirdPartyApp.Kill();  // 强制结束第三方应用程序
                    thirdPartyApp.WaitForExit();  // 等待进程退出
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"无法关闭第三方应用程序: {ex.Message}");
                }
            }
        }

        private void ClosingApp()
        {
            // 检查第三方程序是否存在
            //if (!thirdPartyApp.HasExited)
            //{
            //    thirdPartyApp.Kill(); // 强制关闭第三方程序
            //}

            // 检查第三方程序是否仍在运行
            if (thirdPartyApp != null && !thirdPartyApp.HasExited)
            {
                try
                {
                    thirdPartyApp.Kill();  // 强制结束第三方应用程序
                    thirdPartyApp.WaitForExit();  // 等待进程退出
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"无法关闭第三方应用程序: {ex.Message}");
                }
            }

            Environment.Exit(0);
        }

        private void btnInto_Click(object sender, EventArgs e)
        {
            //控件置顶
            //webView.BringToFront();
            //webView.Source = new Uri("http://localhost:5173/#/main");
        }

        #endregion form event methods

        #region prvite methods

        private void Init()
        {
            // 隐藏标题栏
            this.FormBorderStyle = FormBorderStyle.None;
            // 任务栏不显示窗体
            //this.ShowInTaskbar = false;

            //this.btnInto.BringToFront();

            webView.Dock = DockStyle.Fill;
            //webView.Source = new Uri("http://localhost:5173/#/home");
            //webView.Source = new Uri("http://localhost:5295/#/home");
            webView.Source = new Uri($"{_webHost}/#/home");
            StartLsPrePost();
            webView.BringToFront();
        }

        #region 启动外部程序

        /// <summary>
        /// 启动外部程序
        /// </summary>
        private void StartLsPrePost()
        {
            // 验证LSPrePostPath是否存在
            //if (File.Exists(LsPrePostPath))
            //{
            //    appContainer1.AppFilename = LsPrePostPath;
            //    appContainer1.Start();
            //}
            //else
            //{
            //    MessageBox.Show("LSPrePostPath指定的程序文件不存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //EmbedThirdPartyWindow();
            // 启动并嵌入第三方应用程序
            StartAndEmbedThirdPartyApp();
        }

        private void StartAndEmbedThirdPartyApp()
        {
            // 1340 * 1010 px
            //string filePath = @"D:\software\LS-PrePost-2025R1(4.12)\lsprepost4.12.exe";

            string filePath = _lsPrePostExePath;

            if (File.Exists(filePath))
            {
                // 启动第三方应用并获取其窗口句柄
                thirdPartyApp = Process.Start(filePath);
                thirdPartyApp.WaitForInputIdle();
                // 获取第三方程序的窗口句柄
                IntPtr appHandle = thirdPartyApp.MainWindowHandle;
                // 将其嵌入到当前WinForms窗体中
                SetParent(appHandle, this.lsPanel.Handle);

                // 显示第三方窗口
                ShowWindow(appHandle, SW_SHOWNORMAL);

                // 调整第三方程序的窗口大小与位置，适应当前WinForm大小
                // 调整第三方程序的窗口大小和位置，适应当前WinForm大小和位置
                AdjustThirdPartyWindowSizeAndPosition(appHandle);
                //this.Invoke(new Action(() =>
                //    {
                // WindowManager.SetParent(this.lsPanel.Handle, "LS-PrePost(R) 2025 R1 (v4.12.1) - 17Jan2025");
                //}));
                _appHandle = appHandle;
            }
        }

        private void AdjustThirdPartyWindowSizeAndPosition(IntPtr appHandle)
        {
            //// 获取当前WinForm的大小
            //int formWidth = this.lsPanel.ClientSize.Width;
            //int formHeight = this.lsPanel.ClientSize.Height;

            //// 调整第三方程序窗口的大小
            //SetWindowPos(appHandle, IntPtr.Zero, 0, 0, formWidth, formHeight, SWP_NOZORDER | SWP_NOMOVE | SWP_NOACTIVATE);

            // 获取当前WinForm的位置和大小
            //int formX = this.DesktopLocation.X;
            //int formY = this.DesktopLocation.Y;
            //int formX = this.lsPanel.Location.X;
            //int formY = this.lsPanel.Location.Y;
            int formX = 0;
            int formY = 0;
            int formWidth = this.lsPanel.ClientSize.Width;
            int formHeight = this.lsPanel.ClientSize.Height;

            // 调整第三方程序窗口的大小和位置
            //var res = SetWindowPos(appHandle, IntPtr.Zero, formX, formY, formWidth, formHeight, SWP_NOZORDER | SWP_NOMOVE | SWP_NOACTIVATE);

            var res = SetWindowPos(appHandle, IntPtr.Zero, formX, formY, formWidth, formHeight, SWP_NOZORDER | SWP_NOACTIVATE);
        }

        #endregion 启动外部程序

        private async Task EventShowFormAsync(RequestShowForm request)
        {
            //LocalEventBus.SubjectEventAsync(async (param) =>
            //{
            //    await Task.CompletedTask;

            //});
            await Task.CompletedTask;
            if (request.IsShow)
            {
                this.Invoke(new Action(() =>
                {
                    this.lsPanel.BringToFront();
                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    webView.BringToFront();
                }));
            }
        }

        private async Task<string> CreateTgFileEventAsync(CreateTgFileModel request)
        {
            //await Task.CompletedTask;
            await CreateModelTgFile(request);
            return string.Empty;
        }

        /// <summary>
        /// WebView触发事件传递消息给后端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ReceivedProcess(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var cmdJson = e.TryGetWebMessageAsString();

            var cmdData = JsonConvert.DeserializeObject<CommandRequestData>(cmdJson);
            if (cmdData is null)
            {
                return;
            }

            switch (cmdData.Cmd)
            {
                case "closeApp":
                    ClosingApp();
                    break;

                case "OpenFolderDialog":
                case "OpenFolderDialogByEdit":
                    // 处理命令
                    SelectFolderPath(cmdData.Cmd);
                    break;

                case "CreateTgFile":
                    // 处理命令
                    if (!string.IsNullOrEmpty(cmdData.JsonData))
                    {
                        var requestData = JsonConvert.DeserializeObject<CreateTgFileModel>(cmdData.JsonData!);
                        if (requestData is not null)
                        {
                            await CreateModelTgFile(requestData);
                        }
                    }
                    break;

                case "LoadWarBodyModel":
                    // 处理命令
                    if (!string.IsNullOrEmpty(cmdData.JsonData))
                    {
                        var requestData = JsonConvert.DeserializeObject<CreateTgFileModel>(cmdData.JsonData!);
                        if (requestData is not null)
                        {
                            await LoadWarBodyModel(requestData);
                        }
                    }
                    break;

                case "CreateKangBaoTgFile":
                    // 处理命令
                    if (!string.IsNullOrEmpty(cmdData.JsonData))
                    {
                        var requestData = JsonConvert.DeserializeObject<CreateKangBaoTgFileModel>(cmdData.JsonData!);
                        if (requestData is not null)
                        {
                            await CreateKangBaoTgFile(requestData);
                        }
                    }
                    break;
                // 计算结果
                case "HandleComputeResult":
                    //{
                    //    string str = "{\"projectId\":1,\"computeResultStatus\":3,\"folderPath\":\"D:\\\\test1\",\"partCount\":10}";
                    //    var requestData = JsonConvert.DeserializeObject<ComputeResultStatusModel>(str);
                    //    if (requestData is not null)
                    //    {
                    //        await HandleComputeResultExtract(requestData);
                    //    }
                    //    return;
                    //}

                    if (!string.IsNullOrEmpty(cmdData.JsonData))
                    {
                        var requestData = JsonConvert.DeserializeObject<ComputeResultModel>(cmdData.JsonData!);
                        if (requestData is not null)
                        {
                            //await RunDyna2("", _dynaExePath, 10);

                            await GenerateKFileByComputeResult(requestData);
                        }
                    }
                    break;

                case "HandleComputeResultByKangBao":
                    if (!string.IsNullOrEmpty(cmdData.JsonData))
                    {
                        var requestData = JsonConvert.DeserializeObject<KangBaoComputeResultModel>(cmdData.JsonData!);
                        if (requestData is not null)
                        {
                            await GenerateMainFileByKangBaoComputeResult(requestData);
                        }
                    }
                    break;
                // 计算结果提取
                case "HandleComputeResultExtract":
                    if (!string.IsNullOrEmpty(cmdData.JsonData))
                    {
                        var requestData = JsonConvert.DeserializeObject<ComputeResultStatusModel>(cmdData.JsonData!);
                        if (requestData is not null)
                        {
                            await HandleComputeResultExtract(requestData);
                        }
                    }
                    break;

                default:
                    break;
            }
            //webView.CoreWebView2.PostWebMessageAsString("wpf发送：" + cmdJson);
        }

        /// <summary>
        /// 后端发送消息给WebView前端
        /// </summary>
        /// <param name="responseData"></param>
        private void PostWebMessageAsString(CommandResponseData responseData)
        {
            // webView.CoreWebView2.PostWebMessageAsString("这个一个示例字符串");
            // webView.CoreWebView2.PostWebMessageAsJson("这个一个示例Json");
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver() // 使用驼峰命名法
            };
            var json = JsonConvert.SerializeObject(responseData, settings);
            //webView.CoreWebView2.PostWebMessageAsString(json);
            this.Invoke(new Action(() =>
            {
                webView.CoreWebView2.PostWebMessageAsString(json);
            }));
        }

        private void SelectFolderPath(string cmdCategory)
        {
            string folderPath = string.Empty;
            this.Invoke(new Action(() =>
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择项目文件夹";
                //dialog.InitialDirectory = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(dialog.SelectedPath))
                    {
                        MessageBox.Show(this, "文件夹路径不能为空", "提示");
                        return;
                    }
                }
                folderPath = dialog.SelectedPath;
            }));

            //webView.CoreWebView2.PostWebMessageAsString(dialog.SelectedPath);
            CommandResponseData<object> responseData = new CommandResponseData<object>()
            {
                Success = true,
                //CmdCategory = "OpenFolderDialog",
                CmdCategory = cmdCategory,
                Code = 200,
                Data = new
                {
                    FolderPath = folderPath
                }
            };
            PostWebMessageAsString(responseData);
        }

        /// <summary>
        /// 抗弹模块生成model.tg和model.k文件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task CreateModelTgFile(CreateTgFileModel request)
        {
            await Task.CompletedTask;
            CommandResponseData responseData = new CommandResponseData()
            {
                Success = true,
                CmdCategory = "CreateTgFile",
                Code = 200,
            };

            if (string.IsNullOrEmpty(request.FolderPath))
            {
                responseData.Success = false;
                responseData.Message = "项目没有选择文件夹";
                PostWebMessageAsString(responseData);
                return;
            }
            //webView.CoreWebView2.PostWebMessageAsString("开始生成tg文件");
            responseData.Message = "开始生成tg文件";
            PostWebMessageAsString(responseData);
            //request.FolderPath = @"C:\Users\mw\Desktop\test";
            try
            {
                string fileName = "model.tg";//moxing.tg

                TgHerper.CreateModelTgFile(request, fileName);
                //webView.CoreWebView2.PostWebMessageAsString("生成tg文件成功");
                //string FragmentTgFile = @".\temp\\moxing.tg";
                //AnsysModel.GenerateFragmentTgFile(FragmentTgFile, tmpPoints, fragmentSize, 1, df, hf, db, hb, h, dL, dd1, h1);
                //通过tg文件生成k模型
                //AnsysModel.GenerateKFile("moxing.tg", @".\temp", "C:\\TrueGrid\\tg.exe");

                //AnsysModel.GenerateKFile2(request.FolderPath);

                if (File.Exists(_tgExePath))
                {
                    //webView.CoreWebView2.PostWebMessageAsString("准备生成k文件");
                    AnsysModel.GenerateKFile(fileName, request.FolderPath, _tgExePath);
                    // webView.CoreWebView2.PostWebMessageAsString("生成k文件成功");
                }
                LoadModel(request.FolderPath, "model.k");
                LoadModel(request.FolderPath, "model.k");

                //webView.CoreWebView2.PostWebMessageAsString("模型加载成功");
                responseData.Message = "模型加载成功";
                PostWebMessageAsString(responseData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //webView.CoreWebView2.PostWebMessageAsString("生成文件失败，错误内容：" + ex.Message);
                responseData.Success = false;
                responseData.Message = "生成文件失败，错误内容：" + ex.Message;
                PostWebMessageAsString(responseData);
            }
        }

        /// <summary>
        /// 加载弹体模型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task LoadWarBodyModel(CreateTgFileModel request)
        {
            await Task.CompletedTask;
            //string fileName = "bullet.k";
            string outputFilePath = Path.Combine(request.FolderPath, "bullet.k");
            if (!File.Exists(outputFilePath))
            {
                string kFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
                string inputFilePath = Path.Combine(kFilePath, "bullet.k");
                File.Copy(inputFilePath, outputFilePath, true);
            }

            LoadModel(request.FolderPath, "bullet.k");
            LoadModel(request.FolderPath, "bullet.k");
            CommandResponseData responseData = new CommandResponseData()
            {
                Success = true,
                CmdCategory = "LoadWarBodyModel",
                Code = 200,
                Message = "模型加载成功"
            };
            PostWebMessageAsString(responseData);
        }

        /// <summary>
        /// 调用PrePost程序加载模型文件并生成model.k文件
        /// </summary>
        /// <param name="folderPath"></param>
        private void LoadModel(string folderPath, string fileName)//= "model.k"
        {
            //string absolutePath = Path.Combine(folderPath, "model.k");
            string absolutePath = Path.Combine(folderPath, fileName);
            if (!File.Exists(absolutePath))
            {
                return;
            }
            // 这个this.Invoke用处不大
            //this.Invoke(() =>
            //{
            var handles = LsPrePost.FindChildWindows(_appHandle);

            if (handles.Count > 0)
            {
                //webView.CoreWebView2.PostWebMessageAsString("准备加载模型");
                // 获取句柄号
                string handleStr = handles.Last();
                // 发送文本到指定窗口
                //string absolutePath = Path.GetFullPath(@".\temp");
                string str = $" open keyword \"{absolutePath}\"";
                //"D:\\shubin\\二院\\WarheadSimlink\\WarheadSimlink2\\bin\\x64\\Debug\\Temp\\moxing.k\"
                WindowManager_WarheadSimlink.SendTextToWindow(handleStr, str);
                //MessageBox.Show("OK", "提示");
            }
            else
            {
                MessageBox.Show("未发现PrePost窗口！", "请检查");
            }
            // });
        }

        /// <summary>
        /// 抗爆生成model.tg和model.k文件
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        private async Task CreateKangBaoTgFile(CreateKangBaoTgFileModel requestData)
        {
            CommandResponseData responseData = new CommandResponseData()
            {
                Success = true,
                CmdCategory = "CreateKangBaoTgFile",
                Code = 200,
            };

            await Task.CompletedTask;
            if (string.IsNullOrEmpty(requestData.FolderPath))
            {
                responseData.Success = false;
                responseData.Message = "项目没有选择文件夹";
                PostWebMessageAsString(responseData);
                return;
            }
            responseData.Message = "开始生成tg文件";
            PostWebMessageAsString(responseData);
            try
            {
                string fileName = "box.tg";//moxing.tg
                BoxHelper.CreateKangBaoTgFile(requestData, fileName);

                if (File.Exists(_tgExePath))
                {
                    AnsysModel.GenerateKFile(fileName, requestData.FolderPath, _tgExePath);
                }
                string boxFileName = "model.k";
                LoadModel(requestData.FolderPath, boxFileName);
                LoadModel(requestData.FolderPath, boxFileName);

                responseData.Message = "模型加载成功";
                PostWebMessageAsString(responseData);
            }
            catch (Exception ex)
            {
                responseData.Success = false;
                responseData.Message = "生成文件失败，错误内容：" + ex.Message;
                PostWebMessageAsString(responseData);
            }
        }

        /// <summary>
        /// 抗弹模块计算结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task GenerateKFileByComputeResult(ComputeResultModel request)
        {
            CommandResponseData responseData = new CommandResponseData()
            {
                Success = true,
                CmdCategory = "HandleComputeResult",
                Code = 200,
            };
            if (string.IsNullOrEmpty(request.FolderPath))
            {
                responseData.Success = false;
                responseData.Message = "项目没有选择文件夹";
                PostWebMessageAsString(responseData);
                return;
            }
            // 根据不同的子弹类型调用不同的K文件-7.62毫米子弹对应bullet.k

            #region 把bullet.k copy到工作目录上

            //string kFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            //string inputFilePath = Path.Combine(kFilePath, "bullet.k");
            //string outputFilePath = Path.Combine(request.FolderPath, "bullet.k");
            //File.Copy(inputFilePath, outputFilePath, true);
            string outputFilePath = Path.Combine(request.FolderPath, "bullet.k");
            if (!File.Exists(outputFilePath))
            {
                string kFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
                string inputFilePath = Path.Combine(kFilePath, "bullet.k");
                File.Copy(inputFilePath, outputFilePath, true);
            }

            #endregion 把bullet.k copy到工作目录上

            #region 生成output.k文件（output.k文件就是main.k文件）

            //webView.CoreWebView2.PostWebMessageAsString("开始生成k文件");
            responseData.Message = "开始生成k文件";
            PostWebMessageAsString(responseData);
            await AnsysModel.GenerateKFileByComputeResult(request);
            //webView.CoreWebView2.PostWebMessageAsString("生成k文件完成");
            responseData.Message = "生成k文件完成";
            PostWebMessageAsString(responseData);

            #endregion 生成output.k文件（output.k文件就是main.k文件）

            #region

            // 这个运行Dyna的操作是非常耗时的操作，必须运行完成才能提取结果

            // 状态监控-返回字符串包含：E r r o r   t e r m i n a t i o 是错误的
            //string errorStatus = "E r r o r   t e r m i n a t i o n";
            //string successStatus = "N o r m a l    t e r m i n a t i o n";
            if (File.Exists(_dynaExePath))
            {
                //  string kFile = Path.Combine(request.FolderPath, "output.k");
                //AnsysModel.RunLs971ByDyna(kFile, _dynaExePath, request.FolderPath, 4, "2g");

                //AnsysModel.RunDyna(request.FolderPath, _dynaExePath, request.endtim);
                //RunDyna(request.FolderPath, _dynaExePath, request.endtim);
                await RunDyna2(request.FolderPath, _dynaExePath, request.endtim);
            }

            #endregion prvite methods
        }

        /// <summary>
        /// 抗爆模块计算结果
        /// </summary>
        /// <param name="resultModel"></param>
        /// <returns></returns>

        private async Task GenerateMainFileByKangBaoComputeResult(KangBaoComputeResultModel resultModel)
        {
            CommandResponseData responseData = new CommandResponseData()
            {
                Success = true,
                CmdCategory = "HandleComputeResultByKangBao",
                Code = 200,
            };
            if (string.IsNullOrEmpty(resultModel.FolderPath))
            {
                responseData.Success = false;
                responseData.Message = "项目没有选择文件夹";
                PostWebMessageAsString(responseData);
                return;
            }

            #region 生成output.k文件（output.k文件就是main.k文件）
            responseData.Message = "开始生成k文件";
            PostWebMessageAsString(responseData);
            await AnsysModel.GenerateMainFileByKangBaoComputeResult(resultModel);
            responseData.Message = "生成k文件完成";
            PostWebMessageAsString(responseData);

            #endregion 生成output.k文件（output.k文件就是main.k文件）

            //if (File.Exists(_dynaExePath))
            //{
            //    await RunDyna2(request.FolderPath, _dynaExePath, request.endtim);
            //}
        }

        /// <summary>
        /// 运行LS-DYNA
        /// </summary>
        /// <param name="folderPath">项目文件夹</param>
        /// <param name="dynaFilePath">Dyna程序路径</param>
        /// <param name="endtime">计算时长endtim(仿真参数设置)</param>
        public void RunDyna(string folderPath, string dynaFilePath, double endtime)
        {
            CommandResponseData<int> responseData = new CommandResponseData<int>()
            {
                Success = true,
                CmdCategory = "HandleComputeResultByDyna",
                Code = 200,
                Data = 0,
            };

            int status = 0;

            try
            {
                string kFilePath = Path.Combine(folderPath, "output.k");
                //string arguments = "i=" + kFilePath + " NCPU=" + 4.ToString() + " MEMORY=2g";
                string arguments = $"i={kFilePath} NCPU=4 MEMORY=2g";

                string errorStatus = "E r r o r   t e r m i n a t i o n";
                string successStatus = "N o r m a l    t e r m i n a t i o n";

                ProcessStartInfo s = new ProcessStartInfo()
                {
                    FileName = dynaFilePath, // 替换为你要执行的程序路径
                    UseShellExecute = false, // 不使用操作系统外壳来启动进程
                    RedirectStandardOutput = true, // 重定向标准输出流
                    RedirectStandardError = true,  // 重定向错误输出流（如果需要）
                    CreateNoWindow = false, // 显示控制台窗口
                    WindowStyle = ProcessWindowStyle.Normal,
                    Arguments = arguments,
                    WorkingDirectory = folderPath, // 设置工作目录
                    // UseShellExecute = true // 不重定向，使用系统默认方式启动
                };

                var process = Process.Start(s);
                if (process != null)
                {
                    var sr = process.StandardOutput;
                    while (!sr.EndOfStream)
                    {
                        var str = sr.ReadLine()?.Trim();
                        // Console.WriteLine(str);
                        if (!string.IsNullOrEmpty(str))
                        {
                            // 470 t 1.7977E-02 dt 3.80E-05 write d3plot file            08/21/25 23:47:15
                            if (str.Contains(" dt "))
                            {
                                //var res = str.Substring(6, 10);
                                //Console.WriteLine("时间：" + res);
                                var strs = str.Split(' ');
                                if (strs.Length > 2)
                                {
                                    try
                                    {
                                        //Console.WriteLine("时间：" + strs[2]);
                                        // 实时输出的时间
                                        var time = Convert.ToDouble(strs[2]);
                                        var result = (time / endtime) * 100;
                                        //responseData.Data = (int)result;
                                        responseData.Data = (int)Math.Ceiling(result);
                                        PostWebMessageAsString(responseData);
                                    }
                                    catch { }
                                }
                            }
                            else if (str.Contains(successStatus))
                            {
                                // Console.WriteLine("完成");
                                responseData.Data = 100;
                                responseData.Message = "计算完成";
                                PostWebMessageAsString(responseData);
                                status = 2;
                            }
                            else if (str.Contains(errorStatus))
                            {
                                //Console.WriteLine("中止，出现错误");
                                responseData.Success = false;
                                responseData.Message = "中止，出现错误";
                                PostWebMessageAsString(responseData);
                                status = 1;
                            }
                        }
                    }
                    process.WaitForExit();
                    process.Close();
                }
            }
            catch (Exception ex)
            {
                //string msg = ex.Message;
            }
            if (status == 0)
            {
                string errorMessage = "dyna程序意外退出，请重新启动";
                responseData.Success = false;
                responseData.Message = errorMessage;
                PostWebMessageAsString(responseData);
            }
        }

        /// <summary>
        /// 运行LS-DYNA
        /// </summary>
        /// <param name="folderPath">项目文件夹</param>
        /// <param name="dynaFilePath">Dyna程序路径</param>
        /// <param name="endtime">计算时长endtim(仿真参数设置)</param>
        public async Task RunDyna2(string folderPath, string dynaFilePath, double endtime)
        {
            CommandResponseData<int> responseData = new CommandResponseData<int>()
            {
                Success = true,
                CmdCategory = "HandleComputeResultByDyna",
                Code = 200,
                Data = 0,
            };

            int status = 0;
            int cpuCount = 4;
            int memoryCount = 2;
            this.Invoke(() =>
            {
                MessageForm form = new MessageForm();
                var dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    cpuCount = form.CpuCount;
                    memoryCount = form.MemoryCount;
                }
            });

            try
            {
                string kFilePath = Path.Combine(folderPath, "output.k");
                //string arguments = "i=" + kFilePath + " NCPU=" + 4.ToString() + " MEMORY=2g";
                //string arguments = $"i={kFilePath} NCPU=4 MEMORY=2g";
                string arguments = $"i={kFilePath} NCPU={cpuCount} MEMORY={memoryCount}m";

                //StringBuilder sb = new StringBuilder();
                ////REM 合并后的LS-DYNA求解批处理命令
                ////REM 原lsdynamsvar.bat中的环境变量设置
                //sb.AppendLine(@"set ""INTEL_CMP_REV=2019.5.281""");
                //sb.AppendLine(@"set ""INTEL_MKL_REV=2020.0.166""");
                //sb.AppendLine(@"set ""MS_MPI_REV=10.1.12498.18""");
                //sb.AppendLine(@"set ""MPI_ROOT=%AWP_ROOT231%\commonfiles\MPI\Microsoft\%MS_MPI_REV%\winx64""");
                //sb.AppendLine(@"set ""PATH=%MPI_ROOT%\Bin;%PATH%""");
                //sb.AppendLine(@"set ""PATH=%AWP_ROOT231%\tp\IntelMKL\%INTEL_MKL_REV%\winx64;%PATH%""");
                //sb.AppendLine(@"set ""PATH=%AWP_ROOT231%\tp\IntelCompiler\%INTEL_CMP_REV%\winx64;%PATH%""");
                //sb.AppendLine(@"");
                ////REM 执行LS-DYNA并行求解
                ////sb.AppendLine(@"mpiexec -np 8 -aa -a ""D:\Program Files\ANSYS Inc\v231\ansys\bin\winx64\lsdyna_mpp_sp_msmpi.exe"" ^");
                //sb.AppendLine(@$"mpiexec -np 8 -aa -a ""${dynaFilePath}"" ^");
                ////sb.AppendLine(@"i= D:\test20250921\output.k ^");
                ////sb.AppendLine(@"memory=200m");
                //sb.AppendLine(@$"i= {kFilePath} ^");
                //sb.AppendLine(@$"memory={memoryCount}m");
                //sb.AppendLine(@"");
                ////REM 完成后操作
                //sb.AppendLine(@"echo job finished > lsrunjobid33");
                //sb.AppendLine(@"pause");
                //arguments = sb.ToString();

                string errorStatus = "E r r o r   t e r m i n a t i o n";
                string successStatus = "N o r m a l    t e r m i n a t i o n";
                using (var process = new Process())
                {
                    //var exePath = "cmd.exe";
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = dynaFilePath, // 替换为你要执行的程序路径
                        //FileName = exePath,
                        UseShellExecute = false, // 不使用操作系统外壳来启动进程
                        RedirectStandardOutput = true, // 重定向标准输出流
                        RedirectStandardError = true,  // 重定向错误输出流（如果需要）
                        //CreateNoWindow = true, // 不显示控制台窗口
                        CreateNoWindow = false, // 显示控制台窗口
                        WindowStyle = ProcessWindowStyle.Normal,
                        Arguments = arguments,
                        WorkingDirectory = folderPath, // 设置工作目录
                        StandardOutputEncoding = System.Text.Encoding.UTF8, // 设置编码
                        StandardErrorEncoding = System.Text.Encoding.UTF8
                        // UseShellExecute = true // 不重定向，使用系统默认方式启动
                    };
                    // 允许触发Exited事件
                    process.EnableRaisingEvents = true;

                    // 设置事件处理程序来读取标准输出
                    process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                    {
                        //if (e.Data != null)
                        //    Console.WriteLine("输出: " + e.Data);  // 实时输出到控制台
                        if (string.IsNullOrEmpty(e.Data))
                            return;
                        var str = e.Data.Trim();
                        // 470 t 1.7977E-02 dt 3.80E-05 write d3plot file            08/21/25 23:47:15
                        if (str.Contains(" dt "))
                        {
                            //var res = str.Substring(6, 10);
                            //Console.WriteLine("时间：" + res);
                            var strs = str.Split(' ');
                            if (strs.Length > 2)
                            {
                                try
                                {
                                    //Console.WriteLine("时间：" + strs[2]);
                                    // 实时输出的时间
                                    var time = Convert.ToDouble(strs[2]);
                                    var result = (time / endtime) * 100;
                                    //responseData.Data = (int)result;
                                    responseData.Data = (int)Math.Ceiling(result);
                                    PostWebMessageAsString(responseData);
                                }
                                catch { }
                            }
                        }
                        else if (str.Contains(successStatus))
                        {
                            // Console.WriteLine("完成");
                            responseData.Data = 100;
                            responseData.Message = "计算完成";
                            PostWebMessageAsString(responseData);
                            status = 2;
                        }
                        else if (str.Contains(errorStatus))
                        {
                            //Console.WriteLine("中止，出现错误");
                            responseData.Success = false;
                            responseData.Message = "中止，出现错误";
                            PostWebMessageAsString(responseData);
                            status = 1;
                        }
                    });

                    // 设置事件处理程序来读取错误输出
                    process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                    {
                        //if (e.Data != null)
                        //    Console.WriteLine("错误: " + e.Data);  // 实时错误输出到控制台
                        if (string.IsNullOrEmpty(e.Data))
                            return;
                        var str = e.Data.Trim();
                        string errorMessage = "中止，出现错误，" + str;
                        responseData.Success = false;
                        responseData.Message = errorMessage;
                        PostWebMessageAsString(responseData);
                        status = 1;
                    });

                    // 启动进程
                    process.Start();

                    // 启动异步读取
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // 等待进程结束
                    //process.WaitForExit();
                    await Task.Run(() => process.WaitForExitAsync());

                    // 手动释放资源，确保进程的正确清理
                    //process.Close();
                }
                ;
            }
            catch (Exception ex)
            {
                //string msg = ex.Message;
            }
            if (status == 0)
            {
                string errorMessage = "dyna程序意外退出，请重新启动";
                responseData.Success = false;
                responseData.Message = errorMessage;
                PostWebMessageAsString(responseData);
            }
        }

        /// <summary>
        /// 计算结果提取
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task HandleComputeResultExtract(ComputeResultStatusModel model)
        {
            CommandResponseData responseData = new CommandResponseData()
            {
                Success = true,
                CmdCategory = "HandleComputeResultExtract",
                Code = 200,
            };

            if (string.IsNullOrEmpty(model.FolderPath)) // Directory.Exists(model.FolderPath)
            {
                responseData.Success = false;
                responseData.Message = "项目没有选择文件夹";
                PostWebMessageAsString(responseData);
                return;
            }
            // 获取文件夹以"mes"开头的所有文件
            //var files = Directory.GetFiles(model.FolderPath).Where(x=>x.StartsWith("mes"));
            var files = Directory.GetFiles(model.FolderPath, "mes*");
            if (files is null || files.Length == 0)
            {
                responseData.Success = false;
                responseData.Message = "当前项目未计算或者未计算完成，请等待或者重新计算";
                PostWebMessageAsString(responseData);
                return;
            }
            foreach (var file in files)
            {
                // 读取文件的最后一行
                var lastLine = await GetLastLine(file);

                // 检查最后一行是否包含 "Normol"
                if (!(lastLine != null && lastLine.Contains("N o r m a l")))
                {
                    //Console.WriteLine($"文件: {Path.GetFileName(file)} 的最后一行包含 'Normol'");
                    responseData.Success = false;
                    responseData.Message = "当前项目未计算或者未计算完成，请等待或者重新计算";
                    PostWebMessageAsString(responseData);
                    return;
                }
            }
            responseData.Message = "计算结果完成";
            PostWebMessageAsString(responseData);

            // 计算结果提取
            //CallPrePostCommand();
            _ = CallPrePostCommand(model.ProjectId, model.FolderPath, model.PartCount);

            var isChange = CallPrePostCommand(model.ProjectId, model.FolderPath, model.PartCount, true);
            if (isChange)
            {
                await AddProjectImagesAsync(model.ProjectId);
            }

            CommandResponseData responseData2 = new CommandResponseData()
            {
                Success = true,
                CmdCategory = "HandleComputeResultExtract2",
                Code = 200,
                Message = "计算结果提取完成"
            };
            PostWebMessageAsString(responseData2);
        }

        // 获取文件的最后一行
        private async Task<string?> GetLastLine(string filePath)
        {
            try
            {
                var lines = await File.ReadAllLinesAsync(filePath, Encoding.UTF8);

                //var lines = File.ReadAllLines(filePath, Encoding.UTF8);
                var lastLine = lines?.LastOrDefault();

                //var lines = File.ReadLines(filePath);
                //string? lastLine = null;

                //foreach (var line in lines)
                //{
                //    lastLine = line; // 每次覆盖 lastLine，最终留下最后一行
                //}

                return lastLine;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"无法读取文件 {filePath}: {ex.Message}");
                return null;
            }
        }

        private void CallPrePostCommand()
        {
            var handles = LsPrePost.FindChildWindows(_appHandle);
            if (handles.Count > 0)
            {
                // 获取句柄号
                //string handleStr0 = handles.Last();
                string handleStr = handles.Last();
            }
        }

        private bool CallPrePostCommand(int projectId, string folderPath, int partCount, bool isExecute = false)
        {
            bool isChange = false;
            //this.Invoke用处不大
            //this.Invoke(() =>
            //{
            var handles = LsPrePost.FindChildWindows(_appHandle);

            if (handles.Count > 0)
            {
                // 获取句柄号
                string handleStr = handles.Last();
                StringBuilder sb = new StringBuilder();

                string d3plotPath = Path.Combine(folderPath, "d3plot");
                string binoutPath = Path.Combine(folderPath, "binout0000");
                string imageOnePath = Path.Combine(folderPath, "image_001.png");
                string imageTwoPath = Path.Combine(folderPath, "image_002.png");
                string imageThreePath = Path.Combine(folderPath, "image_003.png");
                string imageZeroPath = Path.Combine(folderPath, "image_000.png");

                #region 测试

                // var strs = $@" open d3plot ""{d3plotPath}""";
                // WindowManager_WarheadSimlink.SendTextToWindow(handleStr, strs);
                // return false;
                #endregion 测试
                List<int> parts = new List<int>();
                for (int i = 1; i <= partCount; i++)
                {
                    parts.Add(1000 + i);
                }
                string partString = string.Join(' ', parts);

                // 0.加载D3plot
                sb.AppendLine($@" open d3plot ""{d3plotPath}""");
                // 1.绘制防护结构各part内能曲线
                // 导入数据文件
                sb.AppendLine("binaski init;");
                sb.AppendLine($@"binaski load ""{binoutPath}""");
                // 读取数据文件
                sb.AppendLine($@"binaski fileswitch ""{binoutPath}"";");
                // 打开matsum
                sb.AppendLine(@"binaski loadblock /matsum");
                // 打开复合板和蜂窝板内能变化曲线-上下复合板和蜂窝PID 1001开始 -当前项目中复合板和蜂窝板的part号，
                //sb.AppendLine($@"binaski plot ""{binoutPath}"" matsum 45 1 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 41 42 43 44 45 46 47 51 52 53 54 55 56 70 internal_energy ;");
                sb.AppendLine($@"binaski plot ""{binoutPath}"" matsum 45 1 {partString} internal_energy ;");
                // 保存曲线图片
                //sb.AppendLine($@"print png ""{imageOnePath}"" opaque enlisted");
                //sb.AppendLine(@"""PlotWindow-1"" ");
                sb.AppendLine($@"print png ""{imageOnePath}"" opaque enlisted ""PlotWindow-1""");
                // 关闭绘图窗口
                sb.AppendLine("closewin 1");
                // 关闭数据选项面板
                sb.AppendLine("genselect label off");

                // 2.绘制弹丸速度曲线
                sb.AppendLine("genselect target part");
                // 83 改成对应子弹的pid 先写死 83
                sb.AppendLine("genselect part add part 83/0");
                sb.AppendLine("mtime 10");
                // 保存速度曲线
                //sb.AppendLine($@"print png ""{imageTwoPath}"" opaque enlisted ");
                //sb.AppendLine(@"""PlotWindow-1""");
                sb.AppendLine($@"print png ""{imageTwoPath}"" opaque enlisted ""PlotWindow-1""");
                sb.AppendLine("closewin 1");

                // 3.绘制防护结构整体内能曲线

                sb.AppendLine("binaski init;");
                // 导入数据文件
                sb.AppendLine($@"binaski load ""{binoutPath}""");
                sb.AppendLine($@"binaski fileswitch ""{binoutPath}"";");
                sb.AppendLine("binaski loadblock /matsum");
                // 当前项目中复合板和蜂窝板的part号
                //sb.AppendLine($@"binaski plot ""{binoutPath}"" matsum 45 1 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 41 42 43 44 45 46 47 51 52 53 54 55 56 70 sum internal_energy ;");
                sb.AppendLine($@"binaski plot ""{binoutPath}"" matsum 45 1 {partString} sum internal_energy ;");
                // 保存速度曲线
                //sb.AppendLine($@"print png ""{imageThreePath}"" opaque enlisted");
                //sb.AppendLine(@"""PlotWindow-1""");
                sb.AppendLine($@"print png ""{imageThreePath}"" opaque enlisted ""PlotWindow-1""");
                sb.AppendLine("closewin 1");
                sb.AppendLine("genselect label off");

                // 4.结构变形云图

                //sb.AppendLine("state -1;");
                //sb.AppendLine("fringe 19");
                //sb.AppendLine("pfringe");

                sb.AppendLine("state -1;");
                sb.AppendLine("isometric x");
                sb.AppendLine("rz 180");
                sb.AppendLine("-M X81");
                sb.AppendLine("-M X82");
                sb.AppendLine("-M X83");
                sb.AppendLine("fringe 19");
                sb.AppendLine("pfringe");
                //
                sb.AppendLine($@"print png ""{imageZeroPath}"" opaque enlisted ""OGL1x1""");

                // 把生成的图片保存到指定路径
                // 年月日/id
                // 发送文本到指定窗口
                //string absolutePath = folderPath;
                //absolutePath = Path.Combine(absolutePath, "model.k");
                //string str = $" open keyword \"{absolutePath}\"";
                string str = sb.ToString();
                WindowManager_WarheadSimlink.SendTextToWindow(handleStr, str);

                if (!isExecute)
                {
                    return false;
                }

                string rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");

                string basePath = Path.Combine(rootPath, projectId.ToString());

                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }

                string imageWebPath1 = Path.Combine(basePath, "image_001.png");
                string imageWebPath2 = Path.Combine(basePath, "image_002.png");
                string imageWebPath3 = Path.Combine(basePath, "image_003.png");
                string imageWebPath4 = Path.Combine(basePath, "image_000.png");

                if (File.Exists(imageOnePath))
                {
                    File.Copy(imageOnePath, imageWebPath1, true);
                    isChange = true;
                }
                if (File.Exists(imageTwoPath))
                {
                    File.Copy(imageTwoPath, imageWebPath2, true);
                    isChange = true;
                }
                if (File.Exists(imageThreePath))
                {
                    File.Copy(imageThreePath, imageWebPath3, true);
                    isChange = true;
                }
                if (File.Exists(imageZeroPath))
                {
                    File.Copy(imageZeroPath, imageWebPath4, true);
                    isChange = true;
                }
            }
            //});
            return isChange;
        }

        private async Task AddProjectImagesAsync(int projectId)
        {
            try
            {
                string apiHost = _configuration.GetValue<string>("ApiHost") ?? "http://localhost:3000";
                string webUrl = _configuration.GetValue<string>("WebHost") ?? "http://127.0.0.1:5295";
                var data = new
                {
                    Id = projectId,
                    ImageOnePath = $"{apiHost}/{projectId}/image_001.png",
                    ImageTwoPath = $"{apiHost}/{projectId}/image_002.png",
                    ImageThreePath = $"{apiHost}/{projectId}/image_003.png",
                    ImageFourPath = $"{apiHost}/{projectId}/image_000.png",
                };

                using var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(webUrl);
                var json = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(json, Encoding.UTF8);
                var response = await httpClient.PostAsync("api/Project/addImages", httpContent);
                if (response.IsSuccessStatusCode)
                {
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        #endregion prvite methods
    }
}