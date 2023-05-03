using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//我看了一遍代码，简单来讲就是，有3到4个类负责实现小的系统，并且组成了大的系统

//外观模式就是定义一个类，将这几个系统的类的对象放到一块，并把它们的功能封装起来

//这其实就是类似一个操作界面，界面上有许多按钮，我们不需要知道每个按钮的功能背后是怎么实现的，但我们知道每个按钮的功能是什么
//所以叫做外观模式，或者说它是操作系统的门面

namespace _11.C_外观模式
{
    class SystemA
    {
        public void FunctionA()
        { Console.WriteLine("执行A功能"); }
    }
    class SystemB
    { public void FunctionB() { Console.WriteLine("执行功能B"); } }
    class SystemC
    { public void FunctionC() { Console.WriteLine("执行功能C"); } }

    class Facade
    {
        SystemA systemA;
        SystemB systemB;
        SystemC systemC;
        public Facade()
        {
            systemA = new SystemA();
            systemB = new SystemB();
            systemC = new SystemC();
        }

        //这只是最简单的一种情形，实际上并不是直接调用那么简单，是要根据情况来添加其他具体代码的
        public void FunctionOfA() { systemA.FunctionA(); }
        public void FunctionOfB() { systemB.FunctionB(); }
        public void FunctionOfC() { systemC.FunctionC(); }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            //没有外观模式的时候
            SystemA a = new SystemA();
            SystemB b = new SystemB();
            SystemC c = new SystemC();
            a.FunctionA();
            b.FunctionB();
            c.FunctionC();

            //有了外观模式后
            Facade f = new Facade();    
            f.FunctionOfA();
            f.FunctionOfB();
            f.FunctionOfC();
        }
    }
}
