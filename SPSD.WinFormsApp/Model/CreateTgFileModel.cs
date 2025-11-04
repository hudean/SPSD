using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp.Model
{
    public class CreateTgFileModel
    {
        /// <summary>
        /// 上复合板层数
        /// </summary>
        public int TopTier { get; set; }
        /// <summary>
        /// 上复合板层厚度
        /// </summary>
        public double TopThickness { get; set; }
        /// <summary>
        /// 下复合板层数
        /// </summary>
        public int BottomTier { get; set; }
        /// <summary>
        /// 下复合板层厚度
        /// </summary>
        public double BottomThickness { get; set; }

        /// <summary>
        /// 胞元建模
        /// </summary>
        public Fwcell FwcellInfo { get; set; } = new Fwcell();

        /// <summary>
        /// 项目工作目录文件夹
        /// </summary>
        public string FolderPath { get; set; } = string.Empty;


        public class Fwcell
        {

            /// <summary>
            /// 胞元内六角上边界边长r
            /// </summary>
            public double InnerTopLength { get; set; }

            ///// <summary>
            ///// 胞元外六角上边界边长rw
            ///// </summary>
            //public double OutTopLength { get; set; }

            /// <summary>
            /// 胞元内六角下边界边长R
            /// </summary>
            public double InnerBottomLength { get; set; }

            ///// <summary>
            ///// 胞元外六角下边界边长Rw
            ///// </summary>
            //public double OutBottomLength { get; set; }

            /// <summary>
            /// 胞元厚度dc
            /// </summary>
            public double Thickness { get; set; }

            /// <summary>
            /// 胞元高度hc
            /// </summary>
            public double Height { get; set; }


            /// <summary>
            /// 横向胞元数
            /// </summary>
            public int XCount { get; set; }

            /// <summary>
            /// 纵向胞元数
            /// </summary>
            public int YCount { get; set; }

        }
    }
}
