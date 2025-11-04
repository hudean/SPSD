using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SPSD.WebApi.Model
{
    /// <summary>
    /// 动能混伤建模信息
    /// KineticEnergyCondition
    /// </summary>
    [Table("KineticEnergyInfo")]
    public class KineticEnergyInfo
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }


       // /// <summary>
       // /// 着靶角度
       // /// </summary>
       // //[Precision(8, 2)]
       // public double LandingAngle { get; set; }

       // /// <summary>
       // /// 带侵蚀的画面接触
       // /// </summary>
       //// [Precision(8, 2)]
       // public double PictureContact { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        //[Precision(8, 2)]
        public double Speed { get; set; }

        ///// <summary>
        ///// 速度方向-默认Z方向
        ///// </summary>
        //public DirectionEnum SpeedDirection { get; set; }

        /// <summary>
        /// TSSFAC-单位微秒
        /// </summary>
        //[Precision(8, 2)]
        public double TSSFAC { get; set; }

        ///// <summary>
        ///// 计算时长-单位微秒
        ///// </summary>
        //public double ComputeDuration { get; set; }

        /// <summary>
        /// D3plots-单位微秒
        /// </summary>
        public double D3plots { get; set; }

        /// <summary>
        /// 滑动界面接触刚度因子-单位微秒
        /// </summary>
        //[Precision(8, 2)]
        public double SLSFAC { get; set; }

        /// <summary>
        /// 计算时间-单位微秒
        /// </summary>
       // [Precision(8, 2)]
        public double ENDTIM { get; set; }

        /// <summary>
        /// 计算时间-单位微秒
        /// </summary>
        //[Precision(8, 2)]
        public double DT { get; set; }
    }

    public enum DirectionEnum
    { 
        X = 0,
        Y = 1,
        Z = 2,
    }
}
