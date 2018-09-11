//***********************************************************************************
// 文件名称：PursuitImpl.cs
// 功能描述：追求者实现类
// 数据表：无
// 作者：killer
// 日期：2018/06/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;

namespace DesignPatterns.Proxy
{
    /// <summary>
    /// 追求者实现类
    /// </summary>
    public class PursuitImpl : ISendGift
    {
        private String Name;

        public PursuitImpl(String name)
        {
            this.Name = name;
        }

        public  void  SendDolls()
        {
            Console.WriteLine($"{this.Name}:送玩偶");
        }

        public void SendFlowers()
        {
            Console.WriteLine($"{this.Name}:送玩鲜花");
        }
    }
}
