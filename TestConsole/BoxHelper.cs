using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    public class BoxHelper
    {
        public struct Box
        {
            /// <summary>
            /// 装药箱体的x方向长度
            /// </summary>
            public double charge_i;        
            /// <summary>
            /// 装药箱体的y方向长度
            /// </summary>
            public double charge_j;        
            /// <summary>
            /// 装药箱体的z方向长度
            /// </summary>
            public double charge_k;        
            /// <summary>
            /// 非装药箱体的y方向长度
            /// </summary>
            public double nocharge_j;      
            /// <summary>
            /// 箱体开口x方向长度
            /// </summary>
            public double opening_i;       
            /// <summary>
            /// 箱体开口y方向长度
            /// </summary>
            public double opening_j;       
        }

        public struct Edx
        {
            /// <summary>
            /// 网格尺寸cm
            /// </summary>
            public double edx;
            /// <summary>
            /// 开坑网格尺寸cm，网格尺寸的三分之一
            /// </summary>
            public double edx_open;         
        }

        // 生成文件的函数
        public static void WriteFile(Box box, Edx medx)
        {
            try
            {
                // 生成文件
                using (StreamWriter writer = new StreamWriter("box.tg"))
                {
                    int num_i = (int)(box.charge_i / medx.edx - 1);
                    int num_ii = (int)(box.opening_i / medx.edx_open - 1);
                    int num_j = (int)((box.charge_j / 2.0) / medx.edx - 1);
                    int num_jj = (int)((box.opening_j / 2.0) / medx.edx_open - 1);
                    int num_no_j = (int)(box.nocharge_j / medx.edx - 1);
                    int num_k = (int)(box.charge_k / medx.edx - 1);

                    writer.WriteLine("mate 2;");
                    //writer.WriteLine("block -1 2 3;-1 2 3 -4 -5;-1 -2;-{0} -{1} 0;-{2} -{3} {4} {5} {6};-{7} {8};",
                    //    box.charge_i, box.opening_i, box.charge_j / 2.0, box.opening_j / 2.0, box.opening_j / 2.0, box.charge_j / 2.0,
                    //    box.charge_j / 2.0 + box.nocharge_j, box.charge_k / 2.0, box.charge_k / 2.0);
                    writer.WriteLine("block -1 2 3;-1 2 3 -4 -5;-1 -2;-{0} -{1} 0;-{2} -{3} {4} {5} {6};-{7} {8};",
                        box.charge_i.ToString("F6"), box.opening_i.ToString("F6"), (box.charge_j / 2.0).ToString("F6"), (box.opening_j / 2.0).ToString("F6"), (box.opening_j / 2.0).ToString("F6"), (box.charge_j / 2.0).ToString("F6"),
                        (box.charge_j / 2.0 + box.nocharge_j).ToString("F6"), (box.charge_k / 2.0).ToString("F6"), (box.charge_k / 2.0).ToString("F6"));
                    writer.WriteLine("dei 2 3; 2 3; -2;");
                    writer.WriteLine("mseq i {0} {1};", num_i, num_ii);
                    writer.WriteLine("mseq j {0} {1} {2} {3};", num_j, num_jj, num_j, num_no_j);
                    writer.WriteLine("mseq k {0};", num_k);
                    writer.WriteLine("mti 1 3; 4 5; 1 2;1;");
                    writer.WriteLine("endpart;");
                    writer.WriteLine("merge");
                    writer.WriteLine("stp 0.01;");
                    writer.WriteLine("lsdyna keyword mof  001model.k;");
                    writer.WriteLine("write;");
                    writer.WriteLine("end;");
                }

                Console.WriteLine("文件生成成功！！！");
            }
            catch (Exception ex)
            {
                Console.WriteLine("生成文件时出现错误: " + ex.Message);
            }
        }
    }
}
