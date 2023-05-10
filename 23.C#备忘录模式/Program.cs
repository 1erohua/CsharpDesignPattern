using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _23.C_备忘录模式
{
    //这大概是这23个设计模式中实现最灵活的一个，有太多可以实现的空间了

    //备忘录模式就是对某个类的状态进行保存下来，等到需要恢复的时候，可以从备忘录中进行恢复
    //在不破坏封装的前提下，捕获一个对象的内部状态，并在该对象之外保存这个状态，这样以后就可以把该对象恢复到原先的状态。

    //我大概明白是怎么回事了
    //说实话想到备忘录模式的这个人有两把刷子
    //尤其是NewBing实现的通过栈实现的 撤销与重做的备忘录模式
    //我们先将new bing实现的文本编辑器

    //当前文本
    class CurrentText
    {
        //这是一个文本
        public string Content { get; set; }
        public CurrentText(string content)
        {
            this.Content = content;
        }
        //保存当前文本(返回一个备忘录)
        public TextMemo Save()
        {
            return new TextMemo(Content);//返回一个记录当前信息的文本
        }
        //从备忘录中恢复
        public void Restore(TextMemo textMemo)
        {
            this.Content = textMemo.Text;
        }
    }
    //备忘录//备忘录只会被备忘录操控中心使用，它不会在主目录中单独定义
    class TextMemo
    {
        //备忘录的文本
        public string Text { get; set; }
        //备忘录构造函数，用于构造备忘录函数
        public TextMemo(string text)
        {
            this.Text = text;
        }
    }
    //备忘录操作类//简单起见，只做撤销工作//与撤销工作相应的重做，即向前
    class TextControl
    {
        //使用栈实现撤销操作
        Stack<TextMemo> Undo_memoStack = new Stack<TextMemo>();
        public void Undo(CurrentText currentText)
        {
            if (Undo_memoStack.Count > 0)//这是可以撤销的情况
            {
                //弹出撤销状态栏最前面的一个
                TextMemo memo = Undo_memoStack.Pop();
                //恢复至该状态
                currentText.Restore(memo);

            }
        }
        //更新文本
        public void Update(CurrentText currentText, string newContent)
        {
            //先将当前文档保存至栈中的撤销栈
            Undo_memoStack.Push(currentText.Save());
            //然后为文档更新
            currentText.Content = newContent;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            CurrentText text = new CurrentText("Hello");
            TextControl undo_Test = new TextControl();

            undo_Test.Update(text, "你好");
            Console.WriteLine(text.Content);

            undo_Test.Undo(text);
            Console.WriteLine(text.Content);
        }
    }
}
//你以为这就完了吗？
//并没有
//现在是2023年5月10日
//讯飞星火模型给了一个我很感兴趣的实现方法
//它的备忘录是Dictionary<string,Action>类型的存储
//我还领悟了，要根据不同的场景，使用不同的数据类型结构存储数据和操作
namespace _备忘录模式_其他有趣的实现
{
    //这个实现没有备忘录管理，备忘录本身管理自己
    abstract class Memo
    {
        //方便起见只用Action
        protected Dictionary<string, Action> memos;
        public Memo(Dictionary<string, Action> memos)
        {
            this.memos = memos;
        }
        //增加事件与操作
        public abstract void Add(string key, Action action);
        //移除操作
        public abstract void Remove(string key);
        //获得一个操作
        public abstract Action GetAction(string key);
    }
    //感觉就是把字典封装起来了
    class SuperMemo : Memo
    {
        public SuperMemo(Dictionary<string, Action> memos) : base(memos)
        {
        }
        //增加事件与操作
        public override void Add(string key, Action action)
        {
            this.memos.Add(key, action);
        }
        //获得一个操作
        public override Action GetAction(string key)
        {
            return this.memos[key];
        }
        //移除一个操作
        public override void Remove(string key)
        {
            this.memos.Remove(key);
        }
    }//其实作为一个真正的memo应该有内容(string)与事件(Action)的，可惜我没学元组，暂时先委屈一下了



    internal class Program
    {
        static void Main(string[] args) 
        { 
            Memo me = new SuperMemo(new Dictionary<string, Action>());
            //开始增加事件（将使用Lambda表达式）
            me.Add("洗衣卡", () =>
            {
                Console.WriteLine("今天下午三点半要充洗衣卡，具体步骤如下。。。");
            });
            me.Add("理发", () =>
            {
                Console.WriteLine("今天下午5点钟要去xxx理发");
            });
            me.Add("作业", () =>
            {
                Console.WriteLine("大物作业要记得写");
            });
            //来个复杂点的
            me.Add("下载", () =>
            {
                Thread thread = new Thread(() =>
                {
                    Console.WriteLine("开始下载");
                    Thread.Sleep(1000);
                    Console.WriteLine("下载完成");
                });
            });
        }
    }

}
