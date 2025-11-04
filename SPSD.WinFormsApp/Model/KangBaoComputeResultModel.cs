using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp.Model
{
    public class KangBaoComputeResultModel
    {
        public int ProjectId { get; set; }

        public double tssfac { get; set; }

        public double slsfac { get; set; }

        public double endtim { get; set; }

        public double dt { get; set; }

        /// <summary>
        /// D3plots-单位微秒
        /// </summary>
        public double D3plots { get; set; }

        /// <summary>
        /// 项目工作目录文件夹
        /// </summary>
        public string FolderPath { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// </summary>
        public double Charge_r { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Charge_l { get; set; }

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
        public double L2_y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double L1_z { get; set; }

        /// <summary>
        /// 炸药材料Id
        /// </summary>
        public int DynamiteMaterialItemId { get; set; }


        /// <summary>
        /// 空气材料Id
        /// </summary>
        public int AirMaterialItemId { get; set; }


        /// <summary>
        /// 箱体材料Id
        /// </summary>
        public int BoxMaterialItemId { get; set; }

        /// <summary>
        /// 抗爆结构材料Id
        /// </summary>
        public int StructMaterialItemId { get; set; }

        /// <summary>
        /// 抗爆结构壁厚单位cm 
        /// </summary>
        public double StructWallThickness { get; set; }

        /// <summary>
        /// 箱体壁厚单位cm 
        /// </summary>
        public double BoxThickness { get; set; }

        public List<MaterialItem> MaterialItems { get; set; } = new List<MaterialItem>();
    }

}
