using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test10
{
    //写一个Circle类，包含「半径」属性、求周长和求面积的成员函数，半径使用构造函数设置（可参考7）
    class Yuan
    {
        static void Main(string[] args)
        {
            Circle circle = new Circle(10.00);
            Console.WriteLine($"圆的半径: {circle.radius}");
            Console.WriteLine($"圆的周长: {circle.zhouchang()}");
            Console.WriteLine($"圆的面积: {circle.area()}");
            Console.ReadKey();
        }
    }
    class Circle
    {
        public double radius { get; private set; }
        public Circle(double r)
        {
            radius = r;
        }
        public double zhouchang()
        {
            return 2 * Math.PI * radius;
        }
        public double area()
        {
            return Math.PI * radius * radius;
        }
    }
}
