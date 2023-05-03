using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _2.C_简单工程模式
{
    internal interface IMakingCars
    {
        //所有产品的抽象的同一制作流程
        void GetCollar();

        void Making();
    }

    //两个不同的产品的具体制作过程
    internal class _RedCar : IMakingCars
    {
        public void Making()
        { Console.WriteLine("this is Red car"); }

        public void GetCollar()
        { }
    }

    internal class _BlueCar : IMakingCars
    {
        public void Making()
        { Console.WriteLine("this a Blue Car"); }

        public void GetCollar()
        { }
    }

    //创建一个产品工厂，负责为这些派生的产品创造实例
    internal class CarFactory
    {
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
            CarFactory factory = new CarFactory();

            //造出红色车
            IMakingCars blueCar = factory.MakingCars("Blue");
            IMakingCars redcars = factory.MakingCars("Red");

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