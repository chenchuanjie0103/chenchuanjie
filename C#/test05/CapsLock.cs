using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test5
{
    class CapsLock
    {
        static void Main(string[] args)
        {
            //输入一段字符，将大写转为小写，小写转为大写
            Console.WriteLine("请输入一段字符: ");
            string word = Console.ReadLine();
            string result = A(word);
            Console.WriteLine($"大小写转换后: {result}");
            Console.ReadKey();
        }

        static string A(string input)
        {
            char[] chars = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsUpper(chars[i]))
                    chars[i] = char.ToLower(chars[i]);
                else if (char.IsLower(chars[i]))
                    chars[i] = char.ToUpper(chars[i]);
            }
            return new string(chars);
        }
    }
}