using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class LibraryManagement
    {
        static void Main()
        {
            ListBooks();
            Selection();
        }
        static void Selection()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine("\n\n==========图书管理系统==========");
                Console.WriteLine("1.添加图书");
                Console.WriteLine("2.删除图书");
                Console.WriteLine("3.查询图书");
                Console.WriteLine("4.借阅图书");
                Console.WriteLine("5.归还图书");
                Console.WriteLine("6.显示所有库存信息");
                Console.WriteLine("0.退出");
                Console.Write("请选择：");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Addition();
                        break;
                    case "2":
                        Deletion();
                        break;
                    case "3":
                        Search();
                        break;
                    case "4":
                        Checkout();
                        break;
                    case "5":
                        Return();
                        break;
                    case "6":
                        Information();
                        break;
                    case "0":
                        check = false;
                        break;
                    default:
                        Console.WriteLine("输入错误，请重新输入！");
                        break;
                }
            }            
        }
        static List<Library> lists = new List<Library>();
        static void ListBooks()
        {
            lists.Add(new Library("水浒传", "施耐庵", 6));
            lists.Add(new Library("三国演义", "罗贯中", 7));
            lists.Add(new Library("西游记", "吴承恩", 4));
            lists.Add(new Library("红楼梦", "曹雪芹", 5));
            lists.Add(new Library("狂人日记", "鲁迅", 2));
            lists.Add(new Library("白鹿原", "陈忠实", 3));
            lists.Add(new Library("尘埃落定", "阿来", 1));
            lists.Add(new Library("活着", "余华", 9));
            lists.Add(new Library("我与地坛", "史铁生", 10));
            lists.Add(new Library("围城", "钱钟书", 8));
        }
        static void Addition()
        {
            string name;
            string author;
            int number;
            Console.WriteLine("\n《请输入要添加的图书信息》");
            while (true)
            {
                Console.Write("书名：");
                if ((name = Console.ReadLine()) != null && name.Trim().Length > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("请重新输入（书名不能为空）");
                }
            }
            while (true)
            {
                Console.Write("作者：");
                if ((author = Console.ReadLine()) != null && author.Trim().Length > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("请重新输入（书名不能为空）");
                }
            }
            while (true)
            {
                Console.Write("库存数量：");
                if (int.TryParse(Console.ReadLine(), out number) && number > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("请重新输入（库存数量必须为正整数）");
                }
            }
            Library same = lists.Find(x => x.Name == name && x.Author == author);
            if (same == null)
            {
                Library book = new Library(name, author, number);
                lists.Add(book);
                Console.WriteLine("添加图书成功！");
            }
            else
            {
                int number_new = int.Parse(same.Number);
                number_new += number;
                same.Number = number_new.ToString();
                Console.WriteLine("图书存在，库存数量已更新！");
            }
        }
        static void Deletion()
        {
            string name;
            Console.WriteLine("\n《请输入要删除的图书信息》");
            while (true)
            {
                Console.Write("书名：");
                if ((name = Console.ReadLine()) != null && name.Trim().Length > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("请重新输入（书名不能为空）");
                }
            }
            Library book = lists.Find(x => x.Name == name);
            if (book != null)
            {
                Console.WriteLine("找到图书：{0}", book.Name);
                while (true)
                {
                    Console.Write("是否删除（Y/N）：");
                    string answer = Console.ReadLine()?.Trim().ToUpper();
                    if (answer == "Y")
                    {
                        lists.Remove(book);
                        Console.WriteLine("删除图书成功！");
                        break;
                    }
                    else if (answer == "N")
                    {
                        Console.WriteLine("取消操作！");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("操作失败，请重新输入！");
                    }
                }
            }
            else
            {
                Console.WriteLine("未找到图书！");
            }
        }
        static void Search()
        {
            string name;
            Console.WriteLine("\n《请输入要查询的图书信息》");
            while (true)
            {
                Console.Write("书名/作者：");
                if ((name = Console.ReadLine()) != null && name.Trim().Length > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("请重新输入（不能为空）");
                }
            }
            Library book = lists.Find(x => x.Name == name || x.Author == name);
            if (book != null)
            {
                Console.WriteLine("------> 查询如下:");
                Console.WriteLine(book);
            }
            else
            {
                Console.WriteLine("未找到图书！");
            }
        }   // 借阅系统都以查询为基础
        static void Checkout()
        {
            string name;
            int number;
            Console.WriteLine("\n《请输入要借阅的图书信息》");
            while (true)
            {
                Console.Write("书名/作者：");
                if ((name = Console.ReadLine()) != null && name.Trim().Length > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("请重新输入（不能为空）");
                }
            }
            while (true)
            {
                Console.Write("请输入借阅数量（至少一本）：");
                if (int.TryParse(Console.ReadLine(), out number) && number > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("输入错误，请输入正整数！");
                }
            }
            Library book = lists.Find(x => x.Name == name || x.Author == name);
            if (book != null)
            {
                if (int.TryParse(book.Number, out int number_new) && number_new >= number)
                {
                    number_new -= number;
                    book.Number = number_new.ToString();
                    book.BorrowCount += number;
                    book.Borrow = true;
                    Console.WriteLine($"成功借阅图书：{book.Name}，库存：{book.Number}，已借：{book.BorrowCount}本");
                }
                else
                {
                    Console.WriteLine("借阅数量多于库存，无法借阅！");
                }
            }
            else
            {
                Console.WriteLine("未找到图书！");
            }            
        }
        static void Return()
        {
            string name;
            int number;
            Console.WriteLine("\n《请输入要归还的图书信息》");
            while (true)
            {
                Console.Write("书名/作者：");
                if ((name = Console.ReadLine()) != null && name.Trim().Length > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("请重新输入（不能为空）");
                }
            }
            while (true)
            {
                Console.Write("请输入归还数量（至少一本）：");
                if (int.TryParse(Console.ReadLine(), out number) && number > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("输入错误，请输入正整数！");
                }
            }
            Library book = lists.Find(x => x.Name == name || x.Author == name);
            if (book != null)
            {
                if (int.TryParse(book.Number, out int number_new) && number <= book.BorrowCount)
                {
                    number_new += number;
                    book.Number = number_new.ToString();
                    book.BorrowCount -= number;
                    if (book.BorrowCount > 0)
                    {
                        Console.WriteLine($"成功归还图书：{book.Name}，库存：{book.Number}，已借：{book.BorrowCount}本");

                    }
                    else
                    {
                        book.Borrow = false;
                    }
                }
                else
                {
                    Console.WriteLine("归还数量多于已借，归还失败！");
                }
            }
            else
            {
                Console.WriteLine("未找到图书！");
            }
        }
        static void Information()
        {
            Console.WriteLine("\n《显示所有库存信息》");
            for (int i = 0; i < lists.Count; i++)
            {
                Console.WriteLine(lists[i]);
            }
        }

    }
    public class Library
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Number { get; set; }
        public bool Borrow { get; set; }
        public int BorrowCount { get; set; }
        public Library(string name, string author, int number, bool borrow=false)
        {
            Name = name;
            Author = author;
            Number = number.ToString();
            Borrow = borrow;
            BorrowCount = 0;
        }
        public override string ToString()
        {
            string status = Borrow ? $"已借[{BorrowCount}本]" : "未借";
            string result = $"书名：{Name,-20}\t作者：{Author,-20}\t库存数量：{Number,-10}\t状态：{status}";
            return result;
        }
    }
}