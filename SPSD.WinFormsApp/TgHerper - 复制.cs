using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPSD.WinFormsApp.Model;

namespace SPSD.WinFormsApp
{
    public class TgHerper2
    {
        public class TgBoard
        {
            /// <summary>
            /// 上复合板层数
            /// </summary>
            public int TopTier { get; set; }
            /// <summary>
            /// 上复合板层厚度
            /// </summary>
            public double Tophickness { get; set; }
            /// <summary>
            /// 下复合板层数
            /// </summary>
            public int BottomTier { get; set; }
            /// <summary>
            /// 下复合板层厚度
            /// </summary>
            public double BottomThickness { get; set; }

            /// <summary>
            /// 复合板横向长
            /// </summary>
            public double X_Length { get; set; }

            /// <summary>
            /// 复合板纵向长
            /// </summary>
            public double Y_Length { get; set; }
        }


        public class TgFwcell
        {
            /// <summary>
            /// 胞元内六角上边界边长r
            /// </summary>
            public double InnerTopLength { get; set; }

            /// <summary>
            /// 胞元外六角上边界边长rw
            /// </summary>
            public double OutTopLength { get; set; }

            /// <summary>
            /// 胞元内六角下边界边长R
            /// </summary>
            public double InnerBottomLength { get; set; }

            /// <summary>
            /// 胞元外六角下边界边长Rw
            /// </summary>
            public double OutBottomLength { get; set; }

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

        }

        public struct TgPoint
        {
            // 定义四分之一（右上角）蜂窝胞元平面6个坐标点
            public double p1;
            public double p2;
            public double p3;
            public double p4;
            public double p5;
            public double p6;
        }

        public struct TgIndex
        {
            public List<double> i_fw;     // 横向索引
            public List<double> j_fw;     // 纵向索引
        }

        public static void CreateTgFile(CreateTgFileModel request)
        {
            // 这里可以实现创建tg文件的逻辑
            // 例如将内容写入到指定的文件中
            //System.IO.File.WriteAllText(fileName, content);


            // 定义蜂窝结构参数类型
            TgFwcell honeycome = new TgFwcell()
            {
                // 输入蜂窝胞元参数
                InnerTopLength = 9,
                InnerBottomLength = 14,
                //OutTopLength = 0,
                //OutBottomLength = 0,
                Thickness = 1.5,
                Height = 10,
                XCount = 6,
                YCount = 5
            };

            // 计算参数
            honeycome.OutTopLength = honeycome.InnerTopLength + 2.0 * honeycome.Thickness / Math.Pow(3, 0.5);
            honeycome.OutBottomLength = honeycome.InnerBottomLength + 2.0 * honeycome.Thickness / Math.Pow(3, 0.5);



            // 定义蜂窝坐标参数类型
            TgPoint xup = new TgPoint();
            TgPoint yup = new TgPoint();
            TgPoint xunder = new TgPoint();
            TgPoint yunder = new TgPoint();

            TgIndex fw_num = new TgIndex
            {
                i_fw = new List<double>(),
                j_fw = new List<double>()
            };

            // 定义复合板参数类型
            TgBoard board = new TgBoard()
            {
                // 输入复合板参数
                TopTier = 6,
                Tophickness = 0.33,
                BottomTier = 38,
                BottomThickness = 0.33,
            };


            double edx = 0.4; // 网格尺寸，单位mm


            // 建立胞元中心坐标长度索引
            for (int ifw_i = 0; ifw_i < honeycome.XCount + 1; ifw_i++)
            {
                double dx = 1.5 * honeycome.OutBottomLength;
                fw_num.i_fw.Add(ifw_i * dx);
            }

            for (int jfw_i = 0; jfw_i < 2 * honeycome.YCount + 1; jfw_i++)
            {
                double dy = Math.Pow(3, 0.5) * honeycome.OutBottomLength / 2;
                fw_num.j_fw.Add(jfw_i * dy);
            }

            board.X_Length = fw_num.i_fw[honeycome.YCount] + honeycome.OutBottomLength;
            board.Y_Length = fw_num.j_fw[2 * honeycome.YCount];

            double board_mid_l = fw_num.i_fw[2] + honeycome.OutBottomLength; // 复合板横向加密长度
            double board_mid_w = fw_num.j_fw[4]; // 复合板纵向加密长度


            // 计算所需四分之一胞元总数
            int cell_sum = 0;

            // for (int sum_i = 0; sum_i < honeycome.num_heng + 1; sum_i++)
            // {
            //     if (sum_i % 2 == 0)
            //     {
            //         cell_sum += honeycome.num_zong + 1;
            //     }
            //     else
            //     {
            //         cell_sum += honeycome.num_zong;
            //     }
            // }

            // 胞元上坐标点
            xup.p1 = 0;
            yup.p1 = Math.Pow(3, 0.5) * honeycome.OutTopLength / 2.0;
            xup.p2 = honeycome.OutTopLength / 2.0;
            yup.p2 = Math.Pow(3, 0.5) * honeycome.OutTopLength / 2.0;
            xup.p3 = honeycome.OutTopLength;
            yup.p3 = 0;
            xup.p4 = honeycome.InnerTopLength;
            yup.p4 = 0;
            xup.p5 = honeycome.InnerTopLength / 2.0;
            yup.p5 = Math.Pow(3, 0.5) * honeycome.InnerTopLength / 2.0;
            xup.p6 = 0;
            yup.p6 = Math.Pow(3, 0.5) * honeycome.InnerTopLength / 2.0;

            // 胞元下坐标点
            xunder.p1 = 0;
            yunder.p1 = Math.Pow(3, 0.5) * honeycome.OutBottomLength / 2.0;
            xunder.p2 = honeycome.OutBottomLength / 2.0;
            yunder.p2 = Math.Pow(3, 0.5) * honeycome.OutBottomLength / 2.0;
            xunder.p3 = honeycome.OutBottomLength;
            yunder.p3 = 0;
            xunder.p4 = honeycome.InnerBottomLength;
            yunder.p4 = 0;
            xunder.p5 = honeycome.InnerBottomLength / 2.0;
            yunder.p5 = Math.Pow(3, 0.5) * honeycome.InnerBottomLength / 2.0;
            xunder.p6 = 0;
            yunder.p6 = Math.Pow(3, 0.5) * honeycome.InnerBottomLength / 2.0;

            // 网格数量计算
            int edx_tn = (int)(honeycome.Thickness / edx);             // 胞元厚度网格
            int edx_heng = (int)(honeycome.InnerTopLength * 0.5 / edx); // 胞元横面网格
            int edx_xie = (int)(honeycome.InnerTopLength / edx);       // 胞元斜面网格
            int edx_he = (int)(honeycome.Height / edx);             // 胞元高度网格

            int edx_board_mid_l = (int)(board_mid_l / edx);         // 复合板横向加密区域网格数
            int edx_board_mid_w = (int)(board_mid_w / edx);         // 复合板纵向加密区域网格数
            int edx_board_l = (int)((board.X_Length - board_mid_l) / (3.2 * edx));  // 复合板横向网格数
            int edx_board_w = (int)((board.Y_Length - board_mid_w) / (3.2 * edx));  // 复合板纵向网格数
            int edx_board_up_h = (int)(board.Tophickness / edx);                 // 上复合板厚度网格数
            int edx_board_under_h = (int)(board.BottomThickness / edx);           // 下复合板厚度网格数



          

        }
    }
}
