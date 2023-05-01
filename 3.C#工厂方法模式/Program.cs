using System;

namespace _3.C_工厂方法模式
{
    internal interface IMakingCars
    {
        //所有产品的抽象的同一制作流程
        void Making();
    }

    //两个不同的产品的具体制作过程(可能会有差别)
    internal class _Lamborghini : IMakingCars
    {
        public void Making()
        { Console.WriteLine("Lamborghini Is Ready!"); }
    }

    internal class _Mercedes_Benz : IMakingCars
    {
        public void Making()
        { Console.WriteLine("this a Mercedes-Benz"); }
    }

    //与简单工厂模式不同——创建一个抽象工厂应该做的事情，然后再细分到不同车的时候，小工厂该怎么做
    internal interface ICarFactory
    {
        IMakingCars GetACar();
    }

    //这是两个小工厂，这样一来当需要扩展的时候，只需要添加两个类（一个是该车的类，另一个就是制造该车的工厂类）
    internal class _Lamborghini_CarFactory : ICarFactory
    {
        public IMakingCars GetACar()
        {
            return new _Lamborghini();
        }
    }

    internal class _Mercedes_Benz_CarFactory : ICarFactory
    {
        public IMakingCars GetACar()
        {
            return new _Mercedes_Benz();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            ICarFactory carFactory;
            string str = "兰博基尼";
            if (str.Equals("兰博基尼"))
            {
                carFactory = new _Lamborghini_CarFactory();
            }
            else if (str.Equals("梅赛德斯奔驰"))
            {
                carFactory = new _Mercedes_Benz_CarFactory();
            }
            else
            {
                carFactory = null;
            }

            //只要知道是什么工厂，就能返回什么车辆了(产品的具体类只是用来被工厂创造实例的时候返回，不会用来直接创建实例)
            IMakingCars cars = carFactory.GetACar();
            cars.Making();
        }
    }
}

//可扩展，适用于同一类产品的扩展（同是汽车、同是鸭脖）
//产品抽象类——同一类产品的抽象类——到每个产品的具体类
//工厂抽象类——同一个工厂的抽象类——到每个工厂的具体类
//每个具体工厂类负责创建对应的一个或几个具体产品类的实例