using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13.C_代理模式
{
    //以桌面的快捷方式为例子
    internal class Program
    {
        static void Main(string[] args)
        {
            //创建一个实例
            AbExeApplication steam = new Steam();
            //为Steam创建一个快捷方式
            Steam_ShortCut steam_ShortCut = new Steam_ShortCut(steam);
            //双击快捷方式
            steam_ShortCut.Operation();
        }
    }

    abstract class AbExeApplication//应用的抽象方式
    {
        public abstract void Operation();
    }

    class Steam:AbExeApplication//具体的应用方式(以steam为例子)
    {
        public override void Operation()
        {
            Console.WriteLine("启动！正在运行该程序！");
        }
    }

    class Steam_ShortCut : AbExeApplication//steam的快捷方式
    {
        //创建一个引用，该引用将被具体的应用赋值
        AbExeApplication UseForShortCut;
        //为引用赋值
        public Steam_ShortCut(AbExeApplication useForShortCut)
        {
            if(useForShortCut!=null) { UseForShortCut = useForShortCut; }
            else
            {
                Console.WriteLine("传入对象为空，将根据已有的类随机操作");//我乱说的，别当真
                UseForShortCut = new Steam();
            }
            
        }

        //调用该应用程序的引用中的函数
        public override void Operation()
        {
            UseForShortCut.Operation();
        }
    }

}
