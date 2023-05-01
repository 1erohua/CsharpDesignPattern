using System;
using System.Threading;

namespace _4.C_抽象工厂模式
{
    //前提：一个工厂制造多个产品

    //多个产品
    internal interface IPhones//手机产品
    {
        void MakeIPhone();
    }

    internal interface IPods
    {
        void MakeIPods();
    }

    //华为工厂要做的的耳机与手机
    internal class HuaWeiPhone : IPhones
    {
        public void MakeIPhone()
        {
            Console.WriteLine("华为手机正在制作···");
            Thread.Sleep(1000);
            Console.WriteLine("华为手机制造成功");
        }
    }

    internal class HuaWeiPods : IPods
    {
        public void MakeIPods()
        {
            Console.WriteLine("华为耳机正在制造···");
            Thread.Sleep(1000);
            Console.WriteLine("华为耳机制造成功");
        }
    }

    //小米工厂要做的耳机与手机
    internal class XiaoMiPhone : IPhones
    {
        public void MakeIPhone()
        {
            Console.WriteLine("小米手机正在制造···");
            Thread.Sleep(1000);
            Console.WriteLine("小米手机制造成功");
        }
    }

    internal class XiaoMiPods : IPods
    {
        public void MakeIPods()
        {
            Console.WriteLine("小米耳机正在制造");
            Thread.Sleep(1000);
            Console.WriteLine("小米耳机制造成功过");
        }
    }

    //抽象工厂
    internal interface IFactoryOfPhoneAndIPods
    {
        IPods GetPods();

        IPhones GetPhones();
    }

    //制造华为产品的工厂：华为工厂
    internal class HuaWeiFactory : IFactoryOfPhoneAndIPods
    {
        public IPhones GetPhones()
        {
            return new HuaWeiPhone();
        }

        public IPods GetPods()
        {
            return new XiaoMiPods();
        }
    }

    //制造小米产品的工厂
    internal class XiaoMiFactory : IFactoryOfPhoneAndIPods
    {
        public IPhones GetPhones()
        {
            return new XiaoMiPhone();
        }

        public IPods GetPods()
        {
            return new XiaoMiPods();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            //说几点感悟
            //1.首先是继承、接口、子类与父类的问题
            //若父类定义并用子类构造，那么该对象的核心还是对应的子类，在这几个工厂中体现得比较明显的是
            //工厂中，获取产品的方法，它的返回值类型是父类，但返回的时候是返回一个子类

            //2.直接return new 构造函数
            //我现在一想，这真的是一个很高明的做法，精彩绝伦，甚至说，它就是工厂模式们的核心
            //如果我在具体工厂类内定义一个产品的对象，并且在函数中new一下，再将这个对象返回去
            //那么由于类内的成员和类的实例绑定，我们只创建了一个工厂实例，也就只能创建一个类内成员
            //实际上我们希望一个工厂实例就能创建很多个新的产品
            //那么这时候 return new 构造函数 就是个绝佳的想法，每次返回都会重新构造一个新的产品对象实例
        }
    }
}