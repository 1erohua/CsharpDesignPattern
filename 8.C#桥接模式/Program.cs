using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8.C_桥接模式
{
    //我看了一下教程的代码，大概就是，将功能的具体实现交给电视机，然后遥控器只是调用电视机里的具体实现

    //我们先做一个电视机
    abstract class TV
    {
        public abstract void On();
        public abstract void Off();
        public abstract void ChangeChannel();
        public abstract void PlayGame();//这个是新增加的功能
    }
    //来一个具体的电视机，增加了新功能的
    class HuaWeiTV : TV
    {
        public override void ChangeChannel()
        {
            Console.WriteLine("华为电视机换台成功");
        }

        public override void Off()
        {
            Console.WriteLine("HuaWei Is off");
        }

        public override void On()
        {
            Console.WriteLine("HuaWei Now is On");
        }
        public override void PlayGame()//新增功能
        {
            Console.WriteLine("打开游戏界面");
        }
    }

    //我们再做一个遥控器
    abstract class RemoteControl
    {
        private TV tV;
        public TV TV
        {
            get { return tV; }
            set
            {
                tV = value;
            }
        }
        //将实现交给具体的电视机吧
        public virtual void On()
        {
            tV.On();
        }
        public virtual void Off()
        {
            tV.Off();
        }
        public virtual void ChangeChannel()
        {
            tV.ChangeChannel();
        }
        public abstract void PlayGame();//新增加的功能
    }
    //来一个具体的遥控器
    class HuaWeiRemoteControl:RemoteControl
    {
        public override void PlayGame()//实现新增加的功能
        {
           TV.PlayGame();
        }

    }



    internal class Program
    {
        static void Main(string[] args)
        {
            TV huaWeiTV00001=new HuaWeiTV();
            RemoteControl remoteControl =new HuaWeiRemoteControl(); 

            //将华为电视给他！
            remoteControl.TV = huaWeiTV00001;   

            remoteControl.PlayGame();
        }
    }
}
