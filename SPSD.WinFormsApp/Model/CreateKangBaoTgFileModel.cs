using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp.Model
{
    public class CreateKangBaoTgFileModel
    {
        /// <summary>
        /// 
        /// </summary>
        public double Charge_l { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Charge_y { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public double Charge_z { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public double SubL1_y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double SubL1_z { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public double L1_x { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double L1_y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double L1_z { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double L2_y { get; set; }

        /// <summary>
        /// 项目工作目录文件夹
        /// </summary>
        public string FolderPath { get; set; } = string.Empty;

        public double Edx = 1.0;
    }
}
