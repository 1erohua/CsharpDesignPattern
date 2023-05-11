using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_单例模式
{
    /// <summary>
    /// 单例类只有一个实例对象；
    ///单例对象必须由单例类自行创建；
    ///单例类对外提供一个访问该单例的全局访问点。
    /// </summary>
    internal class SingleOne
    {
        // 定义一个静态变量来保存类的实例
        private static SingleOne instance;

        private int x;
        private int y;
        private int z;

        //定义一个私有构造函数，使外界不能对它操作
        private SingleOne()
        {
            x = 1; y = 2; z = 3;
        }

        //这是一个简单的单例模式
        //如果这是单线程，那么就到此为止了
        public static SingleOne GetSingleOne1()
        {
            if (instance == null)
            {
                instance = new SingleOne();
            }
            return instance;
        }

        //对于多线程的限制访问
        private static object locker = new object();

        public static SingleOne GetSingleOne2()
        {
            //通过加锁防止重复创建实例
            lock (locker)
            {
                if (instance == null)
                {
                    instance = new SingleOne();
                }
            }
            return instance;
        }

        //优化
        public static SingleOne GetSingleOne3()
        {
            //我们加锁是为了防止重复创建实例
            //那么其实只需要判断是否为空，然后再锁
            //如果不为空，也就不需要在锁外进行判断
            if (instance == null)
            {
                //加锁是为了防止某些线程同时判断为空进入
                lock (locker)
                {
                    //再判断一下，如果前有前程创建了就不管了
                    if (instance == null)
                    {
                        instance = new SingleOne();
                    }
                }
            }
            return instance;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            SingleOne singleOne = SingleOne.GetSingleOne3();
        }
    }
}