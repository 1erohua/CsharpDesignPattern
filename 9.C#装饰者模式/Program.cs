using System;

namespace _9.C_装饰者模式
{

    //我缕一缕它的思路
    //抽象被装饰者、具体被装饰者
    //抽象装饰者，具体装饰

    //抽象装饰者继承自抽象被装饰者（这个思路就很神奇）

    abstract class Phone//抽象被装饰者
    {
        public abstract void Print();
    }
    class ApplePhone : Phone//具体被装饰者
    {
        public override void Print()
        {
            Console.WriteLine("这里是苹果手机");
        }
    }

    //抽象装饰者
    abstract class Decorator : Phone
    {
        Phone phone;
        public Decorator(Phone phone)
        {
            this.phone = phone;
        }

        public override void Print()
        {
            Console.WriteLine();
            phone.Print();
            Console.WriteLine("对手机执行装饰操作");
            Console.WriteLine();
        }
    }
    class PingMu : Decorator
    {
        public PingMu(Phone phone) : base(phone)
        {
        }
        //添加新材料
        public override void Print()
        {
            base.Print();//这个写法太逆天了。。。在原来父类的函数的基础上增加东西，只需重写函数，然后第一行调用base.Print();
            Console.WriteLine("已经为手机加装屏幕");
        }
    }
    class ShouJiKe : Decorator
    {
        public ShouJiKe(Phone phone) : base(phone)
        {
        }
        public override void Print()
        {
            base.Print();
            Console.WriteLine("已经为手机加装了手机外壳");
        }
    }




    internal class Program
    {
        static void Main(string[] args)
        {
            //给手机加装屏幕
            Phone IPhone10 = new ApplePhone();
            Decorator GetPingMu10 = new PingMu(IPhone10);
            GetPingMu10.Print();

            //给手机假装手机壳
            Phone IPhone11 = new ApplePhone();
            Decorator ShouJiKe11 = new ShouJiKe(IPhone11);
            ShouJiKe11.Print();

            //给手机同时假装屏幕和外壳
            Phone IPhone14 = new ApplePhone();
            Decorator GetPingMuAndShouJiKe = new PingMu(IPhone14);
            GetPingMuAndShouJiKe = new ShouJiKe(GetPingMuAndShouJiKe);
            GetPingMuAndShouJiKe.Print();
        }
    }
}
