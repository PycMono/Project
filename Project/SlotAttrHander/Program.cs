//***********************************************************************************
//文件名称：SlotPartAttrHanlder.cs
//功能描述：自定义描述内容类
//数据表：Nothing
//作者：彭亚川
//日期：2016/11/9 17:17:04
//修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PycMono.Project.Slot;

namespace SlotAttrHander
{
    class Program
    {
        static void Main(string[] args)
        {
            var hero = new Hero();
            SlotPartAttrHanlder.CalcAllPartAttr(new List<Hero>() { hero });

            Console.ReadKey();
        }
    }
}
