using System;
using System.IO;
//本实例提供一个简单的用于热水锅炉系统故障排除的应用程序。当维修工程师检查锅炉时，锅炉的温度和压力会随着维修工程师的备注自动记录到日志文件中
namespace BoilerEventAppl
{
    // 1. Boiler 类
    class Boiler
    {
        public int Temp { get; private set; }
        public int Pressure { get; private set; }

        public Boiler(int temp, int pressure)
        {
            Temp = temp;
            Pressure = pressure;
        }
    }

    // 2. 事件发布器
    class DelegateBoilerEvent
    {
        public delegate void BoilerLogHandler(string status);   //定义了一个委托，用于处理字符串类型的日志消息

        public event BoilerLogHandler BoilerEventLog;        // 基于上面的委托定义事件，用于发布锅炉状态日志

        public void LogProcess()
        {
            string remarks = "O.K.";
            Boiler boiler = new Boiler(100, 12);//创建一个 Boiler 实例
            int temp = boiler.Temp;
            int pressure = boiler.Pressure;

            if (temp > 150 || temp < 80 || pressure < 12 || pressure > 15)
            {
                remarks = "Need Maintenance";
            }

            OnBoilerEventLog($"Logging Info:\nTemperature: {temp}\nPressure: {pressure}\nMessage: {remarks}\n");
        }
        //protected:访问修饰符，表示成员（方法或属性）只能在当前类和其派生类中访问
        protected void OnBoilerEventLog(string message)
        {
            BoilerEventLog?.Invoke(message);//触发 BoilerEventLog 事件，传递日志消息
        }
    }

    // 3. 该类保留写入日志文件的条款
    class BoilerInfoLogger : IDisposable
    {
        //接受一个文件名，初始化 StreamWriter，用于将日志写入指定的文件（字段在对象构造期间只能被赋值一次）
        private readonly StreamWriter _streamWriter;

        public BoilerInfoLogger(string filename)
        {
            _streamWriter = new StreamWriter(new FileStream(filename, FileMode.Append, FileAccess.Write));
        }//构造函数

        public void Logger(string info)
        {
            _streamWriter.WriteLine(info);
        }//写入日志信息到一个文件

        public void Dispose()
        {
            _streamWriter?.Close();// ?. 是空条件运算符用于在 BoilerEventLog 事件不为空时调用 Invoke 方法
        }
    }

    // 4. 事件订阅器
    public class RecordBoilerInfo
    {
        static void Logger(string info)
        {
            Console.WriteLine(info);
        }//将日志信息输出到控制台

        static void Main(string[] args)
        {
            using (BoilerInfoLogger fileLogger = new BoilerInfoLogger("C:\\Users\\admin\\Desktop\\C#\\BoilerEventAppl\\boiler.txt"))
            {
                DelegateBoilerEvent boilerEvent = new DelegateBoilerEvent();
                boilerEvent.BoilerEventLog += Logger;
                boilerEvent.BoilerEventLog += fileLogger.Logger;
                boilerEvent.LogProcess();
            }

            Console.ReadKey();


            //1.Main 方法中，创建 BoilerInfoLogger 实例并将其用于写入日志文件。
            //2.创建 DelegateBoilerEvent 实例并将 Logger 和 fileLogger.Logger 方法注册到 BoilerEventLog 事件。
            //3.调用 boilerEvent.LogProcess() 处理锅炉状态。如果温度和压力在正常范围内，将记录 "O.K."，否则记录 "Need Maintenance"。
            //4.事件触发时，Logger 方法输出日志到控制台，fileLogger.Logger 方法将日志写入文件。
            //5.使用 using 语句确保 BoilerInfoLogger 实例在完成后正确关闭文件。
        }
    }
}
