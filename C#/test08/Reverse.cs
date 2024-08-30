using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test08
{
    class Reverse
    {
        static void Main(string[] args)
        {
            //输入字符串返回逆序
            Console.Write("请输入字符串：");
            string input = Console.ReadLine();

            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            string reversed = new string(charArray);

            Console.WriteLine("逆序后字符串：" + reversed);
            Console.ReadKey();
        }
    }
}
