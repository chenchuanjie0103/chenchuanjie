using System;
//这个示例展示了如何定义事件、订阅事件、触发事件、处理事件。
//EventTest 类定义了一个事件 ChangeNum，并在 SetValue 方法中根据条件触发该事件。
//subscribEvent 类定义了处理事件的方法 printf，并在 MainClass 中将这个方法注册到 ChangeNum 事件上，从而实现事件的处理。
namespace SimpleEvent
{
    /***********1.发布器类***********/      //《用于定义事件和触发事件》
    public class EventTest
    {
        private int value;
        public delegate void NumManipulationHandler();
        public event NumManipulationHandler ChangeNum;//事件允许订阅者注册和处理事件
        protected virtual void OnNumChanged()
        {
            if (ChangeNum != null)  //有订阅者
            {
                Console.WriteLine("1");
                ChangeNum(); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey();
            }
        }
        public EventTest()
        {
            Console.WriteLine("2");

            int n = 5;
            SetValue(n);
        }
        public void SetValue(int n)
        {
            if (value != n)
            {
                Console.WriteLine("3");

                value = n;
                OnNumChanged(); //触发事件
            }
        }
    }
    /***********2.订阅器类***********/      //《用于处理事件触发时的操作》
    public class subscribEvent
    {
        public void printf()
        {
            Console.WriteLine("event fire");
            Console.ReadKey();
        }
    }
    /***********3.触发***********/    //《程序的入口点，用于创建实例和演示事件的订阅和触发》
    public class MainClass
    {
        public static void Main()
        {
            Console.WriteLine("4");

            EventTest e = new EventTest(); /* 实例化对象,第一次没有触发事件 */


            Console.WriteLine("5");

            subscribEvent v = new subscribEvent(); /* 实例化对象 */

            Console.WriteLine("6");

            e.ChangeNum += new EventTest.NumManipulationHandler(v.printf); /* 创建 subscribEvent 的实例 v，并将其 printf 方法注册为 ChangeNum 事件的处理方法 */
           
            Console.WriteLine("7");

            e.SetValue(7);


            Console.WriteLine("8");

            e.SetValue(11);

            Console.WriteLine("9");

        }
    }
}
