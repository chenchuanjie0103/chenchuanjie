using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test07
{
    class Circle
    {
        static void Main(string[] args)
        {
            //输入半径求圆的周长和面积
            bool check = true;
            for (; check;)
            {
                Console.Write("请输入圆的半径: ");
                double radius;
                if (double.TryParse(Console.ReadLine(), out radius))
                {
                    double zhouchang = 2 * Math.PI * radius;
                    double area = Math.PI * radius * radius;

                    Console.WriteLine($"圆的周长: {zhouchang}");
                    Console.WriteLine($"圆的面积: {area}");
                    check = false;
                }
                else
                {
                    Console.WriteLine("输入错误，请重新输入！");
                }
            }
            Console.ReadKey();
        }
    }
}
