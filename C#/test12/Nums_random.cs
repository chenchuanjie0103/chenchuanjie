using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test12
{
    //12.写一个Nums类，输出一个随机数组（可参考6）
    //函数：
    //    构造函数：初始化数组为随机长度(20~30)，使用0~100的随机数填满
    //    成员函数：刷新数组为随机长度(20~30)，使用0~100的随机数填满
    //    ToString函数：以可读性字符串返回该数组的内容“[1,3,7,5,......]”
    class Nums_random
    {
        static void Main(string[] args)
        {
            Nums nums = new Nums();
            Console.WriteLine("初始化数组：" + string.Join(", ", nums));
            nums.Refresh();
            Console.WriteLine($"刷新数组并以可读性字符串返回内容：{nums}");
            Console.ReadKey();
        }
    }
    public class Nums
    {
        private int[] array;
        private Random random = new Random(); // 将 Random 实例作为类的成员变量
        public Nums()
        {
            RefreshArray();
        }
        private void RefreshArray()
        {
            int length = random.Next(20, 31);
            array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(0, 101);
            }
        }
        public void Refresh()
        {
            RefreshArray(); // 调用刷新方法
        }
        public override string ToString()
        {
            return "[" + string.Join(", ", array) + "]";
        }
    }
}