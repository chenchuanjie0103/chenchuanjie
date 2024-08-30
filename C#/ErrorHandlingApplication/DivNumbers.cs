using System;
//异常处理
namespace ErrorHandlingApplication
{
    class DivNumbers
    {
        int result;
        DivNumbers()
        {
            result = 0;
        }
        public void division(int num1, int num2)
        {
            try
            {
                result = num1 / num2;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Exception caught:\n\n{0}\n", e);
            }
            finally
            {
                Console.WriteLine("Result: {0}", result);
            }
        }
        public static void A()
        {
            DivNumbers d = new DivNumbers();
            d.division(25, 0);
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            A();
            UserDefinedException.TestTemperature tt = new UserDefinedException.TestTemperature();
            tt.TestTemperature1();
        }
    }
}

//创建用户自定义异常
namespace UserDefinedException
{
    class TestTemperature
    {
        public void TestTemperature1()
        {
            Temperature temp = new Temperature();
            try
            {
                temp.showTemp();
            }
            catch (TempIsZeroException e)
            {
                Console.WriteLine("TempIsZeroException: {0}", e.Message);
            }
            Console.ReadKey();
        }
    }
    //自定义异常类
    public class TempIsZeroException : ApplicationException
    {
        public TempIsZeroException(string message) : base(message)
        {
        }
    }
    public class Temperature
    {
        int temperature = 0;
        public void showTemp()
        {
            if (temperature == 0)
            {
                throw (new TempIsZeroException("Zero Temperature found"));
            }
            else
            {
                Console.WriteLine("Temperature: {0}", temperature);
            }
        }
    }
}