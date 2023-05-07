using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16.C_迭代器模式
{
    //迭代器模式的主要用途是将集合(如列表、数组、字典等)的遍历操作封装起来，从而隐藏了集合底层实现的细节。
    //这使得代码更加简单、易读和可维护，同时也提高了代码的灵活性和扩展性。

    //简单来说我们通过一个叫迭代器的东西来帮我们实现遍历操作

    //抽象聚合类和具体聚合类干的破事就是把一个集合封装起来（不是你封装你妈呢）
    abstract class Collection<Y>
    {
        //首先定义一个集合
        protected List<Y> list = new List<Y>();
        //给它留一个增加元素的方法（不重要）
        public abstract void Add(Y item);

        //由于我们封装了这个集合，我们需要给外界提供它的长度和元素
        //返回元素
        public object GetCurrent(int i)
        {
            return list[i];
        }
        //返回长度
        public int GetLength()
        {
            return list.Count;
        }

        //聚合类需要返回一个它的迭代器
        public abstract Iterator<Y> GetIterator();
    }
    //具体聚合类
    class ConcreteCollection<Y> : Collection<Y>
    {
        //允许它增加元素
        public override void Add(Y a)
        {
            list.Add(a);
        }
        //返回一个当前的迭代器
        public override Iterator<Y> GetIterator()
        {
            return new ConcreteIterator<Y>(this);
        }
    }

    //抽象迭代器迭代器
    abstract class Iterator<Y>
    {
        //将collection拿过来
        protected Collection<Y> collection;
        //collection的索引
        protected int _index;

        //是否移动到下一个
        public abstract bool MoveNext();
        //获取聚合类中的一个元素
        public abstract object GetCurrent();
        //重置索引
        public abstract void Reset();
        //移动到下一个
        public abstract void Next();
    }
    //具体迭代器
    class ConcreteIterator<Y> : Iterator<Y>
    {
        //为collection赋初值
        public ConcreteIterator(Collection<Y> collection)
        {
            base._index = 0;
            base.collection = collection;
        }

        //返回当前值(调用集合类中的函数)
        public override object GetCurrent()
        {
            return collection.GetCurrent(_index);
        }

        //判断是否移动到下一个
        public override bool MoveNext()
        {
            if (_index < collection.GetLength())
            {
                return true;
            }
            return false;
        }

        //移动到下一个
        public override void Next()
        {
            if (_index < collection.GetLength())
            {
                _index++;
            }
        }

        //重置为0
        public override void Reset()
        {
            _index = 0;
        }
    }




    internal class Program
    {
        static void Main(string[] args)
        {
            //怎么说这个东西呢
            //我更感觉，这像是在让我们了解最基础的迭代器是怎么实现的

            Collection<int> collection = new ConcreteCollection<int>(); 
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);  //初始化一下

            //迭代器
            ConcreteIterator<int> concreteIterator = new ConcreteIterator<int>(collection);
            
            //接下来就是我们大费周章想让迭代器实现的功能了
            //将一切集合的所有功能封装在迭代器中（理论上，上面的Add函数也应该再由concreteIterator封装一层）（但是我真的是懒得做了）
            while(concreteIterator.MoveNext())
            {
                Console.WriteLine(concreteIterator.GetCurrent());
                concreteIterator.Next();
            }
        }
    }
}
