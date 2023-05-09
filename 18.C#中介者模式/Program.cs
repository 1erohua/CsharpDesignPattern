using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18.C_中介者模式
{
    //使用中介者模式之后，任何一个类的变化，只会影响中介者和类本身
    //不像之前的设计，任何一个类的变化都会引起其关联所有类的变化。这样的设计大大减少了系统的耦合度。
    //下面以打牌作为示例

    //抽象使用者（牌友）
    abstract class Users
    {
        //基本信息
        public int MoneyNumber;
        public string Name;
        public Users(int MoneyNumber, string User_Name) { this.MoneyNumber = MoneyNumber; this.Name = User_Name; }
        //赢钱函数
        public abstract void WinMoney(int count, AbstractMedia media);
    }
    class UserA : Users
    {
        public UserA(int MoneyNumber, string User_Name) : base(MoneyNumber, User_Name)
        {
        }
        public override void WinMoney(int count, AbstractMedia media)
        {
            media.AWin(count);
        }
    }
    class UserB : Users
    {
        public UserB(int MoneyNumber, string User_Name) : base(MoneyNumber, User_Name)
        {
        }
        public override void WinMoney(int count, AbstractMedia media)
        {
            media.BWin(count);
        }
    }

    abstract class AbstractMedia
    {
        //两名用户
        protected Users userA;
        protected Users userB;
        public AbstractMedia(Users userA, Users userB) { this.userB = userB; this.userA = userB; }
        //AWin、BWin
        public abstract void AWin(int count);
        public abstract void BWin(int count);
    }
    class Media : AbstractMedia
    {
        public Media(Users userA, Users userB) : base(userA, userB)
        {
        }
        public override void AWin(int count)
        {
            base.userA.MoneyNumber += count;
            base.userB.MoneyNumber -= count;
        }
        public override void BWin(int count)
        {
            base.userA.MoneyNumber -= count;
            base.userB.MoneyNumber += count;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //实在不喜欢它的这个实现
            //每个用户就要单独设置一个类？但我现在暂时没空思考这个
            //我想的几个优化办法
            //1.在中介类内，使用字典或者列表存储每个用户的数据
            //2.用户需要和谁进行战斗（打牌）输入对象和输赢钱数，或者每局固定，有中介类定下来
        }
    }
}
