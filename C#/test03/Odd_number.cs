using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test03
{
    class Odd_number
    {
        static void Main(string[] args)
        {
            //求1(含)~99(含)所有奇数的和，用递归实现
            int sum = A(1);
            Console.WriteLine("1(含)~99(含)所有奇数的和: " + sum);
            Console.ReadKey();
        }
        static int A(int n)
        {
            if (n > 99)
            {
                return 0;
            }
            else
            {
                return n + A(n + 2);
            }
            //return n < 1 ? 0 : n + A(n - 2);    ////A(99)
        }
    }
}
