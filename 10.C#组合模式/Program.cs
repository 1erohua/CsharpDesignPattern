using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10.C_组合模式
{
    //它允许将对象组合成树形结构以表示“整体/部分”层次结构。
    //使用此模式，客户端可以按相同的方式处理单个对象和对象集合，而不必关注它们是单个对象还是组合对象
    abstract class WenJian
    {
        protected string fileName;
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public WenJian(string name)
        {
            fileName = name;
        }
        //删除文件(文件自身专属)
        [Obsolete("此函数不可使用", true)]
        public abstract WenJian delete();
        //删除文件（文件夹专属）
        public abstract void delete(WenJian wenJian);
        //增加文件（文件夹专属）
        public abstract void Add(WenJian wenJian);
        //展示文件
        public abstract void Show();
    }


    class File : WenJian// 组合树结构中的最底层元素，它们没有子节点，具有特定的行为。
    {
        public File(string name) : base(name)
        {
        }

        [Obsolete("此函数不可使用",true)]
        public override void Add(WenJian wenJian)
        {
            throw new Exception("没有此功能");
        }

        public override WenJian delete()
        {
            Console.WriteLine("delete success");
            return null;
        }

        public override void Show()
        {
            Console.WriteLine("FileName is " + this.fileName);
        }

        //使用特性强制不允许使用
        [Obsolete("此函数不可使用", true)]
        public override void delete(WenJian wenJian)
        {
            throw new Exception("没有此功能，此功能为文件夹使用的功能");
        }
    }

    class FileFolder : WenJian//容器: 包含一组子节点并维护它们之间的组合结构。容器可以包含其他容器和叶子节点，统一对子节点操作
    {
        public FileFolder(string name):base(name)
        {

        }
        private List<WenJian> files=new List<WenJian>();//复杂对象由简单对象组成
        public override void Add(WenJian wenJian)
        {
            files.Add(wenJian);
        }

        public override void delete(WenJian wenJian)
        {
            if(wenJian!=null)
            {
                foreach (WenJian jian in files)
                {
                    if(jian==wenJian)
                    {
                        files.Remove(jian);
                    }
                }
            }
            else
            {
                Console.WriteLine("该文件不存在！");
            }
        }
        [Obsolete("此函数不可使用", true)]
        public override void Show()
        {
            foreach(WenJian jian in files)
            {
                Console.WriteLine(jian.FileName);
            }
        }
        [Obsolete("此函数不可使用",true)]
        public override WenJian delete()
        {
            throw new Exception("此函数不可以使用");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            FileFolder fileFolder = new FileFolder("Game");
            File file1 = new File("源自之心");
            File file2 = new File("王者");


            fileFolder.Add(file1);
            fileFolder.Add(file2);

      
            fileFolder.Show();
        }
    }
}
