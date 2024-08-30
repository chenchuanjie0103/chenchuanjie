using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test04
{
    class Lists_maxmin
    {

        static void Main(string[] args)
        {
            //输入10个整数，输出最大数和最小数
            List<int> lists = new List<int>();
            Console.WriteLine("（请输入10个整数）");
            for (int i = 1; i <= 10; i++)
            {
                Console.Write("输入第" + i + "个整数: ");
                if (int.TryParse(Console.ReadLine(), out int list))
                {
                    lists.Add(list);
                }
                else
                {
                    Console.WriteLine("输入错误，请重新输入！");
                    i--;
                }
            }
            int max = lists.Max();
            int min = lists.Min();
            Console.WriteLine("列表内容: " + string.Join(", ", lists));
            Console.WriteLine("最大值是:" + max);
            Console.WriteLine("最小值是:" + min);
            Console.ReadKey();
        }
    }
}
