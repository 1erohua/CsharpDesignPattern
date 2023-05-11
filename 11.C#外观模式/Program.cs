using _11.C_外观模式;
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

    class Façade
    {
        SystemA systemA;
        SystemB systemB;
        SystemC systemC;
        public Façade()
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
            Façade f = new Façade();    
            f.FunctionOfA();
            f.FunctionOfB();
            f.FunctionOfC();
        }
    }
}
//更新
namespace _11_外观模式_进一步举例解释
{
    //以一个文本编辑器来说，它只有三个功能：撤销、重写、保存
    //外观模式就是给出这三个按钮，但不告诉你它们是怎么实现的
    //而且是使用一个外观类给出的这三个按钮
    class Undo
    {
        //这里应该是撤销功能的具体实现
        public void UndoFunction() { Console.WriteLine("实现撤销功能"); }
    }
    class Redo
    {
        //重写功能的具体的实现，偷懒起见我只写一个console
        public void RedoFunction() { Console.WriteLine("实现重写功能"); }
    }
    class Save
    {
        //实现保存功能
        public void SaveFunction() { Console.WriteLine("实现保存功能"); }
    }
    //实现外观类，外观类内负责展现文本编辑器的三个按钮给用户按
    class Façade
    {
        //三个按钮
        Undo undo = new Undo();
        Redo redo = new Redo();
        Save save = new Save();

        //还是那句话，这里只是最简单的方式，实际情况不会这么简单直接调用
        public void FunctionOfUndo() { undo.UndoFunction(); }
        public void FunctionOfRedo() { redo.RedoFunction(); }
        public void FunctionOfSave() { save.SaveFunction(); }
        //主程序就不写了，看到这应该明白了
    }


}