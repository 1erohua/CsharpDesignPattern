using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9.C_装饰者模式
{

    //我缕一缕它的思路
    //抽象被装饰者、具体被装饰者
    //抽象装饰者，具体装饰

    //抽象装饰者继承自抽象被装饰者（这个思路就很神奇）

    abstract class Phone
    {
        public abstract void Print();
    }
    class ApplePhone : Phone
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



    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
