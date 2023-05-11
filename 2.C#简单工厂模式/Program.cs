using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//简单工厂模式就是通过一个工厂类，返回同一接口的不同子类的对象
//这些对象一般是由接口 统一定义的，当这些接口对象被不同子类构造后（或者赋值），它们就能使用 子类继承的接口的功能
namespace _2.C_简单工厂模式
{
    internal interface IMakingCars//这是产品的同一个接口
    {
        //所有产品的抽象的同一制作流程
        void GetCollar();

        void Making();
    }

    //两个不同的产品的具体制作过程
    internal class _RedCar : IMakingCars//这是两个不同的产品
    {
        public void Making()
        { Console.WriteLine("this is Red car"); }

        public void GetCollar()
        { }
    }

    internal class _BlueCar : IMakingCars//这两个不同的产品都来自于同一个接口
    {
        public void Making()
        { Console.WriteLine("this a Blue Car"); }

        public void GetCollar()
        { }
    }

    //创建一个产品工厂，负责为这些派生的产品创造实例
    internal class CarFactory//工厂类负责返回这些接口的子类的实例
    {
        //由于是简单工厂模式，所以这里其实并不合适拓展
        public IMakingCars MakingCars(string collar)
        {
            switch (collar)
            {
                case "Blue": return new _BlueCar();//
                case "Red": return new _RedCar();
            }

            return null;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            //创建一个工厂实例
            CarFactory factory = new CarFactory();
            //这些对象一般是由接口 统一定义的
            IMakingCars blueCar = factory.MakingCars("Blue");
            IMakingCars redcars = factory.MakingCars("Red");
            //当这些接口对象被不同子类构造后（或者赋值），它们就能使用 子类继承的接口的功能
            if (blueCar != null)
            {
                blueCar.Making();
            }
            if (redcars != null)
            {
                redcars.Making();
            }
        }
    }
}