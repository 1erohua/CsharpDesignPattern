using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22.C_访问者模式
{
    //访问者模式适用于数据结构相对稳定的系统
    //它把数据结构和作用于数据结构之上的操作之间的耦合度降低
    //使得操作集合可以相对自由地改变

    //我大概理解了它的意思，就是说当几个基本的数据类型不发生变化的时候
    //通过访问者模式将类型与类型执行的操作分割开
    //以图形为例子，简单来讲有好几种基本图形
    //现在我要绘制一个复杂图形，就需要“基本图形”和“操作基本图形”
    //那么访问者类就是那个复杂图形，它里面的函数就是“操作基本图形”，函数的参数是“基本图形”
    //光说无用，我们直接上代码

    //以画图为例子
    //抽象基本图形
    abstract class Shape
    {
        public Shape(string name)
        {
            Name = name;
        }
        //图形的基本信息（注意，draw函数也是图形的基本信息）
        public string Name { get; set; } //图形的名字
        public abstract void Draw();//在平面上画出这个图形
        //访问者模式实现的关键！每个具体的基本类型都有一个接收访问的函数
        public abstract void AcceptVisitor(Visitor visitor);

    }
    //椭圆
    class Ellipse : Shape
    {
        public Ellipse(string name) : base(name)
        {
        }
        //具体化该操作
        public override void Draw()
        {
            Console.WriteLine("在平面上画出一个椭圆");
        }
        //访问者模式实现的关键！每个具体的基本类型都有一个接收访问的函数
        public override void AcceptVisitor(Visitor visitor)
        {
            visitor.Draw(this);
        }
    }
    //矩形
    class Rectangle : Shape
    {
        public Rectangle(string name) : base(name)
        {
        }
        //具体化该操作
        public override void Draw()
        {
            Console.WriteLine("在平面上画出一个矩形");
        }
        //访问者模式实现的关键！每个具体的基本类型都有一个接收访问的函数
        public override void AcceptVisitor(Visitor visitor)
        {
            visitor.Draw(this);
        }
    }
    //线条
    class Line : Shape
    {
        public Line(string name) : base(name)
        {
        }
        //具体化该操作
        public override void Draw()
        {
            Console.WriteLine("在平面上画出一条线");
        }
        //访问者模式实现的关键！每个具体的基本类型都有一个接收访问的函数
        public override void AcceptVisitor(Visitor visitor)
        {
            visitor.Draw(this);
        }
    }

    //抽象访问者形状（复杂形状的抽象）
    abstract class Visitor
    {
        //要包含每个基本图形的抽象操作
        public abstract void Draw(Ellipse ellipse);
        public abstract void Draw(Rectangle rectangle);
        public abstract void Draw(Line line);
    }
    //具体访问者
    class ConcreteVisitor : Visitor
    {
        //根据不同的复杂图形实现不同的复杂操作，参数负责提供对应基本图形的信息
        public override void Draw(Ellipse ellipse)
        {
            Console.WriteLine("我们先画一个{0}", ellipse.Name);
            ellipse.Draw();
        }
        public override void Draw(Line line)
        {
            Console.WriteLine("我们再画一条{0}",line.Name);
            line.Draw();
        }
        public override void Draw(Rectangle rectangle)
        {
            Console.WriteLine("我们再画一个{0}", rectangle.Name);
            rectangle.Draw();
        }
    }

    //这里我们搞一个工厂类，它和访问者没有多大关系,它负责创建形状类
    class ShapeFactory
    {
        public static Shape CreateShape(string name)
        {
            if (name.Equals("ellipse")) { return new Ellipse("Ellipse"); }
            else if (name.Equals("Rectangle")) { return new Rectangle("Rectangle"); }
            else if (name.Equals("Line")) { return new Line("Line"); }
            else { return null; }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Shape ellipse = ShapeFactory.CreateShape("Ellipse");
            Shape line = ShapeFactory.CreateShape("Line");
            Shape rectangle = ShapeFactory.CreateShape("Rectangle");

            //设置一个复杂图形访问者
            Visitor visitor = new ConcreteVisitor();

            //利用其进行画图，即将访问者放入每个基本元素之中//这个操作我觉得也可以放入其他类执行，毕竟访问者要访问所有的基本元素
            ellipse.AcceptVisitor(visitor);
            line.AcceptVisitor(visitor);
            rectangle.AcceptVisitor(visitor);
        }
    }
}
