using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPSD.WebApi.Model
{

    /// <summary>
    /// 靶板模型信息
    /// </summary>
    [Table("KangBaoInfo")]
    public class KangBaoInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Charge_l { get; set; }

        /// <summary>
        /// charge_r/Charge_y
        /// </summary>
        public double Charge_r { get; set; }


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

        public double Edx { get; set; } 

        /// <summary>
        /// 抗爆结构壁厚单位cm 
        /// </summary>
        public double StructWallThickness { get; set; }

        /// <summary>
        /// 箱体壁厚单位cm 
        /// </summary>
        public double BoxThickness { get; set; }

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
    }
}
