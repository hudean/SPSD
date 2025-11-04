using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using SPSD.WinFormsApp.Model;

namespace SPSD.WinFormsApp
{
    public class AnsysModel
    {
        /// <summary>
        /// 将TG文件生成K文件
        /// trueGrid不能设置工作路径，不然默认会在这个工作路径上生成k文件，而不是folderPath上
        /// </summary>
        /// <param name="tgFile"></param>
        /// <param name="folderPath"></param>
        /// <param name="trueGridPath"></param>
        public static void GenerateKFile(string tgFile, string folderPath, string trueGridPath)
        {
            string absolutePath = Path.GetFullPath(folderPath);
            // string argIn = WorkingSpace + tgFile;
            string argument = "-i=" + tgFile + " -o=trugrdo -g=nogui";

            try
            {
                Process tg = new Process();
                tg.StartInfo.FileName = trueGridPath;                              //对大小写不敏感
                tg.StartInfo.Arguments = argument;
                tg.StartInfo.UseShellExecute = false;
                tg.StartInfo.CreateNoWindow = true;
                tg.StartInfo.RedirectStandardInput = true;                          //输入重定向
                tg.StartInfo.RedirectStandardOutput = true;                     //输出重定向
                tg.StartInfo.WorkingDirectory = absolutePath;
                tg.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                tg.Start();

                string[] tgArg = new string[] { "exit" };        //"merge", "co fc 2", "stp 0.01", "lsdyna keyword", "write"
                StreamWriter sw = tg.StandardInput;
                for (int i = 0; i < tgArg.Length; i++)
                    sw.WriteLine(tgArg[i]);
                sw.Close();

                StreamReader sr = tg.StandardOutput;            // NORMAL TERMINATION
                string outStr = sr.ReadToEnd();
                sr.Close();

                try
                {
                    outStr.IndexOf("NORMAL TERMINATION");           //正常结束
                    //System.Windows.Forms.MessageBox.Show($"成功生成{tgFile}!");
                }
                catch
                {
                    //System.Windows.Forms.MessageBox.Show($"生成{tgFile}失败!");
                }
                finally
                {
                    tg.Close();
                }
            }
            catch (System.ComponentModel.Win32Exception w)
            {
                Console.WriteLine(w.Message);
                Console.WriteLine(w.ErrorCode.ToString());
                Console.WriteLine(w.NativeErrorCode.ToString());
                Console.WriteLine(w.StackTrace);
                Console.WriteLine(w.Source);
                Exception e = w.GetBaseException();
                Console.WriteLine(e.Message);
            }
        }

        public static void GenerateKFile2(string folderPath)
        {
            string kFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            string kFilePrefixPath = Path.Combine(kFilePath, "kFilePrefix.k");
            string destinationPath = Path.Combine(folderPath, "fwb.k");
            File.Copy(kFilePrefixPath, destinationPath, true);
            //using var fileStream =  File.Open(destinationPath,FileMode.Open,FileAccess.Write);

            var list = new List<string>();

            list.Add("*SET_NODE_LIST");
            list.Add("$#     sid       da1       da2       da3       da4    solver");
            list.Add("         1       0.0       0.0       0.0       0.0MECH");
            list.Add("$#    nid1      nid2      nid3      nid4      nid5      nid6      nid7      nid8");
            // 8列 * 10个字符

            list.Add("*END");
            File.AppendAllLines(destinationPath, list);
        }

        /// <summary>
        /// 运行LS-DYNA的971版本
        /// </summary>
        /// <param name="kFile">output.k全路径</param>
        /// <param name="dynaFilePath">Dyna程序路径 ansysPath</param>
        /// <param name="folderPath">项目文件夹</param>
        /// <param name="cpuNum">默认是4个</param>
        /// <param name="memorySize">2G</param>
        public static void RunLs971ByDyna(string kFile, string dynaFilePath, string folderPath, int cpuNum, string memorySize)
        {
            try
            {
                string arguments = "i=" + kFile + " NCPU=" + cpuNum.ToString() + " MEMORY=" + memorySize;
                //string pathStr = System.IO.Directory.GetCurrentDirectory();            //D:\\Vs Project\\program\\Ansys\\bin\\Debug

                Process ansys = new Process();
                ansys.StartInfo.FileName = "\"" + dynaFilePath + "\"";
                ansys.StartInfo.UseShellExecute = false;
                ansys.StartInfo.CreateNoWindow = false;
                ansys.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                ansys.StartInfo.WorkingDirectory = folderPath;
                ansys.StartInfo.Arguments = arguments;
                ansys.Start();
                ansys.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 运行LS-DYNA
        /// </summary>
        /// <param name="folderPath">项目文件夹</param>
        /// <param name="dynaFilePath">Dyna程序路径</param>
        /// <param name="endtime">计算时长endtim(仿真参数设置)</param>
        public static void RunDyna(string folderPath, string dynaFilePath,double endtime)
        {
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
                };

                var process = Process.Start(s);
                if (process != null)
                {
                    var sr = process.StandardOutput;
                    while (!sr.EndOfStream)
                    {
                        var str = sr.ReadLine()?.Trim();
                        //Console.WriteLine(str);
                        if (!string.IsNullOrEmpty(str))
                        {
                            // 470 t 1.7977E-02 dt 3.80E-05 write d3plot file            08/21/25 23:47:15
                            if (str.Contains("dt"))
                            {
                                //var res = str.Substring(6, 10);
                                //Console.WriteLine("时间：" + res);
                                var strs = str.Split(' ');
                                if (strs.Length > 2)
                                {
                                    //Console.WriteLine("时间：" + strs[2]);
                                    // 实时输出的时间
                                    var time = Convert.ToDouble(strs[2]);
                                    var result = time / endtime;
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
            catch (Exception ex)
            {
                //string msg = ex.Message;
                string errorMessage = "dyna程序意外退出，请重新启动";
            }
            
        }

        #region 抗弹模块

        public static async Task GenerateKFileByComputeResult(ComputeResultModel request)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("");
            string folderPath = request.FolderPath;
            string kFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            string inputFilePath = Path.Combine(kFilePath, "FileTemplate.k");
            string outputFilePath = Path.Combine(folderPath, "output.k");
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath); // 删除已存在的输出文件
            }
            int lintCount = 0;
            //File.Copy(inputFilePath, outputFilePath , true);
            // 创建 StreamReader 和 StreamWriter
            //using (StreamReader reader = new StreamReader(inputFilePath, Encoding.UTF8))
            //using (StreamWriter writer = new StreamWriter(outputFilePath, false, Encoding.UTF8))
            using (StreamReader reader = new StreamReader(inputFilePath))
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                string? line;

                // 逐行读取文件内容
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lintCount++;
                    switch (lintCount)
                    {
                        case 7:
                            // 滑动界面接触刚度因子
                            {
                                string slsfac = FormatString(request.slsfac);
                                line = line.Replace("{{slsfac}}", slsfac);
                            }
                            break;

                        case 26:
                            // 计算时间
                            {
                                string endtim = FormatString(request.endtim);
                                line = line.Replace("{{endtim}}", endtim);
                            }
                            break;

                        case 29:
                            // 时间步长
                            {
                                string tssfac = FormatString(request.tssfac);
                                line = line.Replace("{{tssfac}}", tssfac);
                            }
                            break;

                        case 34:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_001}}", dt);
                            }
                            break;

                        case 37:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_002}}", dt);
                            }
                            break;

                        case 40:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_003}}", dt);
                            }
                            break;

                        case 43:
                            // 输出间隔
                            {
                                string dt = FormatString(request.D3plots);
                                line = line.Replace("{{dt_004}}", dt);
                            }
                            break;

                        case 48:
                            // 输出间隔
                            {
                                string dt = FormatString(request.D3plots);
                                line = line.Replace("{{dt_005}}", dt);
                            }
                            break;

                        //case 73:
                        //    //弹体编号
                        //    {
                        //        //暂时这样-弹体编号还没做后续在加
                        //        string val = "         2       0.0       0.0       0.0       0.0MECH";
                        //        line = line.Replace("{{WarbodyCode}}", val);
                        //    }
                        //    break;

                        //case 75:
                        //    //上层复合板编号：1-n，n为上层复合板层数，n为5 ：1-5
                        //    //下层复合板：n+1-n+m；m为下层复合板层数 m=10，下层编号6-15；
                        //    //蜂窝编号为：n+m+1  16，下面红色部分需要根据实际编号改为下面：
                        //    {
                        //        int length = request.TopTier + request.BottomTier +1;
                        //        StringBuilder sb = new StringBuilder();
                        //        for (int i = 1; i <= length; i++)
                        //        {
                        //            sb.Append(FormatString(i));
                        //            if (i % 8 == 0 && i != length)
                        //            {
                        //                sb.AppendLine();
                        //            }
                        //        }
                        //        string val = sb.ToString();
                        //        line = line.Replace("{{CompositeBoard}}", val);
                        //    }
                        //    break;

                        //case 95:
                        //    //复合板和蜂窝板编号
                        //    {
                        //        string val = "         1         2         3         4         5         6         7         8";
                        //        line = line.Replace("{{CompositeBoardAndBeehiveBoard}}", val);
                        //    }
                        //    break;

                        //case 100:
                        //    //弹体结构
                        //    {
                        //        string val = "        81        82        83         0         0         0         0         0";
                        //        line = line.Replace("{{WarbodyStruct}}", val);
                        //    }
                        //    break;
                        case 76:
                            {
                                string val = SetPartListValue(request.TopTier, request.BottomTier);
                                line = line.Replace("{{SET_PART_LIST}}", val);
                            }
                            break;
                        //case 102:
                        case 142:
                            {
                                string val = SetBoardContactsValue(request.TopTier, request.BottomTier);
                                line = line.Replace("{{BoardContacts}}", val);
                            }
                            break;

                        case 144:
                            {
                                string val = SetMaterialItemsValue(request.MaterialItems);
                                line = line.Replace("{{MaterialItems}}", val);
                            }
                            break;

                        case 155:
                            {
                                string val = SetPartsValue(request);
                                line = line.Replace("{{Parts}}", val);
                            }
                            break;

                        case 165:
                            {
                                var res = request.Speed / 10000;
                                string val = FormatString(-res);
                                line = line.Replace("{{vz_001}}", val);
                            }
                            break;
                        //case 9:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 10:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 11:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 12:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 13:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 14:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 16:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;

                        default:
                            break;
                    }

                    writer.WriteLine(line);
                }
            }
        }

        private static string SetMaterialItemsValue(List<MaterialItem> materialItems)
        {
            StringBuilder sb = new();
            if (materialItems?.Any() ?? false)
            {
                foreach (var item in materialItems)
                {
                    // 本构方程
                    if (!string.IsNullOrEmpty(item.StrengthModelName))
                    {
                        sb.AppendLine($"*{item.StrengthModelName.Replace("_TITLE", "")}");
                        var parameters = item.StrengthModelParameter.Split(',');
                        int length = parameters.Length + 1;
                        var vals = item.StrengthModelValue.Split(',');
                        var rows = (int)Math.Ceiling(length / 8.0d);
                        var columns = length % 8;
                        for (int i = 0; i < rows; i++)
                        {
                            int index = i * 8 - 1;
                            //if (i != 0)
                            //{
                            //    index = i * 8 - 2; // 每行8个参数，第一行从0开始
                            //}
                            string rowParameter = "";
                            string rowVal = "";

                            if (i == (rows - 1) && columns != 0)
                            {
                                List<string> rowParameterList = new();
                                List<string> rowValList = new();
                                for (int j = 0; j < columns; j++)
                                {
                                    index = i * 8 + j;
                                    if (index == 0)
                                    {
                                        rowParameterList.Add("$#     mid");
                                        rowValList.Add(FormatString(item.Id));
                                    }
                                    else
                                    {
                                        rowParameterList.Add(FormatString(parameters[index - 1]));
                                        rowValList.Add(FormatString(vals[index - 1]));
                                    }
                                }

                                rowParameter = string.Join("", rowParameterList);
                                rowVal = string.Join("", rowValList);
                            }
                            else
                            {
                                string prefixParameter = i == 0 ? "$#     mid" : FormatString(parameters[index]);
                                string prefixVal = i == 0 ? FormatString(item.Id) : FormatString(vals[index]);
                                rowParameter = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", prefixParameter, FormatString(parameters[index + 1]), FormatString(parameters[index + 2]), FormatString(parameters[index + 3]), FormatString(parameters[index + 4]), FormatString(parameters[index + 5]), FormatString(parameters[index + 6]), FormatString(parameters[index + 7]));
                                rowVal = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", prefixVal, FormatString(vals[index + 1]), FormatString(vals[index + 2]), FormatString(vals[index + 3]), FormatString(vals[index + 4]), FormatString(vals[index + 5]), FormatString(vals[index + 6]), FormatString(vals[index + 7]));
                            }

                            //int index = i * 8;
                            //string rowParameter = "";
                            //string rowVal = "";
                            //if (i == (rows - 1) && columns != 0)
                            //{
                            //    rowParameter = string.Join("", Enumerable.Range(0, columns).Select(i2 => FormatString(parameters[index + i2])));

                            //    rowVal = string.Join("", Enumerable.Range(0, columns).Select(i2 => FormatString(vals[index + i2])));
                            //}
                            //else
                            //{
                            //    //rowParameter = $"{FormatString(parameters[index])}{FormatString(parameters[index + 1])}{FormatString(parameters[index + 2])}{FormatString(parameters[index + 3])}{FormatString(parameters[index + 4])}{FormatString(parameters[index + 5])}{FormatString(parameters[index + 6])}{FormatString(parameters[index + 7])}";
                            //    rowParameter = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", FormatString(parameters[index]), FormatString(parameters[index + 1]), FormatString(parameters[index + 2]), FormatString(parameters[index + 3]), FormatString(parameters[index + 4]), FormatString(parameters[index + 5]), FormatString(parameters[index + 6]), FormatString(parameters[index + 7]));
                            //    rowVal = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", FormatString(vals[index]), FormatString(vals[index + 1]), FormatString(vals[index + 2]), FormatString(vals[index + 3]), FormatString(vals[index + 4]), FormatString(vals[index + 5]), FormatString(vals[index + 6]), FormatString(vals[index + 7]));
                            //}

                            rowParameter = string.Concat("$#", rowParameter.AsSpan(2));
                            sb.AppendLine(rowParameter);
                            sb.AppendLine(rowVal);
                        }
                    }

                    // 状态方程
                    if (!string.IsNullOrEmpty(item.EOSName))
                    {
                        sb.AppendLine($"*{item.EOSName.Replace("_TITLE", "")}");
                        var parameters = item.EOSParameter.Split(',');
                        int length = parameters.Length + 1;
                        var vals = item.EOSValue.Split(',');
                        var rows = (int)Math.Ceiling(length / 8.0d);
                        var columns = length % 8;
                        for (int i = 0; i < rows; i++)
                        {
                            //int index = i * 8;
                            //string rowParameter = "";
                            //string rowVal = "";

                            //if (i == (rows - 1) && columns != 0)
                            //{
                            //    rowParameter = string.Join("", Enumerable.Range(0, columns).Select(i2 => {
                            //        var j = index + i2;
                            //        string str = j == 0 ? "$#   eosid" : FormatString(parameters[j-1]);
                            //        return str;
                            //    }));
                            //    rowVal = string.Join("", Enumerable.Range(0, columns).Select(i2 => {
                            //        var j = index + i2;
                            //        string str = j == 0 ? FormatString(item.Id) : FormatString(vals[j - 1]);
                            //        return str;
                            //    }));
                            //}
                            //else
                            //{
                            //    string prefixParameter = index == 0 ? "$#   eosid" : FormatString(parameters[index - 1]);
                            //    string prefixVal = index == 0 ? FormatString(item.Id) : FormatString(vals[index - 1]);
                            //    rowParameter = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", prefixParameter, FormatString(parameters[index]), FormatString(parameters[index + 1]), FormatString(parameters[index + 2]), FormatString(parameters[index + 3]), FormatString(parameters[index + 4]), FormatString(parameters[index + 5]), FormatString(parameters[index + 6]));
                            //    rowVal = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", prefixVal, FormatString(vals[index]), FormatString(vals[index + 1]), FormatString(vals[index + 2]), FormatString(vals[index + 3]), FormatString(vals[index + 4]), FormatString(vals[index + 5]), FormatString(vals[index + 6]));
                            //}
                            int index = i * 8 - 1;
                            string rowParameter = "";
                            string rowVal = "";

                            if (i == (rows - 1) && columns != 0)
                            {
                                List<string> rowParameterList = new();
                                List<string> rowValList = new();
                                for (int j = 0; j < columns; j++)
                                {
                                    index = i * 8 + j;
                                    if (index == 0)
                                    {
                                        rowParameterList.Add("$#   eosid");
                                        rowValList.Add(FormatString(item.Id));
                                    }
                                    else
                                    {
                                        rowParameterList.Add(FormatString(parameters[index - 1]));
                                        rowValList.Add(FormatString(vals[index - 1]));
                                    }
                                }
                                rowParameter = string.Join("", rowParameterList);
                                rowVal = string.Join("", rowValList);
                            }
                            else
                            {
                                string prefixParameter = i == 0 ? "$#   eosid" : FormatString(parameters[index]);
                                string prefixVal = i == 0 ? FormatString(item.Id) : FormatString(vals[index]);
                                rowParameter = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", prefixParameter, FormatString(parameters[index + 1]), FormatString(parameters[index + 2]), FormatString(parameters[index + 3]), FormatString(parameters[index + 4]), FormatString(parameters[index + 5]), FormatString(parameters[index + 6]), FormatString(parameters[index + 7]));
                                rowVal = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", prefixVal, FormatString(vals[index + 1]), FormatString(vals[index + 2]), FormatString(vals[index + 3]), FormatString(vals[index + 4]), FormatString(vals[index + 5]), FormatString(vals[index + 6]), FormatString(vals[index + 7]));
                            }
                            rowParameter = string.Concat("$#", rowParameter.AsSpan(2));
                            sb.AppendLine(rowParameter);
                            sb.AppendLine(rowVal);
                        }
                    }
                    // 失效模型
                    if (!string.IsNullOrEmpty(item.FailureModelName))
                    {
                        sb.AppendLine($"*{item.FailureModelName.Replace("_TITLE", "")}");
                        var parameters = item.FailureModelParameter.Split(',');
                        int length = parameters.Length + 1;
                        var vals = item.FailureModelValue.Split(',');
                        var rows = (int)Math.Ceiling(length / 8.0d);
                        var columns = length % 8;
                        for (int i = 0; i < rows; i++)
                        {
                            int index = i * 8 - 1;
                            string rowParameter = "";
                            string rowVal = "";

                            if (i == (rows - 1) && columns != 0)
                            {
                                List<string> rowParameterList = new();
                                List<string> rowValList = new();
                                for (int j = 0; j < columns; j++)
                                {
                                    index = i * 8 + j;
                                    if (index == 0)
                                    {
                                        rowParameterList.Add("$#     mid");
                                        rowValList.Add(FormatString(item.Id));
                                    }
                                    else
                                    {
                                        rowParameterList.Add(FormatString(parameters[index - 1]));
                                        rowValList.Add(FormatString(vals[index - 1]));
                                    }
                                }
                                rowParameter = string.Join("", rowParameterList);
                                rowVal = string.Join("", rowValList);
                            }
                            else
                            {
                                string prefixParameter = i == 0 ? "$#     mid" : FormatString(parameters[index]);
                                string prefixVal = i == 0 ? FormatString(item.Id) : FormatString(vals[index]);
                                rowParameter = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", prefixParameter, FormatString(parameters[index + 1]), FormatString(parameters[index + 2]), FormatString(parameters[index + 3]), FormatString(parameters[index + 4]), FormatString(parameters[index + 5]), FormatString(parameters[index + 6]), FormatString(parameters[index + 7]));
                                rowVal = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", prefixVal, FormatString(vals[index + 1]), FormatString(vals[index + 2]), FormatString(vals[index + 3]), FormatString(vals[index + 4]), FormatString(vals[index + 5]), FormatString(vals[index + 6]), FormatString(vals[index + 7]));
                            }
                            //rowParameter = "$#" + rowParameter.Substring(2);
                            rowParameter = string.Concat("$#", rowParameter.AsSpan(2));
                            sb.AppendLine(rowParameter);
                            sb.AppendLine(rowVal);
                        }
                    }
                }
            }
            sb.Append("$#");
            return sb.ToString();
        }

        private static string SetPartsValue(ComputeResultModel request)
        {
            //$#  遍历 ：81、82、83, 1001~1000+n+m+1 个PART
            //$#  PID：就是item
            //$#  secid：1
            //$#  mid：MaterialItemId
            //$#  eosid：0/MaterialItemId
            //int topTier, int bottomTier, int materialItemId, string eosName
            int offset = 1000;
            int length = request.TopTier + request.BottomTier + 1;
            StringBuilder sb = new();
            int[] warbodyPids = new int[3] { 81, 82, 83 };
            int[] warbodyMaterialItemIds = new int[3] { request.OneMaterialItemId, request.TwoMaterialItemId, request.ThreeMaterialItemId };
            for (int i = 0; i < warbodyPids.Length; i++)
            {
                string pid = FormatString(warbodyPids[i]);
                string mid = FormatString(warbodyMaterialItemIds[i]);
                string eosid = FormatString(0); // 弹体没有状态方程
                var eosName = request.MaterialItems.Where(m => m.Id == warbodyMaterialItemIds[i]).FirstOrDefault()?.EOSName;
                if (!string.IsNullOrEmpty(eosName))
                {
                    eosid = mid;
                }

                sb.AppendLine("*PART");
                sb.AppendLine("$#                                                                         title");
                sb.AppendLine("PE1");
                sb.AppendLine("$#     pid     secid       mid     eosid      hgid      grav    adpopt      tmid");
                sb.AppendLine($"{pid}         1{mid}{eosid}         0         0         0         0");
            }

            // 3 个 MaterialItemId
            for (int i = 1; i <= length; i++)
            {
                int materialItemId = 0;
                string eosName = string.Empty;
                if (i <= request.TopTier)
                {
                    materialItemId = request.TopMaterialItemId;
                    eosName = request.TopMaterialItemEosName ?? "";
                }
                else if (i > request.TopTier && i < length)
                {
                    materialItemId = request.BottomMaterialItemId;
                    eosName = request.BottomMaterialItemEosName ?? "";
                }
                else
                {
                    materialItemId = request.FwcellMaterialItemId;
                    eosName = request.FwcellMaterialItemEosName ?? "";
                }
                string pid = FormatString(offset + i);
                string mid = FormatString(materialItemId);
                string eosid = string.IsNullOrEmpty(eosName) ? FormatString(0) : mid;
                sb.AppendLine("*PART");
                sb.AppendLine("$#                                                                         title");
                sb.AppendLine("PE1");
                sb.AppendLine("$#     pid     secid       mid     eosid      hgid      grav    adpopt      tmid");
                sb.AppendLine($"{pid}         1{mid}{eosid}         0         0         0         0");
            }
            sb.Append("$#");
            return sb.ToString();
        }

        private static string SetPartListValue(int topTier, int bottomTier)
        {
            // $设置复合板相邻板之间的接触
            // sid = 1 只有弹体，PID 分别为81、82、83
            // 2 复合板和蜂窝 PID 分别为 1001~1000 + n + m + 1
            // 3 弹体、复合板和蜂窝   81、82、83, 1001~1000 + n + m + 1

            // 偏移量+1000
            int offset = 1000;
            int length = topTier + bottomTier + 1;
            StringBuilder sb = new();
            sb.AppendLine("*SET_PART_LIST");
            sb.AppendLine("$#     sid       da1       da2       da3       da4    solver");
            sb.AppendLine("         1       0.0       0.0       0.0       0.0MECH");
            sb.AppendLine("$#    pid1      pid2      pid3      pid4      pid5      pid6      pid7      pid8");
            sb.AppendLine("        81        82        83");

            sb.AppendLine("*SET_PART_LIST");
            sb.AppendLine("$#     sid       da1       da2       da3       da4    solver");
            sb.AppendLine("         2       0.0       0.0       0.0       0.0MECH");
            sb.AppendLine("$#    pid1      pid2      pid3      pid4      pid5      pid6      pid7      pid8");
            for (int i = 1; i <= length; i++)
            {
                sb.Append(FormatString(offset + i));
                if (i % 8 == 0 && i != length)
                {
                    sb.AppendLine();
                }
                else if (i % 8 != 0 && i == length)
                {
                    sb.AppendLine("");
                }
            }

            sb.AppendLine("*SET_PART_LIST");
            sb.AppendLine("$#     sid       da1       da2       da3       da4    solver");
            sb.AppendLine("         3       0.0       0.0       0.0       0.0MECH");
            sb.AppendLine("$#    pid1      pid2      pid3      pid4      pid5      pid6      pid7      pid8");
            int initialValue = 3;
            //length += initialValue;
            sb.Append(FormatString(81));
            sb.Append(FormatString(82));
            sb.Append(FormatString(83));
            for (int i = 1; i <= length; i++)
            {
                sb.Append(FormatString(offset + i));
                int column = i + initialValue;
                if (column % 8 == 0 && i != length)
                {
                    sb.AppendLine();
                }
            }
            //sb.AppendLine("");
            return sb.ToString();
        }

        /// <summary>
        /// 设置复合板相邻板之间的接触
        /// </summary>
        /// <param name="n">上复合板层数</param>
        /// <param name="m">下复合板层数</param>
        /// <returns></returns>
        private static string SetBoardContactsValue(int n, int m)
        {
            //上复合板层数： n
            //cid = 1 ~n - 1
            //surfa = 1 ~n - 1
            //surfb = 2 ~n

            //下复合板层数：  m
            //cid = n+1~n + m - 1
            //surfa = n+1~n + m - 1
            //surfb = n+1 + 1 ~n + m

            StringBuilder sb = new();
            // 偏移量+1000
            int offset = 1000;
            int length = n + m;
            for (int i = 1; i < length; i++)
            {
                if (i == n)
                {
                    continue;
                }
                string cid = FormatString(i + offset);
                string surfa = FormatString(i + offset);
                string surfb = FormatString(i + 1 + offset);
                sb.AppendLine("*CONTACT_AUTOMATIC_SURFACE_TO_SURFACE_TIEBREAK_ID");
                sb.AppendLine("$#     cid                                                                 title");
                sb.AppendLine(cid);
                sb.AppendLine("$#   surfa     surfb  surfatyp  surfbtyp   saboxid   sbboxid      sapr      sbpr");
                //sb.AppendLine("         1         2         3         3         0         0         0         0");
                sb.AppendLine($"{surfa}{surfb}         3         3         0         0         0         0");
                sb.AppendLine("$#      fs        fd        dc        vc       vdc    penchk        bt        dt");
                sb.AppendLine("      0.15      0.15       0.0       0.0       0.0         0       0.01.00000E20");
                sb.AppendLine("$#    sfsa      sfsb      sast      sbst     sfsat     sfsbt       fsf       vsf");
                sb.AppendLine("       5.0       1.0       0.0       0.0       1.0       1.0       1.0       1.0");
                sb.AppendLine("$#  option      nfls      sfls     param    eraten    erates     ct2cn        cn");
                sb.AppendLine("         26.28000E-52.29000E-5     -1.465.60000E-69.20000E-6      -1.0      10.0");
                sb.AppendLine("$#    soft    sofscl    lcidab    maxpar     sbopt     depth     bsort    frcfrq");
                sb.AppendLine("         1       0.1         0     1.025         2         2         0         1");
                sb.AppendLine("$#  penmax    thkopt    shlthk     snlog      isym     i2d3d    sldthk    sldstf");
                sb.AppendLine("       0.0         0         0         0         0         0       0.0       0.0");
            }
            sb.Append("$#");
            string val = sb.ToString();
            return val;
        }

        public static async Task GenerateKFileByComputeResultOld(ComputeResultModel request)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("");
            string folderPath = request.FolderPath;
            string kFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            string inputFilePath = Path.Combine(kFilePath, "FileTemplate.k");
            string outputFilePath = Path.Combine(folderPath, "output.k");
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath); // 删除已存在的输出文件
            }
            int lintCount = 0;
            //File.Copy(inputFilePath, outputFilePath , true);
            // 创建 StreamReader 和 StreamWriter
            //using (StreamReader reader = new StreamReader(inputFilePath, Encoding.UTF8))
            //using (StreamWriter writer = new StreamWriter(outputFilePath, false, Encoding.UTF8))
            using (StreamReader reader = new StreamReader(inputFilePath))
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                string? line;

                // 逐行读取文件内容
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lintCount++;
                    // 替换标识符
                    //line = line.Replace("{{name}}", "张博");
                    //line = line.Replace("{{place}}", "北京");
                    var isContinue = false;
                    switch (lintCount)
                    {
                        case 15:
                            // 滑动界面接触刚度因子
                            {
                                string slsfac = FormatString(request.slsfac);
                                line = line.Replace("{{slsfac}}", slsfac);
                            }
                            break;

                        case 34:
                            // 计算时间
                            {
                                string endtim = FormatString(request.endtim);
                                line = line.Replace("{{endtim}}", endtim);
                            }
                            break;

                        case 37:
                            // 时间步长
                            {
                                string tssfac = FormatString(request.tssfac);
                                line = line.Replace("{{tssfac}}", tssfac);
                            }
                            break;

                        case 42:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_001}}", dt);
                            }
                            break;

                        case 45:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_002}}", dt);
                            }
                            break;

                        case 48:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_003}}", dt);
                            }
                            break;

                        case 51:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_004}}", dt);
                            }
                            break;

                        case 56:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_005}}", dt);
                            }
                            break;
                        //case 9:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 10:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 11:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 12:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 13:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 14:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;
                        //case 16:
                        //    line = line.Replace("{{slsfac}}", "张博");
                        //    break;

                        default:
                            isContinue = true; // 如果不需要替换，跳过当前行
                            break;
                    }

                    //if (isContinue)
                    //{
                    //    continue;
                    //}
                    // 将修改后的行写入输出文件
                    writer.WriteLine(line);
                }
            }
        }

        private static string FormatString(double number)
        {
            ////保留6位小数
            //string str = number.ToString("F6");
            string formattedString = number.ToString().PadLeft(10); // 转换为字符串并填充空格
            //string formattedString = number.ToString("F2").PadLeft(10); // 保留两位小数
            //Console.WriteLine($"'{formattedString}'");
            return formattedString;
        }

        private static string FormatString(int number)
        {
            string formattedString = number.ToString().PadLeft(10); // 转换为字符串并填充空格
            return formattedString;
        }

        private static string FormatString(string original)
        {
            string paddedString = original.PadLeft(10, ' ');
            return paddedString;
        }

        #endregion 抗弹模块

        #region 抗爆模块

        public static async Task GenerateMainFileByKangBaoComputeResult(KangBaoComputeResultModel request)
        {
            string folderPath = request.FolderPath;
            string kFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            string inputFilePath = Path.Combine(kFilePath, "KangBaoMainFileTemplate.k");
            string outputFilePath = Path.Combine(folderPath, "main.k");
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath); // 删除已存在的输出文件
            }

            int lintCount = 0;

            using (StreamReader reader = new StreamReader(inputFilePath))
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                string? line;

                // 逐行读取文件内容
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lintCount++;
                    switch (lintCount)
                    {
                        case 24:
                            // 滑动界面接触刚度因子
                            {
                                string slsfac = FormatString(request.slsfac);
                                line = line.Replace("{{slsfac}}", slsfac);
                            }
                            break;

                        case 47:
                            // 计算时间
                            {
                                string endtim = FormatString(request.endtim);
                                line = line.Replace("{{endtim}}", endtim);
                            }
                            break;

                        case 51:
                            // 时间步长
                            {
                                string tssfac = FormatString(request.tssfac);
                                line = line.Replace("{{tssfac}}", tssfac);
                            }
                            break;

                        case 56:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_001}}", dt);
                            }
                            break;

                        case 59:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_002}}", dt);
                            }
                            break;

                        case 62:
                            // 输出间隔
                            {
                                string dt = FormatString(request.dt);
                                line = line.Replace("{{dt_003}}", dt);
                            }
                            break;

                        case 66:
                            // 输出间隔
                            {
                                string dt = FormatString(request.D3plots);
                                line = line.Replace("{{dt_004}}", dt);
                            }
                            break;

                        case 69:
                            // 输出间隔
                            {
                                string dt = FormatString(request.D3plots);
                                line = line.Replace("{{dt_005}}", dt);
                            }
                            break;

                        case 87:
                            {
                                //string dt = FormatString(request.D3plots);
                                //line = line.Replace("{{DATABASE_TRACER}}", dt);
                            }
                            break;

                        case 205:
                            {
                                string val = FormatString(request.StructMaterialItemId);
                                line = line.Replace("{{mid_01}}", val);
                            }
                            break;

                        case 214:
                            {
                                string val = FormatString(request.StructWallThickness);
                                line = line.Replace("{{Wall_t}}", val).Replace("{{Wall_t}}", val).Replace("{{Wall_t}}", val).Replace("{{Wall_t}}", val);
                            }
                            break;

                        case 223:
                            {
                                string val = FormatString(request.BoxMaterialItemId);
                                line = line.Replace("{{mid_02}}", val);
                            }
                            break;

                        case 232:
                            {
                                string val = FormatString(request.BoxThickness);
                                line = line.Replace("{{Wall_t}}", val).Replace("{{Wall_t}}", val).Replace("{{Wall_t}}", val).Replace("{{Wall_t}}", val);
                            }
                            break;

                        case 240:
                            {
                                string val = FormatString(request.DynamiteMaterialItemId);
                                line = line.Replace("{{mid_03}}", val).Replace("{{eosid3}}", val);
                            }
                            break;

                        case 257:
                            {
                                request.AirMaterialItemId = 1;
                                string val = FormatString(request.AirMaterialItemId);
                                line = line.Replace("{{mid_04}}", val).Replace("{{eosid4}}", val);
                            }
                            break;

                        case 269:
                            {
                                string val = SetMaterialItemsValue(request.MaterialItems);
                                line = line.Replace("{{MaterialItems}}", val);
                            }
                            break;

                        case 278:
                            {
                                // z1和z2对应炸药z方向位置和长度；r1和r2对应在炸药半径，r1和r2都是charge_r，z1 -charge_l/2和z2是charge_l/2
                                string z1 = FormatString(request.Charge_l / 2);
                                string z2 = FormatString(0 - request.Charge_l / 2);
                                string val = FormatString(request.Charge_r);
                               
                                line = line.Replace("{{z1_001}}", z1).Replace("{{z2_001}}", z2).Replace("{{r1_001}}", val).Replace("{{r2_001}}", val);
                            }
                            break;

                        case 305:
                            {
                                
                                string val = FormatString(0 - (request.L1_x + 70));
                                line = line.Replace("{{ale_01}}", val);
                            }
                            break;

                        case 306:
                            {
                                string val = FormatString(0 - (request.L1_x + 10));
                                line = line.Replace("{{ale_02}}", val);
                            }
                            break;

                        case 307:
                            {
                                string val = FormatString((31 + (request.L1_x + 10)));
                                line = line.Replace("{{ale_03}}", val);
                            }
                            break;

                        case 312:
                            {
                                string val = FormatString(0 - (request.L2_y / 2.0 + 100));
                                line = line.Replace("{{ale_04}}", val);
                            }
                            break;

                        case 313:
                            {
                                string val = FormatString(0 - (request.L2_y / 2.0));
                                line = line.Replace("{{ale_05}}", val);
                            }
                            break;

                        case 314:
                            {
                                string val1 = FormatString(51 + (request.L1_y + request.L2_y));
                                string val2 = FormatString((request.L2_y / 2.0) + request.L1_y);
                                line = line.Replace("{{ale_06}}", val1).Replace("{{ale_07}}", val2);
                            }
                            break;

                        case 315:
                            {
                                string val1 = FormatString(81 + (request.L1_y + request.L2_y));
                                string val2 = FormatString((request.L2_y / 2.0) + request.L1_y + 60);
                                line = line.Replace("{{ale_08}}", val1).Replace("{{ale_09}}", val2);
                            }
                            break;

                        case 320:
                            {
                                string val = FormatString(0 - (request.L1_z / 2.0 + 100));
                                line = line.Replace("{{ale_10}}", val);
                            }
                            break;

                        case 321:
                            {
                                string val = FormatString(0 - (request.L1_z / 2.0));
                                line = line.Replace("{{ale_11}}", val);
                            }
                            break;

                        case 322:
                            {
                                string val1 = FormatString(51 + request.L1_z);
                                string val2 = FormatString(request.L1_z / 2.0);
                                line = line.Replace("{{ale_12}}", val1).Replace("{{ale_13}}", val2);
                            }
                            break;

                        case 323:
                            {
                                string val1 = FormatString(101 + request.L1_z);
                                string val2 = FormatString((request.L1_z / 2.0) + 100);
                                line = line.Replace("{{ale_14}}", val1).Replace("{{ale_15}}", val2);
                            }
                            break;

                        default:
                            break;
                    }
                    writer.WriteLine(line);
                }

            }
        }

        #endregion 抗爆模块
    }
}