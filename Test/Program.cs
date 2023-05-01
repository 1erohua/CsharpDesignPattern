using System;

namespace Test
{
    internal class One
    {
        public Three ThreeOne()
        {
            return new Three();
        }
    }

    internal class Two
    {
        private Three three = new Three();

        public Three ThreeTwo()
        {
            return three;
        }
    }

    internal class Three
    {
        public int x;
        public int y;

        public Three()
        {
            x = 11;
            y = 12;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            One Test1 = new One();
            Two Test2 = new Two();

            //返回new的
            Three Test11 = Test1.ThreeOne();
            Three Test12 = Test1.ThreeOne();

            //返回非new的
            Three Test21 = Test2.ThreeTwo();
            Three Test22 = Test2.ThreeTwo();

            Test11.y = 0;
            Test11.x = 0;
            Test12.y = 9;
            Test12.x = 9;

            Test21.x = 0;
            Test21.y = 0;
            Test22.x = 9;
            Test22.y = 9;

            Console.WriteLine("Test11：" + Test11.x + Test11.y);
            Console.WriteLine("Test12：" + Test12.x + Test12.y);
            Console.WriteLine("Test21：" + Test21.x + Test21.y);
            Console.WriteLine("Test22：" + Test22.x + Test22.y);
        }
    }
}