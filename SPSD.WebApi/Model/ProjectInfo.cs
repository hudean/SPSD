using System.ComponentModel.DataAnnotations.Schema;

namespace SPSD.WebApi.Model
{
    /// <summary>
    /// 项目信息
    /// </summary>
    [Table("ProjectInfo")]
    public class ProjectInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类别 1 main 2 抗暴
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public required string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public required string ProjectName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 创建人creator
        /// </summary>
        public string CreateUser { get; set; } = string.Empty;

        /// <summary>
        /// 仿真类型1
        /// </summary>
        public string? SimulationTypeOne { get; set; }

        /// <summary>
        /// 仿真类型2
        /// </summary>
        public string? SimulationTypeTwo { get; set; }

        /// <summary>
        /// 工作目录
        /// </summary>
        public string? WorkDirectory { get; set; }

        /// <summary>
        /// 防护结构变形图
        /// </summary>
        public string? ImageOnePath { get; set; }

        /// <summary>
        /// 弹体速度变化取消图
        /// </summary>
        public string? ImageTwoPath { get; set; }

        /// <summary>
        /// 防护结构各Part内能变化曲线图
        /// </summary>
        public string? ImageThreePath { get; set; }

        /// <summary>
        /// 防护结构整体内容变化曲线图
        /// </summary>
        public string? ImageFourPath { get; set; }

        /// <summary>
        /// 计算结果状态 0 默认 1 中止 2 计算完成  3 计算结果提取完成
        /// </summary>
        public int ComputeResultStatus { get; set; } = 0;
    }
}
