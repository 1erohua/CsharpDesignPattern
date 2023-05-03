using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace TestForDesign
{
    internal class Program
    {
        static void Main(string[] args)
        {
            One one = new One();
            one.Test();
            Zero zero = new One();
            zero.Test();    
        }

    }
    public abstract class Zero
    {
        public abstract void Test();
    }

    public class One : Zero
    {
        [Obsolete("Test is deprecated, use NewTest instead.",true)]
        public override void Test()
        {
            Console.WriteLine("hello");
            // Your implementation here
        }

        public void NewTest()
        {
            // Your new implementation here
        }
    }
}
