using System;

delegate void NumberChanger(int n);
namespace LambdaAndDelegate
{
    class TestLambdaDelegate
    {
        static int num = 10;
        public static void AddNum(int p)
        {
            num += p;
            Console.WriteLine("Named Method: {0}", num);
        }

        public static void MultNum(int q)
        {
            num *= q;
            Console.WriteLine("Named Method: {0}", num);
        }

        static void Main(string[] args)
        {
            // 1.使用匿名方法创建委托实例
            NumberChanger nc1 = delegate (int x)
            {
                Console.WriteLine("Anonymous Method: {0}", x);
            };
            // 2.使用 lambda 表达式创建委托实例
            NumberChanger nc2 = x => Console.WriteLine($"Lambda Expression: {x}");



            // 使用匿名方法调用委托
            nc1(10);

            // 使用命名方法实例化委托
            nc1 = new NumberChanger(AddNum);

            // 使用命名方法调用委托
            nc1(5);

            // 使用另一个命名方法实例化委托
            nc1 = new NumberChanger(MultNum);

            // 使用命名方法调用委托
            nc1(2);
            Console.ReadKey();
        }
    }
}
