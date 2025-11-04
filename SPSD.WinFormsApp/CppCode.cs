//#include <iostream>
//#include <vector>

//using namespace std;

////输入单位cm

//struct Board
//{
//    int num_up;     //上复合板层数
//    int num_under;  //下复合板层数
//    double h_up;    //上复合板厚度
//    double h_under; //下复合板厚度
//    double l;       //复合板横向长
//    double w;		//复合板纵向长
//};

//struct Fwcell
//{

//    double r_in_up;     //胞元内六角上边界边长r
//    double r_out_up;    //胞元外六角上边界边长rw
//    double r_in_under;  //胞元内六角下边界边长R
//    double r_out_under; //胞元外六角下边界边长Rw

//    double Tn;  //胞元厚度dc

//    double He;  //胞元高度hc

//    int num_heng;   //横向胞元数
//    int num_zong;	//纵向胞元数

//};

//struct Point
//{

//    //定义四分之一（右上角）蜂窝胞元平面6个坐标点，从外六角第一个为p1，按顺时针方向定义到p6；
//    double p1;
//    double p2;
//    double p3;
//    double p4;
//    double p5;
//    double p6;

//};

//struct Index
//{
//    vector<double> i_fw;    //横向索引
//    vector<double> j_fw;	//纵向索引
//};


//int main()
//{


//    //定义蜂窝结构参数类型
//    Fwcell honeycome;
//    //定义蜂窝坐标参数类型
//    Point xup;
//    Point yup;
//    Point xunder;
//    Point yunder;

//    Index fw_num;

//    //定义复合板参数类型
//    Board board;
//    //------------------------------------------------------------------------------------------------
//    //------------------------------------------------------------------------------------------------
//    //蜂窝胞元输入参数
//    honeycome.r_in_up = 9;
//    honeycome.r_in_under = 14;
//    honeycome.Tn = 1.5;
//    honeycome.He = 10;
//    honeycome.num_heng = 6;
//    honeycome.num_zong = 5;

//    double edx = 0.4;//网格尺寸，单位mm


//    //------------------------------------------------------------------------------------------------
//    //------------------------------------------------------------------------------------------------
//    //复合板输入参数

//    board.num_up = 6;
//    board.num_under = 38;
//    board.h_up = 0.33;
//    board.h_under = 0.33;





//    //计算参数

//    honeycome.r_out_up = honeycome.r_in_up + 2.0 * honeycome.Tn / pow(3, 0.5);
//    honeycome.r_out_under = honeycome.r_in_under + 2.0 * honeycome.Tn / pow(3, 0.5);


//    //建立胞元中心坐标长度索引
//    for (int ifw_i = 0; ifw_i < honeycome.num_heng + 1; ifw_i++)
//    {
//        double dx = 1.5 * honeycome.r_out_under;

//        fw_num.i_fw.push_back(ifw_i * dx);

//    }

//    for (int jfw_i = 0; jfw_i < 2 * honeycome.num_zong + 1; jfw_i++)
//    {
//        double dy = pow(3, 0.5) * honeycome.r_out_under / 2;

//        fw_num.j_fw.push_back(jfw_i * dy);
//    }

//    board.l = fw_num.i_fw[honeycome.num_heng] + honeycome.r_out_under;
//    board.w = fw_num.j_fw[2 * honeycome.num_zong];

//    double board_mid_l = fw_num.i_fw[2] + honeycome.r_out_under;        //复合板横向加密长度
//    double board_mid_w = fw_num.j_fw[4];                            //复合板纵向加密长度

//    //计算所需四分之一胞元总数
//    int cell_sum = 0;

//    //for (int sum_i = 0; sum_i < honeycome.num_heng + 1; sum_i++)
//    //{

//    //	if (sum_i % 2 == 0) {

//    //		cell_sum += honeycome.num_zong + 1;

//    //	}
//    //	else
//    //	{
//    //		cell_sum += honeycome.num_zong;
//    //	}

//    //}

//    //胞元上坐标点
//    xup.p1 = 0;
//    yup.p1 = pow(3, 0.5) * honeycome.r_out_up / 2.0;
//    xup.p2 = honeycome.r_out_up / 2.0;
//    yup.p2 = pow(3, 0.5) * honeycome.r_out_up / 2.0;
//    xup.p3 = honeycome.r_out_up;
//    yup.p3 = 0;
//    xup.p4 = honeycome.r_in_up;
//    yup.p4 = 0;
//    xup.p5 = honeycome.r_in_up / 2.0;
//    yup.p5 = pow(3, 0.5) * honeycome.r_in_up / 2.0;
//    xup.p6 = 0;
//    yup.p6 = pow(3, 0.5) * honeycome.r_in_up / 2.0;

//    //胞元下坐标点

//    xunder.p1 = 0;
//    yunder.p1 = pow(3, 0.5) * honeycome.r_out_under / 2.0;
//    xunder.p2 = honeycome.r_out_under / 2.0;
//    yunder.p2 = pow(3, 0.5) * honeycome.r_out_under / 2.0;
//    xunder.p3 = honeycome.r_out_under;
//    yunder.p3 = 0;
//    xunder.p4 = honeycome.r_in_under;
//    yunder.p4 = 0;
//    xunder.p5 = honeycome.r_in_under / 2.0;
//    yunder.p5 = pow(3, 0.5) * honeycome.r_in_under / 2.0;
//    xunder.p6 = 0;
//    yunder.p6 = pow(3, 0.5) * honeycome.r_in_under / 2.0;


//    //网格数量计算
//    int edx_tn = honeycome.Tn / edx;                //胞元厚度网格
//    int edx_heng = honeycome.r_in_up * 0.5 / edx;       //胞元横面网格
//    int edx_xie = honeycome.r_in_up / edx;          //胞元斜面网格
//    int edx_he = honeycome.He / edx;                //胞元高度网格

//    int edx_board_mid_l = board_mid_l / edx;            //复合板横向加密区域网格数
//    int edx_board_mid_w = board_mid_w / edx;        //复合板纵向加密区域网格数
//    int edx_board_l = (board.l - board_mid_l) / (3.2 * edx);        //复合板横向网格数
//    int edx_board_w = (board.w - board_mid_w) / (3.2 * edx);        //复合板纵向网格数
//    int edx_board_up_h = board.h_up / edx;                  //上复合板厚度网格数
//    int edx_board_under_h = board.h_under / edx;            //下复合板厚度网格数

//    //------------------------------------------------------------------------------------------------
//    //------------------------------------------------------------------------------------------------
//    //生成文件
//    FILE* tg;
//    errno_t err;
//    err = fopen_s(&tg, "fengwo.tg", "w");
//    if (err != 0 || tg == nullptr)
//    {
//        printf("打开文件错误\n");
//        return 1;
//    }


//    //------------------------------------------------------------------------------------------------
//    //------------------------------------------------------------------------------------------------
//    //上层复合板
//    for (int i = 0; i < board.num_up; i++)
//    {
//        fprintf(tg, "mate %d;\n", i + 1);

//        fprintf(tg, "block 1 2 3;1 2 3;1 2;0 %f %f;0 %f %f;%f %f;\n", board_mid_l, board.l, board_mid_w, board.w, honeycome.He + i * board.h_up, honeycome.He + (i + 1) * board.h_up);
//        fprintf(tg, "mseq i %d %d;\n", edx_board_mid_l - 1, edx_board_l - 1);
//        fprintf(tg, "mseq j %d %d;\n", edx_board_mid_w - 1, edx_board_w - 1);
//        fprintf(tg, "mseq k %d\n", edx_board_up_h - 1);
//        fprintf(tg, "endpart\n");
//        fprintf(tg, "\n");
//        fprintf(tg, "\n");
//        fprintf(tg, "\n");

//    }



//    //------------------------------------------------------------------------------------------------
//    //------------------------------------------------------------------------------------------------
//    //下层复合板
//    for (int i = 0; i < board.num_under; i++)
//    {
//        fprintf(tg, "mate %d;\n", board.num_up + i + 1);

//        fprintf(tg, "block 1 2 3;1 2 3;1 2;0 %f %f;0 %f %f;%f %f;\n", board_mid_l, board.l, board_mid_w, board.w, -i * board.h_under, -(i + 1) * board.h_under);
//        fprintf(tg, "mseq i %d %d;\n", edx_board_mid_l - 1, edx_board_l - 1);
//        fprintf(tg, "mseq j %d %d;\n", edx_board_mid_w - 1, edx_board_w - 1);
//        fprintf(tg, "mseq k %d\n", edx_board_under_h - 1);
//        fprintf(tg, "endpart\n");
//        fprintf(tg, "\n");
//        fprintf(tg, "\n");
//        fprintf(tg, "\n");

//    }


//    //------------------------------------------------------------------------------------------------
//    //------------------------------------------------------------------------------------------------
//    //蜂窝板建模


//    fprintf(tg, "mate %d;\n", board.num_up + board.num_under + 1);
//    for (int ii = 0; ii < honeycome.num_heng + 1; ii++)
//    {
//        for (int jj = 0; jj < 2 * honeycome.num_zong + 1; jj++)
//        {

//            //横纵坐标索引相加为偶数才有蜂窝胞元中心点
//            if ((ii + jj) % 2 == 0)
//            {
//                cell_sum++;
//                fprintf(tg, "block 1 2;1 2 3;1 2;0 %f;0 %f %f;0 0;\n", honeycome.Tn, honeycome.r_in_up, honeycome.r_in_up * 1.5);
//                fprintf(tg, "mseq i %d;\n", edx_tn);
//                fprintf(tg, "mseq j %d %d;\n", edx_xie, edx_heng);
//                fprintf(tg, "mseq k %d;\n", edx_he);

//                fprintf(tg, "pb 2 3 2 2 3 2 xy %f %f\n", xup.p1, yup.p1);
//                fprintf(tg, "pb 2 2 2 2 2 2 xy %f %f\n", xup.p2, yup.p2);
//                fprintf(tg, "pb 2 1 2 2 1 2 xy %f %f\n", xup.p3, yup.p3);
//                fprintf(tg, "pb 1 1 2 1 1 2 xy %f %f\n", xup.p4, yup.p4);
//                fprintf(tg, "pb 1 2 2 1 2 2 xy %f %f\n", xup.p5, yup.p5);
//                fprintf(tg, "pb 1 3 2 1 3 2 xy %f %f\n", xup.p6, yup.p6);

//                fprintf(tg, "pb 2 3 1 2 3 1 xy %f %f\n", xunder.p1, yunder.p1);
//                fprintf(tg, "pb 2 2 1 2 2 1 xy %f %f\n", xunder.p2, yunder.p2);
//                fprintf(tg, "pb 2 1 1 2 1 1 xy %f %f\n", xunder.p3, yunder.p3);
//                fprintf(tg, "pb 1 1 1 1 1 1 xy %f %f\n", xunder.p4, yunder.p4);
//                fprintf(tg, "pb 1 2 1 1 2 1 xy %f %f\n", xunder.p5, yunder.p5);
//                fprintf(tg, "pb 1 3 1 1 3 1 xy %f %f\n", xunder.p6, yunder.p6);

//                fprintf(tg, "sfi 1 2; 1 3; -2;plan 0 0 %f 0 0 1;\n", honeycome.He);
//                fprintf(tg, "sfi 1 2; -3; 1 2;plan 0 0 0 1 0 0;\n");
//                fprintf(tg, "sfi 1 2; -1; 1 2;plan 0 0 0 0 1 0;\n");

//                //横坐标为0时仅有胞元右边二分之一或者四分之一蜂窝
//                if (ii == 0)
//                {
//                    //横纵坐标均为0时，仅有胞元右上角四分之一蜂窝结构
//                    if (jj == 0)
//                    {
//                        fprintf(tg, "endpart;\n");
//                        fprintf(tg, "\n");
//                    }

//                    //纵坐标为最大边界时，仅有右下角四分之一蜂窝结构
//                    else if (jj == 2 * honeycome.num_zong)
//                    {

//                        fprintf(tg, "lct 1 rzx;\n");
//                        fprintf(tg, "lrep 1;\n");
//                        fprintf(tg, "gct 1 mx %f my %f;\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
//                        fprintf(tg, "grep 1;\n");
//                        fprintf(tg, "endpart;\n");
//                        fprintf(tg, "\n");
//                    }
//                    //其余横索引为0时胞元仅右边蜂窝结构
//                    else
//                    {
//                        fprintf(tg, "lct 1 rzx;\n");
//                        fprintf(tg, "lrep 0 1;\n");
//                        fprintf(tg, "gct 1 mx %f my %f;\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
//                        fprintf(tg, "grep 1;\n");
//                        fprintf(tg, "endpart;\n");
//                        fprintf(tg, "\n");
//                    }

//                }
//                //横坐标为偶数时 仅纵向第一个和最后一个为胞元半个上下结构
//                else if (ii % 2 == 0)
//                {
//                    //横向最大时，纵向第一个为半个胞元结构上
//                    if (jj == 0)
//                    {
//                        fprintf(tg, "lct 1 ryz;\n");
//                        fprintf(tg, "lrep 0 1;\n");
//                        fprintf(tg, "gct 1 mx %f my %f;\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
//                        fprintf(tg, "grep 1;\n");
//                        fprintf(tg, "endpart;\n");
//                        fprintf(tg, "\n");
//                    }
//                    //横向最大时，纵向最后一个为半个胞元结构下
//                    else if (jj == 2 * honeycome.num_zong)
//                    {
//                        fprintf(tg, "lct 2 rzx;rz 180;\n");
//                        fprintf(tg, "lrep 1 2;\n");
//                        fprintf(tg, "gct 1 mx %f my %f;\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
//                        fprintf(tg, "grep 1;\n");
//                        fprintf(tg, "endpart;\n");
//                        fprintf(tg, "\n");

//                    }
//                    //横向最大时，其余为整个胞元结构
//                    else
//                    {
//                        fprintf(tg, "lct 3 rzx;rz 180;ryz;\n");
//                        fprintf(tg, "lrep 0:3;\n");
//                        fprintf(tg, "gct 1 mx %f my %f;\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
//                        fprintf(tg, "grep 1;\n");
//                        fprintf(tg, "endpart;\n");
//                        fprintf(tg, "\n");
//                    }
//                }
//                //横坐标为奇数时，全部排布完整胞元
//                else
//                {

//                    fprintf(tg, "lct 3 rzx;rz 180;ryz;\n");
//                    fprintf(tg, "lrep 0:3;\n");
//                    fprintf(tg, "gct 1 mx %f my %f;\n", fw_num.i_fw[ii], fw_num.j_fw[jj]);
//                    fprintf(tg, "grep 1;\n");
//                    fprintf(tg, "endpart;\n");
//                    fprintf(tg, "\n");

//                }


//            }


//        }

//    }


//    fprintf(tg, "merge\n");
//    fprintf(tg, "stp 0.01\n");
//    fprintf(tg, "lsdyna keyword mof fwb.k\n");
//    fprintf(tg, "write\n");
//    fprintf(tg, "\n");
//    fprintf(tg, "\n");
//    fprintf(tg, "\n");
//    fprintf(tg, "\n");
//    fprintf(tg, "\n");
//    fprintf(tg, "\n");






//    return 0;
//}