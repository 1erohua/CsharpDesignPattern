using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14.C_模板方法模式
{
    //这个就很简单了，就是把父类抽象类中的一些实现留到子类中实现
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    //这里我们以PPT为例子
    abstract class PPT_template
    {
        //PPT排版
        public void Format()
        {
            Console.WriteLine("这里是已经写好的PPT排版");
        }
        //PPT风格
        public void Style()
        {
            Console.WriteLine("这里是已经做好的PPT风格");
        }
        //PPT内容
        public abstract void Content();

        //PPT展示
        public void show()
        {
            this.Content();
            this.Style();
            this.Format();
        }
    }

    //具体的PPT
    class PPT_Concrete_A : PPT_template
    {
        public override void Content()
        {
            Console.WriteLine("这里是小组A写的PPT的内容");
        }
    }

    class PPT_Concrete_B : PPT_template
    {
        public override void Content()
        {
            Console.WriteLine("这里是小组B写的PPT的内容");
        }
    }
}
