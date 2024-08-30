using System;
using System.IO;
using System.Text;

namespace FileApplication
{
    internal class File
    {
        public static void A()
        {
            FileStream F = new FileStream("Test-FileApplicationbinDebug.dat",
            FileMode.OpenOrCreate, FileAccess.ReadWrite);//相对路径,可能是项目的输出目录（例如 bin/Debug 或 bin/Release）
            for (int i = 1; i <= 20; i++)
            {
                F.WriteByte((byte)i);
            }

            F.Position = 0;//将文件流的位置设置为开头

            for (int i = 0; i <= 20; i++)
            {
                Console.Write(F.ReadByte() + " ");
            }
            Console.WriteLine();
            F.Close();
            Console.ReadKey();
        }           //文件的输入与输出

        public static void B()
        {
            //写入文件
            string[] names = new string[] { "Zara Ali", "Nuha Ali" };
            using (StreamWriter sw = new StreamWriter("C:\\Users\\admin\\Desktop\\C#\\FileApplication\\names.txt"))
            {
                foreach (string s in names)
                {
                    sw.WriteLine(s);
                }
            }
            // 从文件中读取并显示每行
            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader                
                using (StreamReader sr = new StreamReader(@"C:\Users\admin\Desktop\C#\FileApplication\names.txt", Encoding.UTF8))
                {
                    string line = "";
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }           //文本文件的读写

        public static void BinaryFileApplication()
        {
            BinaryWriter bw;
            BinaryReader br;
            int i = 25;
            double d = 3.14157;
            bool b = true;
            string s = "I am happy";
            // 尝试实例化,创建二进制文件
            try
            {
                //相对路径,可能是项目的输出目录（例如 bin/Debug 或 bin/Release）
                bw = new BinaryWriter(new FileStream("binaryfile.bin", FileMode.Create));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot create file.");
                return;
            }
            // 尝试写入二进制文件
            try
            {
                bw.Write(i);
                bw.Write(d);
                bw.Write(b);
                bw.Write(s);//写入四行，每次写入一行
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot write to file.");
                return;
            }
            bw.Close();
            // 尝试打开该二进制文件
            try
            {
                br = new BinaryReader(new FileStream("binaryfile.bin", FileMode.Open));
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot open file.");
                return;
            }
            // 尝试读取该二进制文件
            try
            {
                //按顺序读取数据，这里的读取方式对应了之前的存储方式，并且按顺序操作
                i = br.ReadInt32();
                Console.WriteLine("Integer data: {0}", i);
                d = br.ReadDouble();
                Console.WriteLine("Double data: {0}", d);
                b = br.ReadBoolean();
                Console.WriteLine("Boolean data: {0}", b);
                s = br.ReadString();
                Console.WriteLine("String data: {0}", s);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot read from file.");
                return;
            }
            br.Close();
            Console.ReadKey();
        }           //二进制文件的读写
        static void Main(string[] args)
        {
            A();
            B();    
            BinaryFileApplication();
            TraverseFile.TraverseFile1 tf = new TraverseFile.TraverseFile1();
            tf.TraverseFile2();
        }
    }
}

namespace TraverseFile
{
    class TraverseFile1
    {
        public void TraverseFile2()
        {
            string path;
            while (true)
            {
                Console.Write("请输入路径：");
                path = Console.ReadLine();
                if (Directory.Exists(path))
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("文件详情列表：");
                    SearchFile(path);
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.Error.WriteLine("路径不存在！\n");
                }
            }
        }           //Windows 文件系统的操作
        public void SearchFile(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);//用于表示指定路径的目录
            FileInfo[] fi = di.GetFiles();//获取目录下的所有文件
            foreach (FileInfo f in fi)
            {
                Console.WriteLine($"文件名：{f.Name,-60}文件大小：{f.Length}");
            }
            Console.WriteLine();
            DirectoryInfo[] dList = di.GetDirectories();//获取目录下的所有子目录
            foreach (DirectoryInfo d in dList)
            {
                SearchFile(d.FullName);//对每个子目录调用 SearchFile 方法，以实现递归遍历
            }
        }
    }
}
