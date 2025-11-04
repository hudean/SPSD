using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPSD.WinFormsApp.Model;

namespace SPSD.WinFormsApp
{
    public class TgHerper
    {
        public struct Board
        {
            /// <summary>
            /// 上复合板层数
            /// </summary>
            public int num_up;

            /// <summary>
            /// 下复合板层数
            /// </summary>
            public int num_under;

            /// <summary>
            /// 上复合板厚度
            /// </summary>
            public double h_up;       

            /// <summary>
            /// 下复合板厚度
            /// </summary>
            public double h_under;    

            /// <summary>
            /// 复合板横向长
            /// </summary>
            public double l;          

            /// <summary>
            /// 复合板纵向长
            /// </summary>
            public double w;          
        }

        public struct Fwcell
        {
            /// <summary>
            /// 胞元内六角上边界边长r
            /// </summary>
            public double r_in_up;        

            /// <summary>
            /// 胞元外六角上边界边长rw
            /// </summary>
            public double r_out_up;       

            /// <summary>
            /// 胞元内六角下边界边长R
            /// </summary>
            public double r_in_under;     

            /// <summary>
            /// 胞元外六角下边界边长Rw
            /// </summary>
            public double r_out_under;    

            /// <summary>
            /// 胞元厚度dc
            /// </summary>
            public double Tn;             

            /// <summary>
            /// 胞元高度hc
            /// </summary>
            public double He;             

            /// <summary>
            /// 横向胞元数
            /// </summary>
            public int num_heng;          

            /// <summary>
            /// 纵向胞元数
            /// </summary>
            public int num_zong;          
        }

        public struct Point
        {
            // 定义四分之一（右上角）蜂窝胞元平面6个坐标点
            public double p1;

            public double p2;
            public double p3;
            public double p4;
            public double p5;
            public double p6;
        }

        public struct Index
        {
            public List<double> i_fw;     // 横向索引
            public List<double> j_fw;     // 纵向索引
        }

        /// <summary>
        /// 生成model.tg文件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fileName"></param>
        public static void CreateModelTgFile(CreateTgFileModel request,string fileName)
        {
            // 定义蜂窝结构参数类型
            Fwcell honeycome = new Fwcell();
            // 定义蜂窝坐标参数类型
            Point xup = new Point();
            Point yup = new Point();
            Point xunder = new Point();
            Point yunder = new Point();

            Index fw_num = new Index
            {
                i_fw = new List<double>(),
                j_fw = new List<double>()
            };

            // 定义复合板参数类型
            Board board = new Board();

            // 输入蜂窝胞元参数
            honeycome.r_in_up = request.FwcellInfo.InnerTopLength;//  9;
            honeycome.r_in_under = request.FwcellInfo.InnerBottomLength; //14;
            honeycome.Tn = request.FwcellInfo.Thickness; //1.5;
            honeycome.He = request.FwcellInfo.Height; //10;
            honeycome.num_heng = request.FwcellInfo.XCount; // 6;
            honeycome.num_zong = request.FwcellInfo.YCount; // 5;

            double edx = 0.4; // 网格尺寸，单位mm

            // 输入复合板参数
            board.num_up = request.TopTier;//6;
            board.num_under = request.BottomTier;//38;
            board.h_up = request.TopThickness;//0.33;
            board.h_under = request.BottomThickness;//0.33;

            // 计算参数
            honeycome.r_out_up = honeycome.r_in_up + 2.0 * honeycome.Tn / Math.Pow(3, 0.5);
            honeycome.r_out_under = honeycome.r_in_under + 2.0 * honeycome.Tn / Math.Pow(3, 0.5);

            // 建立胞元中心坐标长度索引
            for (int ifw_i = 0; ifw_i < honeycome.num_heng + 1; ifw_i++)
            {
                double dx = 1.5 * honeycome.r_out_under;
                fw_num.i_fw.Add(ifw_i * dx);
            }

            for (int jfw_i = 0; jfw_i < 2 * honeycome.num_zong + 1; jfw_i++)
            {
                double dy = Math.Pow(3, 0.5) * honeycome.r_out_under / 2;
                fw_num.j_fw.Add(jfw_i * dy);
            }

            board.l = fw_num.i_fw[honeycome.num_heng] + honeycome.r_out_under;
            board.w = fw_num.j_fw[2 * honeycome.num_zong];

            double board_mid_l = fw_num.i_fw[2] + honeycome.r_out_under; // 复合板横向加密长度
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
            yup.p1 = Math.Pow(3, 0.5) * honeycome.r_out_up / 2.0;
            xup.p2 = honeycome.r_out_up / 2.0;
            yup.p2 = Math.Pow(3, 0.5) * honeycome.r_out_up / 2.0;
            xup.p3 = honeycome.r_out_up;
            yup.p3 = 0;
            xup.p4 = honeycome.r_in_up;
            yup.p4 = 0;
            xup.p5 = honeycome.r_in_up / 2.0;
            yup.p5 = Math.Pow(3, 0.5) * honeycome.r_in_up / 2.0;
            xup.p6 = 0;
            yup.p6 = Math.Pow(3, 0.5) * honeycome.r_in_up / 2.0;

            // 胞元下坐标点
            xunder.p1 = 0;
            yunder.p1 = Math.Pow(3, 0.5) * honeycome.r_out_under / 2.0;
            xunder.p2 = honeycome.r_out_under / 2.0;
            yunder.p2 = Math.Pow(3, 0.5) * honeycome.r_out_under / 2.0;
            xunder.p3 = honeycome.r_out_under;
            yunder.p3 = 0;
            xunder.p4 = honeycome.r_in_under;
            yunder.p4 = 0;
            xunder.p5 = honeycome.r_in_under / 2.0;
            yunder.p5 = Math.Pow(3, 0.5) * honeycome.r_in_under / 2.0;
            xunder.p6 = 0;
            yunder.p6 = Math.Pow(3, 0.5) * honeycome.r_in_under / 2.0;

            // 网格数量计算
            int edx_tn = (int)(honeycome.Tn / edx);             // 胞元厚度网格
            int edx_heng = (int)(honeycome.r_in_up * 0.5 / edx); // 胞元横面网格
            int edx_xie = (int)(honeycome.r_in_up / edx);       // 胞元斜面网格
            int edx_he = (int)(honeycome.He / edx);             // 胞元高度网格

            int edx_board_mid_l = (int)(board_mid_l / edx);         // 复合板横向加密区域网格数
            int edx_board_mid_w = (int)(board_mid_w / edx);         // 复合板纵向加密区域网格数
            int edx_board_l = (int)((board.l - board_mid_l) / (3.2 * edx));  // 复合板横向网格数
            int edx_board_w = (int)((board.w - board_mid_w) / (3.2 * edx));  // 复合板纵向网格数
            int edx_board_up_h = (int)(board.h_up / edx);                 // 上复合板厚度网格数
            int edx_board_under_h = (int)(board.h_under / edx);           // 下复合板厚度网格数

            // TODO: 生成TG文件内容

            if (!Directory.Exists(request.FolderPath))
            {
                Directory.CreateDirectory(request.FolderPath);
            }
            string filePath = Path.Combine(request.FolderPath, fileName);
            // 删除旧文件
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            // 偏移量+1000
            int offset = 1000;
            // double 保留两位小数
            using (StreamWriter tgFile = new StreamWriter(filePath))
            {
                tgFile.WriteLine("csca 0.1;");
                //上层复合板
                for (int i = 0; i < board.num_up; i++)
                {
                   
                    tgFile.Write("mate {0};\n", i + 1+ offset);
                    tgFile.Write("block 1 2 3;1 2 3;1 2;0 {0} {1};0 {2} {3};{4} {5};\n", board_mid_l, board.l, board_mid_w, board.w, honeycome.He + i * board.h_up, honeycome.He + (i + 1) * board.h_up);
                    tgFile.Write("mseq i {0} {1};\n", edx_board_mid_l - 1, edx_board_l - 1);
                    tgFile.Write("mseq j {0} {1};\n", edx_board_mid_w - 1, edx_board_w - 1);
                    tgFile.Write("mseq k {0}\n", edx_board_up_h - 1);
                    tgFile.WriteLine("b 1 3 1 3 3 2 dx 1 dy 1 dz 1 rx 1 ry 1 rz 1;");
                    tgFile.WriteLine("b 3 1 1 3 3 2 dx 1 dy 1 dz 1 rx 1 ry 1 rz 1;");
                    tgFile.Write("endpart\n");
                    tgFile.Write("\n");

                }

                //------------------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------------------
                //下层复合板
                for (int i = 0; i < board.num_under; i++)
                {
                    tgFile.Write("mate {0};\n", board.num_up + i + 1 + offset);

                    tgFile.Write("block 1 2 3;1 2 3;1 2;0 {0} {1};0 {2} {3};{4} {5};\n", board_mid_l, board.l, board_mid_w, board.w, -i * board.h_under, -(i + 1) * board.h_under);
                    tgFile.Write("mseq i {0} {1};\n", edx_board_mid_l - 1, edx_board_l - 1);
                    tgFile.Write("mseq j {0} {1};\n", edx_board_mid_w - 1, edx_board_w - 1);
                    tgFile.Write("mseq k {0}\n", edx_board_under_h - 1);
                    tgFile.WriteLine("b 1 3 1 3 3 2 dx 1 dy 1 dz 1 rx 1 ry 1 rz 1;");
                    tgFile.WriteLine("b 3 1 1 3 3 2 dx 1 dy 1 dz 1 rx 1 ry 1 rz 1;");
                    tgFile.Write("endpart\n");
                    tgFile.Write("\n");

                }

                //------------------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------------------
                //蜂窝板建模

                tgFile.WriteLine("mate {0};", board.num_up + board.num_under + 1+ offset);
                for (int ii = 0; ii < honeycome.num_heng + 1; ii++)
                {
                    for (int jj = 0; jj < 2 * honeycome.num_zong + 1; jj++)
                    {
                        //横纵坐标索引相加为偶数才有蜂窝胞元中心点
                        if ((ii + jj) % 2 == 0)
                        {
                            cell_sum++;
                            tgFile.Write("block 1 2;1 2 3;1 2;0 {0};0 {1} {2};0 0;\n", honeycome.Tn, honeycome.r_in_up, honeycome.r_in_up * 1.5);
                            tgFile.Write("mseq i {0};\n", edx_tn);
                            tgFile.Write("mseq j {0} {1};\n", edx_xie, edx_heng);
                            tgFile.Write("mseq k {0};\n", edx_he);

                            tgFile.Write("pb 2 3 2 2 3 2 xy {0} {1}\n", xup.p1, yup.p1);
                            tgFile.Write("pb 2 2 2 2 2 2 xy {0} {1}\n", xup.p2, yup.p2);
                            tgFile.Write("pb 2 1 2 2 1 2 xy {0} {1}\n", xup.p3, yup.p3);
                            tgFile.Write("pb 1 1 2 1 1 2 xy {0} {1}\n", xup.p4, yup.p4);
                            tgFile.Write("pb 1 2 2 1 2 2 xy {0} {1}\n", xup.p5, yup.p5);
                            tgFile.Write("pb 1 3 2 1 3 2 xy {0} {1}\n", xup.p6, yup.p6);

                            tgFile.Write("pb 2 3 1 2 3 1 xy {0} {1}\n", xunder.p1, yunder.p1);
                            tgFile.Write("pb 2 2 1 2 2 1 xy {0} {1}\n", xunder.p2, yunder.p2);
                            tgFile.Write("pb 2 1 1 2 1 1 xy {0} {1}\n", xunder.p3, yunder.p3);
                            tgFile.Write("pb 1 1 1 1 1 1 xy {0} {1}\n", xunder.p4, yunder.p4);
                            tgFile.Write("pb 1 2 1 1 2 1 xy {0} {1}\n", xunder.p5, yunder.p5);
                            tgFile.Write("pb 1 3 1 1 3 1 xy {0} {1}\n", xunder.p6, yunder.p6);

                            tgFile.Write("sfi 1 2; 1 3; -2;plan 0 0 {0} 0 0 1;\n", honeycome.He);
                            tgFile.Write("sfi 1 2; -3; 1 2;plan 0 0 0 1 0 0;\n");
                            tgFile.Write("sfi 1 2; -1; 1 2;plan 0 0 0 0 1 0;\n");

                            //横坐标为0时仅有胞元右边二分之一或者四分之一蜂窝
                            if (ii == 0)
                            {
                                //横纵坐标均为0时，仅有胞元右上角四分之一蜂窝结构
                                if (jj == 0)
                                {
                                    tgFile.Write("endpart;\n");
                                    tgFile.Write("\n");
                                }

                                //纵坐标为最大边界时，仅有右下角四分之一蜂窝结构
                                else if (jj == 2 * honeycome.num_zong)
                                {
                                    tgFile.Write("lct 1 rzx;\n");
                                    tgFile.Write("lrep 1;\n");
                                    tgFile.Write("gct 1 mx {0} my {1};\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
                                    tgFile.Write("grep 1;\n");
                                    tgFile.Write("endpart;\n");
                                    tgFile.Write("\n");
                                }
                                //其余横索引为0时胞元仅右边蜂窝结构
                                else
                                {
                                    tgFile.Write("lct 1 rzx;\n");
                                    tgFile.Write("lrep 0 1;\n");
                                    tgFile.Write("gct 1 mx {0} my {1};\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
                                    tgFile.Write("grep 1;\n");
                                    tgFile.Write("endpart;\n");
                                    tgFile.Write("\n");
                                }
                            }
                            //横坐标为偶数时 仅纵向第一个和最后一个为胞元半个上下结构
                            else if (ii % 2 == 0)
                            {
                                //横向最大时，纵向第一个为半个胞元结构上
                                if (jj == 0)
                                {
                                    tgFile.Write("lct 1 ryz;\n");
                                    tgFile.Write("lrep 0 1;\n");
                                    tgFile.Write("gct 1 mx {0} my {1};\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
                                    tgFile.Write("grep 1;\n");
                                    tgFile.Write("endpart;\n");
                                    tgFile.Write("\n");
                                }
                                //横向最大时，纵向最后一个为半个胞元结构下
                                else if (jj == 2 * honeycome.num_zong)
                                {
                                    tgFile.Write("lct 2 rzx;rz 180;\n");
                                    tgFile.Write("lrep 1 2;\n");
                                    tgFile.Write("gct 1 mx {0} my {1};\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
                                    tgFile.Write("grep 1;\n");
                                    tgFile.Write("endpart;\n");
                                    tgFile.Write("\n");
                                }
                                //横向最大时，其余为整个胞元结构
                                else
                                {
                                    tgFile.Write("lct 3 rzx;rz 180;ryz;\n");
                                    tgFile.Write("lrep 0:3;\n");
                                    tgFile.Write("gct 1 mx {0} my {1};\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
                                    tgFile.Write("grep 1;\n");
                                    tgFile.Write("endpart;\n");
                                    tgFile.Write("\n");
                                }
                            }
                            //横坐标为奇数时，全部排布完整胞元
                            else
                            {
                                tgFile.Write("lct 3 rzx;rz 180;ryz;\n");
                                tgFile.Write("lrep 0:3;\n");
                                tgFile.Write("gct 1 mx {0} my {1};\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
                                tgFile.Write("grep 1;\n");
                                tgFile.Write("endpart;\n");
                                tgFile.Write("\n");
                            }
                        }
                    }
                }

                tgFile.WriteLine("merge");

                for (int i = 0; i < board.num_up; i++)
                {
                    tgFile.WriteLine("bptol {0} {1} -1", i + 1, i + 2);
                }

                for (int i = board.num_up; i <= board.num_under + board.num_up; i++)
                {
                    tgFile.WriteLine("bptol {0} {1} -1", i, i + 1);
                }

                for (int i = 0; i < cell_sum; i++)
                {
                    tgFile.WriteLine("bptol {0} 1 -1", board.num_under + board.num_up + i + 1);
                    tgFile.WriteLine("bptol {0} {1} -1", board.num_under + board.num_up + i + 1, board.num_up + 1);
                }

                tgFile.WriteLine("stp 0.01");
                //tgFile.Write("lsdyna keyword mof fwb.k\n");
                tgFile.WriteLine("lsdyna keyword mof model.k");
                tgFile.WriteLine("write;");
                tgFile.WriteLine("");
                tgFile.WriteLine("end;");
            }
        }
    }
}