using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7.C_适配器模式
{
    //适配器模式分为类的适配器模式和对象的适配器模式
    //适配器模式——使得新环境中不需要去重复实现已经存在了的实现而很好地把现有对象（指原来环境中的现有对象）加入到新环境来使用。
    internal class Program
    {
        static void Main(string[] args)
        {
            //类的适配器模式
            IThreeHole myHomeThreeHole = new AdapterI();
            myHomeThreeHole.ThreeHoleInsert();

            //对象的适配器模式
            ThreeHole threeHole=new AdapterII();
            threeHole.ThreeHoleInsert();    
        }
    }

    //类的适配器模式
    //具体场景是:
    //在生活中，我们买的电器插头是2个孔的，但是我们买的插座只有三个孔的
    //此时我们就希望电器的插头可以转换为三个孔的就好，这样我们就可以直接把它插在插座上
    //此时三个孔插头就是客户端期待的另一种接口，自然两个孔的插头就是现有的接口
    //适配器模式就是用来完成这种转换的

    //不兼容的类1（适配器中的目标角色）
    public interface IThreeHole//由于C#不支持多继承，所以我们选择使用接口
    {
        void ThreeHoleInsert(); //三孔插头的功能
    }
    //不兼容的类2
    class TwoHole//（适配器中的源角色）
    {
        public void TwoHoleInsert()
        {
            Console.WriteLine("两孔插口正在为设备执行充电");
        }
    }
    //使他们兼容的适配器
    class AdapterI : TwoHole, IThreeHole
    {
        public void ThreeHoleInsert()
        {
            //兼容的方式就是，适配器继承了不兼容的两个类，使得适配器可以用来构造这两个类创建的对象
            //我们通常使用 目标角色创建对象，然后用适配器构造该对象
            //并且在适配器内调用原角色的函数，从而实现，目标角色使用不兼容的类的函数
            TwoHoleInsert();
        }
    }

    //对象的适配器模式
    abstract class ThreeHole
    {
        public abstract void ThreeHoleInsert();
    }
    class AdapterII : ThreeHole
    {
        TwoHole twoHole = new TwoHole();
        public override void ThreeHoleInsert()
        {
            twoHole.TwoHoleInsert();
        }
    }



}
