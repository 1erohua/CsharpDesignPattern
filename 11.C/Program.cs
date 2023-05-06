using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12.C_享元模式
{
    //在软件开发中如果需要生成大量细粒度的类实例来表示数据
    //如果这些实例除了几个参数外基本上都是相同的
    //这时候就可以使用享元模式来大幅度减少需要实例化类的数量

    //相同的、不变的参数我们称为内部参数(这个不变是相对不变，就是变化程度远不如外部参数频繁)
    //不同的、会变的参数我们称为外部参数

    //对于可变的外部参数，我们通过方法将其（可变的外部参数）传递进来处理，这样一来，一个类实例加上多个不同的外部参数，就可以实现多个类实例的功能
    //当然这就会导致系统复杂，因为需要把外部参数传递到本类里面处理，导致本类里面就会有复杂的函数

    //简而言之就是，享元模式就是把多个类实例分解成 一个类实例（包含相同的数据）+多个外部参数
    //而 当要用到某个类实例的时候，就把这 多个外部参数 通过  一个类实例的类内函数传递进来  ，这样就能做到享元模式

    //为了规范享元模式的使用，需要一个享元工厂来进行操作

    internal class Program
    {
        static void Main(string[] args)
        {
            //创建一个享元工厂
            FlyweightFactory flyweightFactory = new FlyweightFactory();

            //定义一个外部数据
            string OutInformation = "这是一个外部数据";

            //接下来进行创建对象，如果这个对象已经存在，那么我们将从工厂中得到这个对象
            Flyweight flyweightA = flyweightFactory.GetFlyweight("键1");
            Flyweight flyweightB = flyweightFactory.GetFlyweight("键4");
            
            //如果不为空，那我们就传入外部数据进行操作
            if(flyweightA != null) { flyweightA.Operation(OutInformation); }

            //如果为空，我们创建这个对象，并把它存入享元工厂中,然后再执行操作
            if(flyweightB != null) { flyweightA.Operation(OutInformation); }
            else 
            { 
                flyweightB = new ConcreteFlyweight("内部数据4");
                flyweightFactory.flyweights.Add("键4", flyweightB);
                flyweightB.Operation(OutInformation);
            }
        }
    }

    //抽象享元类(负责提供一个 传入外部信息进行操作的函数  的接口)
    abstract class Flyweight
    {
        public abstract void Operation(string OutInformation);
    }

    //具体享元类
    class ConcreteFlyweight : Flyweight
    {
        //提供内部的数据
        private string InformationOne;

        public ConcreteFlyweight(string InFor)
        {
            InformationOne = InFor; 
        }

        //接入外部数据进行操作
        public override void Operation(string OutInformation)
        {
            Console.WriteLine("识别外部数据为" + OutInformation + ";内部数据为" + InformationOne);
        }
    }

    //享元工厂
    class FlyweightFactory
    {
        //管理每个享元对象，防止乱跑
        public Dictionary<string,Flyweight>flyweights= new Dictionary<string,Flyweight>();

        //初始化
        public FlyweightFactory()
        {
            flyweights.Add("键1", new ConcreteFlyweight("内部数据1"));
            flyweights.Add("键2", new ConcreteFlyweight("内部数据2"));
            flyweights.Add("键3", new ConcreteFlyweight("内部数据3"));
        }

        //使用键是为了更好的从工厂中获取享元对象
        public Flyweight GetFlyweight(string Name)
        {
            if(flyweights.ContainsKey(Name))
            {
                //这里就是享元模式的核心之一了，如果对象已经存在了，那就不再重复创建了
                return flyweights[Name];
            }
            //如果不存在，那么将返回一个null，告诉主程序，这里没有这个对象
            return null;    
        }

    }

}
