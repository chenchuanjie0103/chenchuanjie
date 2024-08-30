using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test15
{
    //写一个int类型的单向LinkedList，有Add、Count、get、isEmpty，delete(下标)，insert
    public class LinkedList_int
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            Console.WriteLine("链表: " + list); // 输出: [1, 2, 3]

            list.Insert(1, 4);
            Console.WriteLine("插入4到下标1: " + list); // 输出: [1, 4, 2, 3]

            list.Delete(2);
            Console.WriteLine("删除下标2的元素: " + list); // 输出: [1, 4, 3]

            Console.WriteLine("元素数量: " + list.Count); // 输出: 3
            Console.WriteLine("下标1的元素: " + list.Get(1)); // 输出: 4

            Console.WriteLine("链表是否为空: " + list.IsEmpty); // 输出: False
            Console.ReadKey();
        }
    }
    public class LinkedList
    {
        private Node head;
        private int count;

        // 节点类
        private class Node
        {
            public int Data;
            public Node Next;

            public Node(int data)
            {
                Data = data;
                Next = null;
            }
        }

        // 添加元素到链表末尾
        public void Add(int data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            count++;
        }

        // 获取链表中元素的数量
        public int Count
        {
            get { return count; }
        }

        // 获取指定下标的元素
        public int Get(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.");
            }
            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current.Data;
        }

        // 判断链表是否为空
        public bool IsEmpty
        {
            get { return count == 0; }
        }

        // 删除指定下标的元素
        public void Delete(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.");
            }

            if (index == 0)
            {
                head = head.Next;
            }
            else
            {
                Node current = head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                current.Next = current.Next.Next;
            }
            count--;
        }

        // 在指定下标插入新元素
        public void Insert(int index, int data)
        {
            if (index < 0 || index > count)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.");
            }

            Node newNode = new Node(data);
            if (index == 0)
            {
                newNode.Next = head;
                head = newNode;
            }
            else
            {
                Node current = head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }
                newNode.Next = current.Next;
                current.Next = newNode;
            }
            count++;
        }

        // 打印链表
        public override string ToString()
        {
            Node current = head;
            string result = "[";
            while (current != null)
            {
                result += current.Data;
                if (current.Next != null)
                {
                    result += ", ";
                }
                current = current.Next;
            }
            result += "]";
            return result;
        }
    }
}
