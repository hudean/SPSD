namespace SPSD.WebApi.ParamModel
{
    public class EditComputeResultStatusModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// 计算结果状态 0 默认 1 中止 2 完成
        /// </summary>
        public int ComputeResultStatus { get; set; } = 0;
    }
}
