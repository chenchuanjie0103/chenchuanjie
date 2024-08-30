using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test13
{
    //Nums类添加排序算法：选择排序和冒泡排序
    class Nums_sort
    {
        static void Main(string[] args)
        {
            Nums nums = new Nums();
            Console.WriteLine("初始化数组：" + string.Join(", ", nums));
           
            nums.Refresh();
            Console.WriteLine($"刷新数组并以可读性字符串返回内容：{nums}");

            nums.select_sort();
            Console.WriteLine($"选择排序后的数组：{nums}");
            
            nums.Refresh();            
            nums.bubble_sort();
            Console.WriteLine($"冒泡排序后的数组：{nums}");
            Console.ReadKey();
        }
    }
    public class Nums
    {
        private Random random = new Random(); // 将 Random 实例作为类的成员变量
        private int[] array;
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

        // 选择排序算法
        public void select_sort()
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    int temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
        }
        // 冒泡排序算法
        public void bubble_sort()
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }
}
