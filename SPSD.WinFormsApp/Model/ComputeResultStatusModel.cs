using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp.Model
{
    public class ComputeResultStatusModel
    {
        /// <summary>
        /// ProjectId
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目工作目录文件夹
        /// </summary>
        public string FolderPath { get; set; } = string.Empty;

        /// <summary>
        /// 计算结果状态 0 默认 1 中止 2 完成
        /// </summary>
        public int ComputeResultStatus { get; set; } = 0;


        public int PartCount { get; set; }
    }
}
