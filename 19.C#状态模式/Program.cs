using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19.C_状态模式
{
    //如果某个对象有多个状态时，那么就会对应很多的行为。
    //那么对这些状态的判断和根据状态完成的行为，就会导致多重条件语句，
    //并且如果添加一种新的状态时，需要更改之前现有的代码

    //状态模式就是为了解决这个而生
    //状态模式将每种状态对应的行为抽象出来成为单独新的对象，这样状态的变化不再依赖于对象内部的行为。

    //我把代码看了一遍，只能说是非常神奇
    //它厉害的地方在于，定义了一个抽象状态
    //然后定义了多个具体状态
    //每个具体状态之间重载一个构造方法，该构造方法传入一个 抽象状态对象啊作为参数
    //这个构造方法用来刷新一些属性和变化一些方法，同时保留部分抽象状态的属性
    //而改变状态的方法 通过 改变属性的方法激发

    //为了省事起见，我们以银行账户为例子
    //当存款在0到1000的时候，为正常账户，即NormalState
    //存款在1000以上的时候，为富有账户，即RichState
    //存款在0以下时，为欠费账户，即UnEnoughState(英语不好，谢罪)
    //抽象状态类
    abstract class State
    {
        //本质信息
        public double Money { get; set; }//存款
        public Account account { get; set; }//存款人账户
        //随着存款变化而改变的信息
        public abstract void GetMoney(double money);//取钱
        public abstract void SetMoney(double money);//存钱
        //改变用户状态(改变state是该改变account中的State对象)
        public abstract void ChangeState();
    }
    //状态1，正常账户
    class NormalState : State
    {
        //该函数用于切换状态并保留部分属性
        public NormalState(State state)
        {
            this.account = state.account;
            this.Money = state.Money;
        }
        //构造函数
        public NormalState(Account account)
        {
            this.account = account;
            this.Money = 0;
        }
        //该函数用于判断是否改变状态
        public override void ChangeState()
        {
            if (Money > 1000)//升级为富裕账户
            {
                account.State = new RichState(this);
            }
            else if (account.State.Money < 0)//降级为欠费账户
            {
                account.State = new NotEnoughState(this);
            }
        }
        //存钱
        public override void GetMoney(double money)
        {
            Money -= money;
            ChangeState();
        }
        //取钱
        public override void SetMoney(double money)
        {
            Money += money;
            ChangeState();
        }
    }
    //状态2，富裕账户
    class RichState : State
    {
        //该函数用于切换状态并保留部分属性
        public RichState(State state)
        {
            this.account = state.account;
            this.Money = state.Money;
        }
        //构造函数
        private RichState(Account account) { }//Rich和UnEnough不应该有构造函数
        //该函数用于判断是否改变状态
        public override void ChangeState()
        {
            if ((Money < 1000) && (Money > 0))//降级为普通账户
            {
                account.State = new NormalState(this);
            }
            else if (Money < 0) //降级为欠费账户
            {
                account.State = new NotEnoughState(this);
            }
        }
        //存钱
        public override void GetMoney(double money)
        {
            Money -= money;
            ChangeState();
        }
        //取钱
        public override void SetMoney(double money)
        {
            Money += money;
            ChangeState();
        }
    }
    //状态3，欠费账户
    class NotEnoughState : State
    {
        //该函数用于切换状态并保留部分属性
        public NotEnoughState(State state)
        {
            this.account = state.account;
            this.Money = state.Money;
        }
        //构造函数
        private NotEnoughState(Account account) { }//Rich和UnEnough不应该有构造函数
        //该函数用于判断是否改变状态
        public override void ChangeState()
        {
            if ((Money < 1000) && (Money > 0))//升级为普通账户
            {
                account.State = new NormalState(this);
            }
            else if (Money > 1000)//升级为超级用户
            {
                account.State = new RichState(this);
            }
        }
        //存钱
        public override void GetMoney(double money)
        {
            Console.WriteLine("欠款中，不能取钱！");
        }
        //取钱
        public override void SetMoney(double money)
        {
            Money += money;
            ChangeState();
        }
    }
    class Account//存款人
    {
        public string Name { get; set; }//存款人信息
        public State State { get; set; }//每个存款人都有一个账户
        public Account(string Name)
        {
            this.Name = Name;
            //刚开户
            State = new NormalState(this);
        }
    }
    //刚刚打代码的时候有几点要说一下才行
    //就是我犯了一个错误，什么错误呢
    //我把state传过来，然后使用 类似state = new RichState(state);
    //然而这并不会影响主函数中account的state属性的，只是改变了这里的一个形式参数
    internal class Program
    {
        static void NoneMain(string[] args)//有两个Main，据此修改罢了
        {
            //ctm
            //千万别买14寸电脑
            //买性能好点的我操
            Account account = new Account("LiHua");
            account.State.GetMoney(1001);
            account.State.GetMoney(100);
            Console.WriteLine(account.State.Money.ToString());
            //状态类需要有账户的引用，而账户也需要有状态的引用
        }
    }
}
namespace update
{
    //写于2023/5/13
    //感谢学长提醒到这仍未解决到开闭原则的问题
    //在经过一番思考后决定修改一下状态模式
    //将判断状态修改和执行状态修改放入同一个类中作为静态函数
    //为方便起见，与上文重复部分不再编写注释
    abstract class State
    {
        //本质信息
        public double Money { get; set; }//存款
        public Account account { get; set; }//存款人账户
        //随着存款变化而改变的信息
        public abstract void GetMoney(double money);//取钱
        public abstract void SetMoney(double money);//存钱
    }
    //状态1，正常账户
    class NormalState : State
    {
        //该函数用于切换状态并保留部分属性
        public NormalState(State state)
        {
            this.account = state.account;
            this.Money = state.Money;
        }
        //构造函数
        public NormalState(Account account)
        {
            this.account = account;
            this.Money = 0;
        }
        //存钱
        public override void GetMoney(double money)
        {
            Money -= money;
            StateChange.ChangeState(ref account.State);
        }
        //取钱
        public override void SetMoney(double money)
        {
            Money += money;
            StateChange.ChangeState(ref account.State);
        }
    }
    //状态2，富裕账户
    class RichState : State
    {
        //该函数用于切换状态并保留部分属性
        public RichState(State state)
        {
            this.account = state.account;
            this.Money = state.Money;
        }
        //构造函数
        private RichState(Account account) { }//Rich和UnEnough不应该有构造函数
        //存钱
        public override void GetMoney(double money)
        {
            Money -= money;
            StateChange.ChangeState(ref account.State);
        }
        //取钱
        public override void SetMoney(double money)
        {
            Money += money;
            StateChange.ChangeState(ref account.State);
        }
    }
    //状态3，欠费账户
    class NotEnoughState : State
    {
        //该函数用于切换状态并保留部分属性
        public NotEnoughState(State state)
        {
            this.account = state.account;
            this.Money = state.Money;
        }
        //构造函数
        private NotEnoughState(Account account) { }//Rich和UnEnough不应该有构造函数
        //存钱
        public override void GetMoney(double money)
        {
            Console.WriteLine("欠款中，不能取钱！");
        }
        //取钱
        public override void SetMoney(double money)
        {
            Money += money;
            StateChange.ChangeState(ref account.State);
        }
    }
    class Account//存款人
    {
        public string Name { get; set; }//存款人信息
        public State State;//每个存款人都有一个账户
        public Account(string Name)
        {
            this.Name = Name;
            //刚开户
            State = new NormalState(this);
        }
    }
    class StateChange
    {
        public static void ChangeState(ref State state)
        {
            if (state == null)
            {
                Console.WriteLine("No");
                return;
            }
            if (state.Money < 0)
            {
                state = new NotEnoughState(state);
            }
            else if (state.Money > 0 && state.Money < 1000)
            {
                state = new NormalState(state);
            }
            else if (state.Money > 1000)
            {
                state = new RichState(state);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("LiHua");
            account.State.GetMoney(1001);
            account.State.GetMoney(100);
            account.State.SetMoney(100000);
            
            Console.WriteLine(account.State.Money.ToString());

            //只能说得益于账户和状态之间是互相嵌套的，ref this是不成立但ref account.State是可以的
            //毕竟我们只从account获取信息
        }
    }
}
