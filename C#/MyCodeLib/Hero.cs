using System;

namespace MyCodeLib
{
    public class Hero
    {
        //static
        public string mingzi = "";
        public string xingbie = null;
        public int nianling;
        public int shanghai;
        public int xueliang;
        public int shengao;
        public string jineng1;
        public int jineng1shanghai;
        public string jineng2;
        public int jineng2shanghai;


        public void ShowInfo()
        {
            Console.WriteLine("《这是调用Hero类中的ShowInfo()方法》");
            Console.WriteLine("名字：" + mingzi);
            Console.WriteLine("性别：" + xingbie);
            Console.WriteLine("年龄：" + nianling);
            Console.WriteLine("基础伤害：" + shanghai);
            Console.WriteLine("基础血量：" + xueliang);
            Console.WriteLine("身高：" + shengao);
            Console.WriteLine("技能1" + jineng1);
            Console.WriteLine("技能1伤害" + jineng1shanghai);
            Console.WriteLine("技能2" + jineng2);
            Console.WriteLine("技能2伤害" + jineng2shanghai);
        }
    }
}
