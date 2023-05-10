using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _21.C_责任链模式
{
    //某个请求可能需要几个人的审批，即使技术经理审批完了，还需要上一级的审批。
    //责任链模式指的是——某个请求需要多个对象进行处理，从而避免请求的发送者和接收之间的耦合关系。
    //将这些对象连成一条链子，并沿着这条链子传递该请求，直到有对象处理它为止。

    //某个请求
    class Holiday
    {
        //请假请求
        public string Name;
        public int Day;
        public Holiday(string name, int day)
        {
            Name = name;
            Day = day;
        }
    }
    //抽象责任人//抽象责任人应该有上一级的引用，即联系上级请求
    abstract class Leader
    {
        //我的上级
        public Leader MyLeader {  get; set; }
        //我的名字
        public string MyName { get; set; }
        //处理请求
        public abstract void Operation(Holiday holiday);
        //初始化
        public Leader(string name)
        {
            MyName = name;
        }
    }
    //班长
    class BanZhang : Leader
    {
        public BanZhang(string name) : base(name)
        {
        }
        //假期判断
        public override void Operation(Holiday holiday)
        {
            if(holiday.Day<=1) { Console.WriteLine("这个{1}假期，{0}批了",MyName,holiday.Day); }
            else
            {
                //批不了假期，交给能批的人
                this.MyLeader.Operation(holiday);
            }
        }
    }
    //辅导员
    class FuDaoYuan : Leader
    {
        public FuDaoYuan(string name) : base(name)
        {
        }
        //假期判断
        public override void Operation(Holiday holiday)
        {
            if(holiday.Day>1&&holiday.Day<3)
            {
                Console.WriteLine("这个{1}假期，{0}批了", MyName, holiday.Day);
            }
            else
            {
                this.MyLeader.Operation(holiday);
            }
        }
    }
    //总务处
    class ZongWuChu : Leader
    {
        public ZongWuChu(string name) : base(name)
        {
        }

        public override void Operation(Holiday holiday)
        {
            if (holiday.Day >=3 && holiday.Day < 7)
            {
                Console.WriteLine("这个{1}假期，{0}批了", MyName, holiday.Day);
            }
            else
            {
                this.MyLeader.Operation(holiday);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Leader leaderOne = new BanZhang("班长");
            Leader leaderTwo = new FuDaoYuan("辅导员");
            Leader leaderThree = new ZongWuChu("总务处");

            Holiday holiday = new Holiday("ZeroHua", 6);
            leaderOne.MyLeader = leaderTwo;
            leaderTwo.MyLeader = leaderThree;

            leaderOne.Operation(holiday);

        }
    }
}
