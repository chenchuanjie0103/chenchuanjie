using System;
//基类的初始化
namespace RectangleApplication
{
    class Rectangle
    {
        // 成员变量
        protected double length;
        protected double width;

        // 构造函数
        public Rectangle(double l, double w)
        {
            length = l;
            width = w;
        }
        public double GetArea()
        {
            return length * width;
        }
        public void Display()
        {
            Console.WriteLine("长度： {0}", length);
            Console.WriteLine("宽度： {0}", width);
            Console.WriteLine("面积： {0}", GetArea());
        }
    }
    class Tabletop : Rectangle
    {
        private double cost;

        // 构造函数
        public Tabletop(double l, double w) : base(l, w)
        { }
        public double GetCost()
        {
            double cost;
            cost = GetArea() * 70;
            return cost;
        }

        // 重写 Display 方法
        public void Display()
        {
            base.Display(); // 调用基类 Rectangle 的 Display 方法
            Console.WriteLine("成本： {0}", GetCost());
        }
    }
    class ExecuteRectangle
    {
        static void Main(string[] args)
        {
            Tabletop t = new Tabletop(4.5, 7.5);
            t.Display();
            Console.ReadLine();

            InheritanceApplication.RectangleTester rt = new InheritanceApplication.RectangleTester();
            rt.RT();
        }
    }
}

//C# 多重继承
namespace InheritanceApplication
{
    // 基类 Shape
    class Shape
    {
        protected int width;
        protected int height;
        public void setWidth(int w)
        {
            width = w;
        }
        public void setHeight(int h)
        {
            height = h;
        }
    }

    // 基类 PaintCost
    public interface PaintCost
    {
        int getCost(int area);
    }

    // 派生类
    class Rectangle : Shape, PaintCost
    {
        public int getArea()
        {
            return (width * height);
        }
        public int getCost(int area)
        {
            return area * 70;
        }
    }

    class RectangleTester
    {
        public void RT()
        {
            Rectangle Rect = new Rectangle();
            Rect.setWidth(5);
            Rect.setHeight(7);
            int area = Rect.getArea();
            // 打印对象的面积
            Console.WriteLine("总面积： {0}", Rect.getArea());
            Console.WriteLine("油漆总成本： ${0}", Rect.getCost(area));
            Console.ReadKey();
        }
    }
}