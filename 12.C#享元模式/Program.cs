using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12.C_享元模式
{
    //享元模式就是说，当有很多类实例而且这些类实例有相当一部分的元素是相同的
    //这时候把相同的元素分割出来，作为享元模式的内部元素；不同的元素的作为外部元素，通过享元类的函数传递进享元类操作

    //这里以水杯和饮料为例子，我们有很多饮料，水杯只有几个（好几种饮料是用一种水杯装的）
    //如果为了每个饮料而创建一个水杯实例，那就会很浪费内存
    //将水杯和饮料分隔开，把饮料作为外部参数，水杯作为内部参数

    //享元抽象类，提供一个 传入外部信息进行操作的接口//抽象水杯
    abstract class Flyweight_WaterBottle
    {
        public abstract void Operation(string OutInfor);
    }

    //具体抽象类
    class ConcreteFlyweight_WaterBottle : Flyweight_WaterBottle
    {
        string InInfor_Name;
        public ConcreteFlyweight_WaterBottle(string inInfor_Name)
        {
            InInfor_Name = inInfor_Name;
        }
        //实现具体函数
        public override void Operation(string OutInfor)
        {
            Console.WriteLine("将饮料" + OutInfor + "装入水杯" + InInfor_Name);
        }
    }

    class FlyFactory_WaterBottle
    {
        //定义一个水杯的字典
        //如果主函数要用的水杯是在我们这个字典里面的，那我们直接返回给他
        //如果不是，就在主函数用Bottles自动添加吧，毕竟它是公开的
        //其实更好的设计应该是，键值对中键值同名，这样就可以在这个工厂里面将字典设置为私有
        public Dictionary<string, Flyweight_WaterBottle> Bottles = new Dictionary<string, Flyweight_WaterBottle>();

        //获取一个水杯
        public Flyweight_WaterBottle GetBottle(string name)
        {
            if (Bottles.ContainsKey(name)) return Bottles[name];
            else return null;
            //如果键值对同名
            //else那里可以这样写
            //Flyweight_WaterBottle Comtent = new ConcreteFlyweight(name);
            //Bottle.Add(name,Comtent)
            //return Comtent ;
        }

        //初始化
        public FlyFactory_WaterBottle()
        {
            Bottles.Add("水杯1", new ConcreteFlyweight_WaterBottle("水杯1"));
            Bottles.Add("水杯2", new ConcreteFlyweight_WaterBottle("水杯2"));
        }
    }




    internal class Program
    {
        static void Main(string[] args)
        {
            //创建一个享元工厂
            FlyFactory_WaterBottle aFactory = new FlyFactory_WaterBottle();

            //创建一种饮料
            string OutInfor_Drink = "Colo";

            Flyweight_WaterBottle fa = aFactory.GetBottle("水杯1");//字典里面有的
            Flyweight_WaterBottle fc = aFactory.GetBottle("水杯3"); ;//字典里面没有的

            if (fa != null) { fa.Operation(OutInfor_Drink); }

            if (fc != null) { fc.Operation(OutInfor_Drink); }
            else
            {
                fc = new ConcreteFlyweight_WaterBottle("水杯3");
                aFactory.Bottles.Add("水杯3", fc);
                fc.Operation(OutInfor_Drink);
            }
        }
    }
}
