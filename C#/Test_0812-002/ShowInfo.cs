using MyCodeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_0812_002
{
    class ShowInfo
    {
        public static void ShowHeroInfo(Hero hero)
        {
            Console.WriteLine("《这是调用ShowInfo类中的ShowHereInfo()方法》");
            Console.WriteLine("名字：" + hero.mingzi);
            Console.WriteLine("性别：" + hero.xingbie);
            Console.WriteLine("年龄：" + hero.nianling);
            Console.WriteLine("基础伤害：" + hero.shanghai);
            Console.WriteLine("基础血量：" + hero.xueliang);
            Console.WriteLine("身高：" + hero.shengao);
            Console.WriteLine("技能1" + hero.jineng1);
            Console.WriteLine("技能1伤害" + hero.jineng1shanghai);
            Console.WriteLine("技能2" + hero.jineng2);
            Console.WriteLine("技能2伤害" + hero.jineng2shanghai);
        }
    }
}
