using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20.C_策略者模式
{
    //建议先了解状态模式之后再去看策略者模式

    //我们先复习一下状态模式，状态模式是对状态进行抽象化处理
    //那么策略模式就是对策略，即算法进行抽象化处理

    //策略模式是针对一组算法，将每个算法封装到具有公共接口的独立的类中，从而使它们可以相互替换。
    //策略模式使得算法可以在不影响到客户端的情况下发生变化。
    //“将每个算法封装到不同的策略类中，使得它们可以互换”

    //嘛这个其实很简单的，我看了一遍，就是在玩抽象类与具体类的本质呗，不过玩的过程用了一个角色包装起来了
    //由于这个封装，你并不会知道对象是以哪种策略进行计算的

    //以不同年龄段的票价折扣为例子
    //抽象策略类
    abstract class Strategy
    {
        public abstract double DiscountCalculate(double money);
    }
    //具体策略类
    class NotEnough18 : Strategy//8折//18岁以下
    {
        public override double DiscountCalculate(double money)
        {
            return money * (0.8);
        }
    }
    class Over18_NotEnough60 : Strategy//成人无优惠
    {
        public override double DiscountCalculate(double money)
        {
            return money;
        }
    }
    class Over60 : Strategy//老人5折
    {
        public override double DiscountCalculate(double money)
        {
            return 0.5 * money;
        }
    }
    //策略者负责管理
    class StrategyOperation
    {
        //必须有一个策略的引用
        public Strategy strategy;
        public StrategyOperation(Strategy strategy)
        {
            this.strategy = strategy;
        }
        //使用策略//使用一个类进行封装
        public void Calculate(double money)
        {
            strategy.DiscountCalculate(money);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //注意这种构造方法的使用，就可以不用在主程序构造一个NotEnough18的对象了（这句话怎么有点奇怪）
            StrategyOperation strategyOperation1 = new StrategyOperation(new NotEnough18());//不够18
            StrategyOperation strategyOperation2 = new StrategyOperation(new Over18_NotEnough60());//大过18小过60
            StrategyOperation strategyOperation3 = new StrategyOperation(new Over60());//大过60

        }
    }
}
