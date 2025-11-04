using System.Text;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string path = "D:\\Program Files\\ANSYS Inc\\v231\\ansys\\bin\\winx64\\LSDYNA_dp.exe";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@$"mpiexec -np 8 -aa -a ""{path}"" ^");


            string str = sb.ToString();

            Console.WriteLine(str);

            int length = 10000;
            for (int i = 0; i < length; i++)
            {
                await Task.Delay(500);
                Console.WriteLine("这是一行输出内容{0}",i);
            }
            Console.WriteLine("所有输出完成。按任意键退出。");
            Console.ReadKey();
        }
    }
}
