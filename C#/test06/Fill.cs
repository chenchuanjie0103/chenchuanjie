using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test06
{
    class Fill
    {
        static void Main(string[] args)
        {
            //写一个方法生成一个随机长度(20~30)数组，用0~100的随机数填充该数组
            int[] randomArray = GenerateRandomArray();
            Console.WriteLine("Array: " + string.Join(", ", randomArray));
            Console.ReadKey();
        }
        static int[] GenerateRandomArray()
        {
            Random random = new Random();
            int length = random.Next(20, 31);
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(0, 101);
            }
            return array;
        }
    }
}
