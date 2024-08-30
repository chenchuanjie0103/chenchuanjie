using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test11
{
    //写一个测试方法，实例化Circle类，设置「半径」属性，求周长和面积
    class Yuan_new
    {
        static void Main(string[] args)
        {
            Test();
        }
        static void Test()
        {
            Circle circle = new Circle();
            circle.radius = 100.00;
            double zhouchang = circle.zhouchang();
            double area = circle.area();
            Console.WriteLine($"圆的半径: {circle.radius}");
            Console.WriteLine($"圆的周长: {zhouchang}");
            Console.WriteLine($"圆的面积: {area}");
            Console.ReadKey();
        }
    }
    class Circle
    {
        public double radius;
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
