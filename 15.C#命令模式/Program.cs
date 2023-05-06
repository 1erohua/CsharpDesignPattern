using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15.C_命令模式
{
    //我感觉这个模式，怎么说呢，我有点没抓住核心
    //命令模式的核心思想是将请求封装为一个对象，将其作为命令发起者和接收者的中介
    //而抽象出来的命令对象又使得能够对一系列请求进行操作，如对请求进行排队，记录请求日志以及支持可撤销的操作等

    //命令的接收者
    class Student_Receiver
    {
        public void RunAction()
        {
            Console.WriteLine("接收到命令了！执行！");
            Console.WriteLine("开始跑一千米");
        }
        public void JumpAction()
        {
            Console.WriteLine("接到命令了！开始执行！");
            Console.WriteLine("开始跳远");
        }
    }

    //抽象命令
    abstract class Command
    {
        //命令内必须要有 接收者的实例，这样才能 唤醒接收者
        protected Student_Receiver _student;
        public Command(Student_Receiver student)
        {
            this._student = student;
        }
        //执行命令
        public abstract void Action();
        //这里应该还有一个撤销命令的，但我懒得写了（嘻嘻嘻）
    }
    
    //每个具体的命令都要用具体的派生类写出来
    //具体的命令:跑步
    class RunCommand : Command
    {
        public RunCommand(Student_Receiver student) : base(student)
        {
        }
        //这里执行
        public override void Action()
        {
            _student.RunAction();
        }
    }
    //具体的命令:跳远
    class JumpCommand : Command
    {
        public JumpCommand(Student_Receiver student) : base(student)
        {
        }

        public override void Action()
        {
            _student.JumpAction();
        }
    }


    //教练！
    class Coach_Invoker
    {
        //教练有一系列命令清单
        //其实这里应该用字典更好点，因为这样客户端就能指定调用哪个命令从而减少代码量
        //而且这里更应该和享元模式的那个思维结合
        //就是主程序调用命令的时候，应该先从命令列表看看命令存不存在
        List<Command> _commands = new List<Command>();

        //增加命令
        public void AddCommand(Command command)
        {
            _commands.Add(command);
        }

        //执行命令
        public void UseCommand()
        {
            foreach (Command command in _commands) { command.Action(); }//这里其实也是为了方便省事起见的
        }
        //根据我的理解来，就是领导（客户端）添加命令给 教练（Invoker）的命令清单里面
        //然后这些命令，必须都是学生（Receiver）能执行的，即Receiver类里面有的（当然也可以以抽象类的形式不断拓展（可惜C#不允许多继承）
        //当领导需要教练使用的时候，就会UseCommand传入参数，所以这里用Dictionary会比较好，键值对Dictionary<string,Command>
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Student_Receiver student = new Student_Receiver();
            Command aCommand = new JumpCommand(student);
            Coach_Invoker coach_Invoker = new Coach_Invoker();

            coach_Invoker.AddCommand(aCommand);
            coach_Invoker.UseCommand();
        }
    }
}
