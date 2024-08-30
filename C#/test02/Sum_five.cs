using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test02
{
    class Sum_five
    {
        static void Main(string[] args)
        {
            //求0 ~200(含) 所有个位数为5的数的和
            int sum = 0;
            for (int i = 0; i <= 200; i++)
            {
                if (i % 10 == 5)
                {
                    sum += i;
                }
            }
            Console.WriteLine("0 ~200(含) 所有个位数为5的数的和：" + sum);
            Console.ReadKey();
        }
    }
}
