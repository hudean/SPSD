using System.ComponentModel.DataAnnotations;

namespace SPSD.WebApi.ParamModel
{
    public class AddProject
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类别 1 main 2 抗暴
        /// </summary>
        [Range(1, 2,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
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
        public string? CreateTime { get; set; }

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
    }
}
