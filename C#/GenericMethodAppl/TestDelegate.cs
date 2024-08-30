using System;
using System.IO;
using GenericDelegateAppl.DelegateAppl;

delegate T NumberChanger<T>(T n);   //泛型（Generic）委托（Delegate）
namespace GenericDelegateAppl
{
    class TestDelegate
    {
        static int num = 10;
        public static int AddNum(int p)
        {
            num += p;
            return num;
        }
        public static int MultNum(int q)
        {
            num *= q;
            return num;
        }
        public static int getNum()
        {
            return num;
        }
        public static void TestDelegate1()
        {
            // 创建委托实例
            NumberChanger<int> nc1 = new NumberChanger<int>(AddNum);
            NumberChanger<int> nc2 = new NumberChanger<int>(MultNum);
            // 使用委托对象调用方法
            nc1(25);
            Console.WriteLine("Value of Num: {0}", getNum());
            nc2(5);
            Console.WriteLine("Value of Num: {0}", getNum());
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            TestDelegate1();

            PrintString.DelegateAppl1();

            MulticastDelegateExample.MulticastDelegateExample1 mde = new MulticastDelegateExample.MulticastDelegateExample1();
            mde.MulticastDelegateExample2();

            GenericApplication.Tester gal = new GenericApplication.Tester();
            gal.GenericApplication1();
        }
    }
    namespace DelegateAppl
    {
        class PrintString
        {
            static FileStream fs;
            static StreamWriter sw;
            // 委托声明
            public delegate void printString(string s);

            // 该方法打印到控制台
            public static void WriteToScreen(string str)
            {
                Console.WriteLine("The String is: {0}", str);
            }
            // 该方法打印到文件
            public static void WriteToFile(string s)
            {
                fs = new FileStream(@"C:\Users\admin\Desktop\C#\GenericMethodAppl\message.txt", FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine(s);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            // 该方法把委托作为参数，并使用它调用方法
            public static void sendString(printString ps)
            {
                ps("Hello World");
            }
            public static void DelegateAppl1()
            {
                printString ps1 = new printString(WriteToScreen);
                printString ps2 = new printString(WriteToFile);
                sendString(ps1);
                sendString(ps2);
                Console.ReadKey();
            }
        }
    }
}

//委托对象可使用 "+" 运算符进行合并。一个合并委托调用它所合并的两个委托。
//只有相同类型的委托可被合并。"-" 运算符可用于从合并的委托中移除组件委托。
//使用委托的这个有用的特点，您可以创建一个委托被调用时要调用的方法的调用列表。这被称为委托的 多播（multicasting），也叫组播。
namespace MulticastDelegateExample
{
    // 定义一个委托，指定它可以指向无返回值的方法
    public delegate void NotifyDelegate(string message);
    class MulticastDelegateExample1
    {
        public void MulticastDelegateExample2()
        {
            // 创建委托实例，并将其绑定到多个方法
            NotifyDelegate notify = Notify;
            notify += NotifyUppercase;//使用 += 操作符将 NotifyUppercase 方法添加到 notify 委托的调用列表中

            // 调用多播委托
            notify("Hello, Multicast Delegate!");

            Console.ReadKey();
        }

        // 第一个方法
        static void Notify(string message)
        {
            Console.WriteLine("Notify: " + message);
        }

        // 第二个方法
        static void NotifyUppercase(string message)
        {
            Console.WriteLine("NotifyUppercase: " + message.ToUpper());
        }
    }
}

namespace GenericApplication    //泛型（Generic）
{
    public class MyGenericArray<T>
    {
        private T[] array;
        public MyGenericArray(int size)
        {
            array = new T[size + 1];//避免越界访问（如果你从 0 到 size 进行访问，实际需要 size + 1 的空间）
        }
        public T getItem(int index)
        {
            return array[index];
        }
        public void setItem(int index, T value)
        {
            array[index] = value;
        }
    }

    class Tester
    {
        static void A()
        {
            // 声明一个整型数组
            MyGenericArray<int> intArray = new MyGenericArray<int>(5);//数组大小为 5（实际分配了 6 个位置）
            // 设置值
            for (int c = 0; c < 5; c++)
            {
                intArray.setItem(c, c * 5);
            }
            // 获取值
            for (int c = 0; c < 5; c++)
            {
                Console.Write(intArray.getItem(c) + " ");
            }
            Console.WriteLine();
            // 声明一个字符数组
            MyGenericArray<char> charArray = new MyGenericArray<char>(5);//数组大小为 5（实际分配了 6 个位置）
            // 设置值
            for (int c = 0; c < 5; c++)
            {
                charArray.setItem(c, (char)(c + 97));//将整数 c 转换为相应的 ASCII 字符：'a' 的 ASCII 值是 97，'b' 的 ASCII 值是 98，以此类推。。。
            }
            // 获取值
            for (int c = 0; c < 5; c++)
            {
                Console.Write(charArray.getItem(c) + " ");
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        public void B()
        {
            int a, b;
            char c, d;
            a = 10;
            b = 20;
            c = 'I';
            d = 'V';

            // 在交换之前显示值
            Console.WriteLine("Int values before calling swap:");
            Console.WriteLine("a = {0}, b = {1}", a, b);
            Console.WriteLine("Char values before calling swap:");
            Console.WriteLine("c = {0}, d = {1}", c, d);

            // 调用 swap
            Swap<int>(ref a, ref b);
            Swap<char>(ref c, ref d);

            // 在交换之后显示值
            Console.WriteLine("Int values after calling swap:");
            Console.WriteLine("a = {0}, b = {1}", a, b);
            Console.WriteLine("Char values after calling swap:");
            Console.WriteLine("c = {0}, d = {1}", c, d);
            Console.ReadKey();
        }
        public void GenericApplication1()
        {
            A();
            Tester X = new Tester();
            X.B();
        }
    }
}