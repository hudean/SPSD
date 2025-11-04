using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SPSD.WebApi.Model
{
    /// <summary>
    /// 靶板模型信息
    /// </summary>
    [Table("BoardInfo")]
    public class BoardInfo
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
        /// 上复合板层数
        /// </summary>
        public int TopTier { get; set; }
        /// <summary>
        /// 上复合板层厚度
        /// </summary>
        public double TopThickness { get; set; }

        /// <summary>
        /// 上复合板层材料
        /// </summary>
        public int TopMaterialItemId { get; set; }

        /// <summary>
        /// 下复合板层数
        /// </summary>
        public int BottomTier { get; set; }
        /// <summary>
        /// 下复合板层厚度
        /// </summary>
        public double BottomThickness { get; set; }

        /// <summary>
        /// 下复合板层材料
        /// </summary>
        public int BottomMaterialItemId { get; set; }


        /// <summary>
        /// 胞元建模
        /// </summary>
        public Fwcell FwcellInfo { get; set; } = new Fwcell();

    }

    /// <summary>
    /// 胞元就是蜂窝
    /// </summary>
    [Owned]
    public class Fwcell
    {

        /// <summary>
        /// 胞元内六角上边界边长r
        /// </summary>
        public double InnerTopLength { get; set; }

        /// <summary>
        /// 胞元内六角下边界边长R
        /// </summary>
        public double InnerBottomLength { get; set; }

        ///// <summary>
        ///// 胞元外六角上边界边长rw
        ///// </summary>
        //public double OutTopLength { get; set; }

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

        /// <summary>
        /// 蜂窝复合板层材料
        /// </summary>
        public int FwcellMaterialItemId { get; set; }

    }
}
