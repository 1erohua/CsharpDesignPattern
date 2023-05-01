using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace TestForDesign
{
    class YinYong
    {
        public string str1;
        public string str2;
        public YinYong(string str1,string str2)
        {
            this.str1 = str1;
            this.str2 = str2;
        }
        public YinYong()
        {
            this.str2 =(string)null;
            this.str1 =(string)null;
        }
    }
    class Test1
    {
        public int x;
        public int y;
        public YinYong test=new YinYong();//这个是引用类型
        public Test1 ShallowClone()
        {
            return (Test1)this.MemberwiseClone();//MenberwiseClone，返回当前实例的浅克隆。
        }
        public Test1()
        {
            this.x = 10;
            this.y = 10;
            this.test.str1 = "hello";
            this.test.str2 = "good";
        }
        public Test1 DeepClone()
        {
            //先创建一个浅克隆
            Test1 test = this.MemberwiseClone() as Test1;
            //然后给引用对象创建新对象，并初始化相同的数值
            test.test = new YinYong(test.test.str1, test.test.str2);
            return test;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Test1 test = new Test1();
            //Test1 test2 = test.ShallowClone();

            //test2.x = 0;
            //test2.y = 0;
            //test2.test.str1 = "no";
            //test2.test.str2= "no";
            //Console.WriteLine(test.y + test.test.str1);

            Test1 test = new Test1();
            Test1 test2=test.DeepClone();

            test2.test.str1 = "no";
            test2.test.str2="no";   
            Console.WriteLine(test.test.str1);
        }
    }
}
