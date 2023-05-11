using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//首先我们要认识一个函数，叫MemberwiseClone以及浅拷贝和深拷贝的概念
//拷贝
namespace _6.C_单例模式
{
    abstract class Zero
    {
        public int x;
        public int y;
        public abstract Zero ZeroClone();
    }
    //类一可能要添加新的东西，类二搞不好也是如此
    class One : Zero
    {
        public double s;
        public override Zero ZeroClone()
        {
            return (One)MemberwiseClone();
        }
        public One()
        {
            s = 11.0;
        }
    }
    class Two : Zero
    {
        public string l;
        public override Zero ZeroClone()
        {
            return (Two)MemberwiseClone();
        }
        public Two()
        {
            l = "hello";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //我们用抽象类定义变量，然后用已存在的实例进行克隆
            One one = new One();
            Two two = new Two();

            //我们想要复制一个One
            Zero zero = one.ZeroClone();
            //然后复制一个Two
            Zero zero2 = two.ZeroClone();
            Console.WriteLine(((One)zero).s);
        }
    }
}
