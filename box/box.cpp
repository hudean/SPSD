// box.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
using namespace std;
//int main()
//{
//    std::cout << "Hello World!\n";
//}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门使用技巧: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件


//输入单位cm


struct Box
{
	double charge_i;		//装药箱体的x方向长度
	double charge_j;		//装药箱体的y方向长度
	double charge_k;		//装药箱体的z方向长度
	double nocharge_j;		//非装药箱体的y方向长度

	double opening_i;		//箱体开口x方向长度
	double opening_j;		//箱体开口y方向长度

};

struct Edx
{
	//网格尺寸cm
	double edx;
	double edx_open;//开坑网格尺寸cm，网格尺寸的三分之一

};


void writeFile(Box box, Edx medx) {
	//生成文件
	FILE* tg;
	errno_t err;
	err = fopen_s(&tg, "box.tg", "w");
	if (err != 0 || tg == nullptr)
	{
		printf("打开文件错误\n");
		return;
	}

	int num_i = box.charge_i / medx.edx - 1;
	int num_ii = box.opening_i / medx.edx_open - 1;
	int num_j = (box.charge_j / 2.0) / medx.edx - 1;
	int num_jj = (box.opening_j / 2.0) / medx.edx_open - 1;
	int num_no_j = box.nocharge_j / medx.edx - 1;
	int num_k = box.charge_k / medx.edx - 1;



	fprintf(tg, "mate 2;\n");
	fprintf(tg, "block -1 2 3;-1 2 3 -4 -5;-1 -2;-%f -%f 0;-%f -%f %f %f %f;-%f %f;\n",
		box.charge_i, box.opening_i, box.charge_j / 2.0, box.opening_j / 2.0, box.opening_j / 2.0, box.charge_j / 2.0,
		box.charge_j / 2.0 + box.nocharge_j, box.charge_k / 2.0, box.charge_k / 2.0);
	fprintf(tg, "dei 2 3; 2 3; -2;\n");
	fprintf(tg, "mseq i %d %d;\n", num_i, num_ii);
	fprintf(tg, "mseq j %d %d %d %d;\n", num_j, num_jj, num_j, num_no_j);
	fprintf(tg, "mseq k %d;\n", num_k);
	fprintf(tg, "mti 1 3; 4 5; 1 2;1;\n");
	fprintf(tg, "endpart;\n");
	fprintf(tg, "merge\n");
	fprintf(tg, "stp 0.01;\n");
	fprintf(tg, "lsdyna keyword mof  001model.k;\n");
	fprintf(tg, "write;\n");
	fprintf(tg, "end;\n");


	fclose(tg);

	printf("文件生成成功！！！\n");

}

int main() {

	Box box;
	Edx medx;

	box.charge_i = 150;
	box.charge_j = 200;
	box.charge_k = 150;
	box.nocharge_j = 80;
	box.opening_i = 5;
	box.opening_j = 10;

	medx.edx = 3;
	medx.edx_open = medx.edx / 3.0;




	writeFile(box, medx);


	return 0;
}