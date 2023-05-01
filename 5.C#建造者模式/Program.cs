using System;
using System.Collections.Generic;

namespace _5.C_建造者模式
{
    internal class Director//指挥者：负责指挥建造者
    {
        public void PackageAll(IBuilder builder)
        {
            //指挥者指挥建造员去组装
            builder.BuildPart1();
            builder.BuildPart2();
        }
    }

    internal abstract class IBuilder//建造者：负责对产品进行组装制造（假设产品制造的同一流程相同），并返回组装好的产品
    {
        protected Product product = new Product();//接口不允许定义字段

        public abstract void BuildPart1();

        public abstract void BuildPart2();

        public abstract Product GetThisProduct();

        public void Reset()//如果要创造多个实例，则重置
        {
            product = new Product();
        }
    }

    internal class Product//产品类
    //由于不同的产品族与建造者绑定，
    //这里的产品更代表的是一种预装结构（该填充什么），不同的类别建造者负责建造不同类别的产品
    {
        //以列表表示为组件
        private List<string> combinations = new List<string>();

        public void Add(string combination)
        {
            combinations.Add(combination);
            Console.WriteLine("当前组件数量为" + combinations.Count);
        }

        public void Show()
        {
            foreach (string combination in combinations)
            {
                Console.WriteLine(combination + "已经装好了");
            }
            Console.WriteLine("电脑已经装好了");
        }
    }

    //具体的建造者一：套餐一
    internal class PackageOne : IBuilder
    {
        public override void BuildPart1()
        {
            this.product.Add("制造者一负责的第一个部分");
        }

        public override void BuildPart2()
        {
            this.product.Add("制造者一负责的第二部分");
        }

        public override Product GetThisProduct()
        {
            return this.product;
        }
    }

    //具体的建造者二：套餐二
    internal class PackageTwo : IBuilder
    {
        public override void BuildPart1()
        {
            this.product.Add("制造者二负责的第一部分");
        }

        public override void BuildPart2()
        {
            this.product.Add("制造者二负责的第二部分");
        }

        public override Product GetThisProduct()
        {
            return this.product;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Director boss = new Director();
            PackageOne packageOne = new PackageOne();
            PackageTwo packageTwo = new PackageTwo();

            //指挥者指挥建造者进行组装，此时建造开始进行
            boss.PackageAll(packageOne);
            //获取建造完成后返回的对象
            Product productOne = packageOne.GetThisProduct(); //这里其实应该放入数组的，但为了方便起见，我们随便找个地方给他安家
            //建造完成后就reset一下，重置一下它的实例
            packageOne.Reset();

            boss.PackageAll(packageTwo);
        }
    }
}