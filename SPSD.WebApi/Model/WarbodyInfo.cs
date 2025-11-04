using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPSD.WebApi.Model
{
    /// <summary>
    /// 弹体建模信息表
    /// </summary>
    [Table("WarbodyInfo")]
    public class WarbodyInfo
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }

        /// <summary>
        /// 弹体类型
        /// </summary>
        public WarbodyTypeEnum? WarbodyType { get; set; }

        /// <summary>
        /// 靶板长度1
        /// </summary>
        public decimal? TargetPlatesLengthOne { get; set; }

        /// <summary>
        ///  靶板长度2
        /// </summary>
        public decimal? TargetPlatesLengthTwo { get; set; }

        /// <summary>
        /// 弹体长度
        /// </summary>
        public decimal? WarbodyLength { get; set; }

        ///// <summary>
        ///// 材料ID
        ///// </summary>
        //public int? MaterialId { get; set; }

        ///// <summary>
        ///// 材料名称
        ///// </summary>
        //public required string MaterialName { get; set; }


        /// <summary>
        /// 81被甲材料项Id
        /// </summary>
        public int OneMaterialItemId { get; set; }

        /// <summary>
        /// 82尖头材料项Id
        /// </summary>
        public int TwoMaterialItemId { get; set; }


        /// <summary>
        /// 83弹芯材料项Id
        /// </summary>
        public int ThreeMaterialItemId { get; set; }
    }

    public enum WarbodyTypeEnum
    {
        /// <summary>
        /// 截锥
        /// </summary>
        TruncatedCone = 0,
        /// <summary>
        /// 圆柱形
        /// </summary>
        Cylindrical = 1,
        /// <summary>
        /// 卵形弹
        /// </summary>
        Oval = 2,
    }
}
