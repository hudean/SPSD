using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSD.WinFormsApp.Model
{
    public class ComputeResultModel
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
        /// 上复合板层数
        /// </summary>
        public int TopTier { get; set; }

        /// <summary>
        /// 下复合板层数
        /// </summary>
        public int BottomTier { get; set; }

        /// <summary>
        /// 上复合板层材料
        /// </summary>
        public int TopMaterialItemId { get; set; }

        /// <summary>
        /// 上复合板层材料EosName
        /// </summary>
        public string? TopMaterialItemEosName { get; set; }


        /// <summary>
        /// 下复合板层材料
        /// </summary>
        public int BottomMaterialItemId { get; set; }

        /// <summary>
        /// 下复合板层材料EosName
        /// </summary>
        public string? BottomMaterialItemEosName { get; set; }

        /// <summary>
        /// 蜂窝复合板层材料
        /// </summary>
        public int FwcellMaterialItemId { get; set; }

        /// <summary>
        /// 蜂窝复合板层材料EosName
        /// </summary>
        public string? FwcellMaterialItemEosName { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public double Speed { get; set; }


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


        public List<MaterialItem> MaterialItems { get; set; } = new List<MaterialItem>();

    }
}



public class MaterialItem
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Key]
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int MaterialId { get; set; }

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
    [MaxLength(255)]
    public required string StrengthModelParameter { get; set; }


    /// <summary>
    /// 强度模型参数值
    /// </summary>
    [MaxLength(255)]
    public required string StrengthModelValue { get; set; }


    /// <summary>
    /// 状态方程名称
    /// </summary>
    [MaxLength(255)]
    public required string EOSName { get; set; }

    /// <summary>
    /// 状态方程参数名
    /// </summary>
    [MaxLength(255)]
    public required string EOSParameter { get; set; }

    /// <summary>
    /// 状态方程参数值
    /// </summary>
    [MaxLength(255)]
    public required string EOSValue { get; set; }

    /// <summary>
    /// 失效模型
    /// </summary>
    public string FailureModelName { get; set; } = string.Empty ;

    /// <summary>
    /// 失效模型参数
    /// </summary>
    public string FailureModelParameter { get; set; } = string.Empty;


    /// <summary>
    /// 失效模型参数值
    /// </summary>
    public string FailureModelValue { get; set; } = string.Empty;
    /// <summary>
    /// 引用数量
    /// </summary>
    public int ReferenceCount { get; set; }

}