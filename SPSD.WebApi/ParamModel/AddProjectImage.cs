namespace SPSD.WebApi.ParamModel
{
    public class AddProjectImage
    {
        public int Id { get; set; }

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
    }
}
