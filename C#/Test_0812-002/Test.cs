using MyCodeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_0812_002
{
    class Test
    {
        //调试：最左侧按住断点，F10跳过断点调试，F11进入断点调试
        static void Main()
        {
            HelloWorldApplication.A hwa = new HelloWorldApplication.A();
            hwa.ab();

            Prime_number();

            Console.WriteLine("请输入大侠名称：");
            Hero hrMan = new Hero();
            hrMan.mingzi = Console.ReadLine();
            //实例 实例化
            hrMan.xingbie = "男";
            hrMan.nianling = 18;
            hrMan.shanghai = 100;
            hrMan.xueliang = 100;
            hrMan.shengao = 175;
            hrMan.jineng1 = "飞檐走壁1.0";
            hrMan.jineng1shanghai = 20;
            hrMan.jineng2 = "飞沙走石1.0";
            hrMan.jineng2shanghai = 30;
            //hrMan.ShowInfo();
            ShowInfo.ShowHeroInfo(hrMan);

            Console.WriteLine("请输入女侠名称：");
            Hero hr = new Hero();
            hr.mingzi = Console.ReadLine();
            //实例 实例化
            hr.xingbie = "女";
            hr.nianling = 18;
            hr.shanghai = 100;
            hr.xueliang = 100;
            hr.shengao = 175;
            hr.jineng1 = "飞檐走壁1.0";
            hr.jineng1shanghai = 40;
            hr.jineng2 = "飞沙走石1.0";
            hr.jineng2shanghai = 60;
            hrMan.ShowInfo();
            //ShowInfo.ShowHeroInfo(hr);



            string result = "你的计算结果是：";
            int n = 100;
            int m = 200;
            int sum;
            sum = n + m;
            Console.WriteLine(result + sum);

            int age = 0;
            bool ischeck = true;
            for(; ischeck;)
            {
                Console.WriteLine("\n请输入你的年龄：");
                string str = Console.ReadLine();
                //int age = int.Parse(str) + 20;
                //string age = str + 20.ToString();
                try
                {
                    age = int.Parse(str);
                    ischeck = false;
                }
                catch
                {
                    Console.WriteLine("请输入一个正确的年龄（必须是数字）");
                    //return;
                }
            }
            age += 20;
            Console.WriteLine("你二十年后的年龄是：" + age.ToString());

            GetUserInfo();
            Console.ReadKey();
        }

        public static void Prime_number()
        {
            /* 局部变量定义 */
            int i, j;
            Console.WriteLine("100以内的质数有：");
            for (i = 2; i < 100; i++)
            {
                for (j = 2; j <= (i / j); j++)
                    if ((i % j) == 0) break; // 如果找到，则不是质数
                if (j > (i / j))
                    Console.WriteLine("{0} 是质数", i);
            }
            Console.ReadKey();
        }

        public static void GetUserInfo()     //可以在类外访问
        {
            Console.WriteLine("请输入你的姓名：");
            string name = Console.ReadLine();
            string Name = ChangeData(name);

            Console.WriteLine("请输入你的故乡：");
            string address = Console.ReadLine();
            Console.WriteLine("你的名字是：" + Name);
            Console.WriteLine("你的故乡是：" + address);
        }
        static string ChangeData(string XXX)
        {
            //bool isCheck = name == "张三";
            //if(isCheck)
            if (XXX == "张三")
            {
                Console.WriteLine("《你输入的是张三》");
                XXX = "法外狂徒张三";
            }
            else if (XXX == "李四")
            {
                Console.WriteLine("《你输入的是李四》");
                XXX = "无情铁手李四";
            }
            else
            {
                Console.WriteLine("《你输入的名字不满足条件，默认王五》");
                XXX = "铁血柔情王五";
            }
            return XXX;
        }

    }
}

namespace HelloWorldApplication
{
    class B
    {
        enum Day { Sun, Mon, Tue, Wed, Thu, Fri, Sat };
        public void AAA()
        {
            int x = (int)Day.Sun;
            int y = (int)Day.Fri;
            Console.WriteLine("Sun = {0}", x);
            Console.WriteLine("Fri = {0}", y);
        }
    }
    class A
    {
        public static void BBB()
        {
            var a = new Dictionary<int, int>();
            a.Add(12, 14);//键值对
            a.Add(0, 1);
            Console.WriteLine("删去前的Count" + a.Count);
            a.Remove(0);
            Console.WriteLine(a[12]);//访问键12，输出值14
            Console.WriteLine(a.Count);
            Console.WriteLine(a.ContainsKey(12));
            Console.ReadKey();
        }
        public void ab()
        {
            B X = new B();
            X.AAA();
            BBB();
        }
    }
}