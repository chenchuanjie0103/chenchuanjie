using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test08
{
    class Word_01
    {
        static void Main(string[] args)
        {
            //输入一个字母，判断是大写字母还是小写字母
            bool check = true;
            for (; check; )
            {
                Console.WriteLine("请输入一个字母：");
                char word = Console.ReadKey().KeyChar;
                if (word >= 'A' && word <= 'Z')
                {
                    Console.WriteLine("该字母为大写字母");
                    check = false;
                }
                else if (word >= 'a' && word <= 'z')
                {
                    Console.WriteLine("该字母为小写字母");
                    check = false;
                }
                else
                {
                    Console.WriteLine("输入的不是字母，请重新输出！");
                }
            }
            Console.ReadKey();
        }
    }
}
