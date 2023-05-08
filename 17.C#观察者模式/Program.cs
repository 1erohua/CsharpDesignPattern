using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17.C_观察者模式
{
    //观察者模式定义了一种一对多的依赖关系，让多个观察者对象同时监听某一个主题对象，这个主题对象在状态发生变化时，会通知所有观察者对象，使它们能够自动更新自己的行为。

    //微信订阅公众号为例子

    //抽象观察者与具体观察者（观众）
    abstract class Observer
    {
        //观察者的信息
        public string Name { get; set; }
        public string UID { get; set; }
        public Observer(string Name, string uid)
        {
            this.Name = Name;
            this.UID = uid;
        }

        //接收函数
        public abstract void Receive(Publisher publisher);
    }
    class Subscriber : Observer
    {
        public Subscriber(string Name, string uid) : base(Name, uid)
        {
        }

        public override void Receive(Publisher publisher)
        {
            Console.WriteLine("您订阅的" + publisher.Name + "已更新");
        }
    }

    //抽象被观察者和具体被观察者
    abstract class Publisher
    {
        //订阅号的信息
        public String Name { get; set; }

        //订阅者的列表
        List<Observer> _observers = new List<Observer>();

        //增加订阅
        public void AddObserver(Observer observer)
        {
            _observers.Add(observer);
        }
        //删除订阅
        public void RemoveObserver(Observer observer)
        {
            _observers.Remove(observer);
        }
        //发送更新
        public void Update()
        {
            foreach (Observer observer in _observers)
            {
                observer.Receive(this);
            }
        }
    }

    class WeiXin : Publisher
    {
        public WeiXin(string name)
        {
            base.Name = name;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Observer observer = new Subscriber("zerohua","1111");

            Publisher publisher = new WeiXin("微信运动");

            //让observer加入订阅
            publisher.AddObserver(observer);
            //更新，通知
            publisher.Update();
        }
    }
}
