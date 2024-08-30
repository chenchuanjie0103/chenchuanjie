using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test14
{
    //输出该二叉树的前序中序后序遍历
    class Program
    {
        static void Main(string[] args)
        {
            Node Tree = getTree();

            Console.WriteLine("前序遍历:");
            PreOrder(Tree);
            Console.WriteLine();
            Console.WriteLine("中序遍历:");
            InOrder(Tree);
            Console.WriteLine();
            Console.WriteLine("后序遍历:");
            PostOrder(Tree);
            Console.WriteLine();
            Console.ReadLine();
        }
        static Node getTree()
        {
            return new Node("A",
                new Node("B",
                    null,
                    new Node("D",
                        new Node("G"),
                        null)
                    ),
                new Node("C",
                    new Node("E",
                        null,
                        new Node("H")
                        ),
                    new Node("F")
                    )
                );
        }
        // 前序遍历：根 -> 左 -> 右      A B D G C E H F
        static void PreOrder(Node node)
        {
            if (node != null)
            {
                Console.Write(node.value + " ");
                PreOrder(node.leftChild);
                PreOrder(node.rightChild);
            }
        }
        // 中序遍历：左 -> 根 -> 右      B G D A E H C F
        static void InOrder(Node node)
        {
            if (node != null)
            {
                InOrder(node.leftChild);
                Console.Write(node.value + " ");
                InOrder(node.rightChild);
            }
        }
        // 后序遍历：左 -> 右 -> 根      G D B H E F C A
        static void PostOrder(Node node)
        {
            if (node != null)
            {
                PostOrder(node.leftChild);
                PostOrder(node.rightChild);
                Console.Write(node.value + " ");
            }        
        }
    }

    class Node
    {
        public string value;
        public Node leftChild;
        public Node rightChild;
        public Node() { }
        public Node(string value)
        {
            this.value = value;
        }
        public Node(string value, Node leftChild, Node rightChild)
        {
            this.value = value;
            this.leftChild = leftChild;
            this.rightChild = rightChild;
        }
        public override string ToString()
        {
            string leftStr = leftChild == null ? "null" : leftChild.ToString();
            string rightStr = rightChild == null ? "null" : rightChild.ToString();
            return $"[Node:{value},left:{leftStr},right:{rightStr}";
        }
    }
}
