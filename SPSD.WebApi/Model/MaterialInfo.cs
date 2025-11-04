using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPSD.WebApi.Model
{
    /// <summary>
    /// 材料表信息
    /// </summary>
    //[Table("TTmpMaterial")]
    [Table("MaterialInfo")]
    public class MaterialInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        //[Column("MaterialId")]
        public int Id { get; set; }

        /// <summary>
        /// 材料名称
        /// </summary>
        [MaxLength(255)]
        public required string MaterialName { get; set; }

        /// <summary>
        /// 强度模型名称(本构方程)
        /// </summary>
        [MaxLength(255)]
        public required string StrengthModelName { get; set; }


        /// <summary>
        /// 强度模型参数名
        /// </summary>
        [MaxLength(500)]
        public required string StrengthModelParameter { get; set; }


        /// <summary>
        /// 强度模型参数值
        /// </summary>
        [MaxLength(500)]
        public required string StrengthModelValue { get; set; }


        /// <summary>
        /// 状态方程名称
        /// </summary>
        [MaxLength(255)]
        public required string EOSName { get; set; }

        /// <summary>
        /// 状态方程参数名
        /// </summary>
        [MaxLength(500)]
        public required string EOSParameter { get; set; }

        /// <summary>
        /// 状态方程参数值
        /// </summary>
        [MaxLength(500)]
        public required string EOSValue { get; set; }

        /// <summary>
        /// 失效模型名称
        /// </summary>
        [MaxLength(255)]
        public string FailureModelName { get; set; } = string.Empty;

        /// <summary>
        /// 失效模型参数
        /// </summary>
        [MaxLength(500)]
        public string FailureModelParameter { get; set; } = string.Empty;


        /// <summary>
        /// 失效模型参数值
        /// </summary>
        [MaxLength(500)]
        public string FailureModelValue { get; set; } = string.Empty;


        /// <summary>
        /// 引用数量
        /// </summary>
        public int ReferenceCount { get; set; }
    }
}
