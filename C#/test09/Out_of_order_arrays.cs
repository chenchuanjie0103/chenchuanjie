using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test09
{
    class Out_of_order_arrays
    {
        static void Main(string[] args)
        {
            //生成一个100个元素的乱序数组包含0~99的每个数字且只出现一次
            var numbers = Enumerable.Range(0, 100).OrderBy(x => Guid.NewGuid()).ToArray();
            Console.WriteLine("乱序数组:");
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + " ");
                if ((i + 1) % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
    }
}
