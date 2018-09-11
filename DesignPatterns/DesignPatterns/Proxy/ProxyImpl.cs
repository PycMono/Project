//***********************************************************************************
// 文件名称：ProxyImpl.cs
// 功能描述：代理类
// 数据表：无
// 作者：killer
// 日期：2018/06/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;

namespace DesignPatterns.Proxy
{
    /// <summary>
    /// 代理类
    /// </summary>
    public class ProxyImpl : ISendGift
    {
        /// <summary>
        /// 追求者实现类
        /// </summary>
        private PursuitImpl PursuitImpl;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">名字</param>
        public ProxyImpl(String name)
        {
            PursuitImpl = new PursuitImpl(name);
        }

        public void SendDolls()
        {
            PursuitImpl.SendDolls();
        }

        public void SendFlowers()
        {
            PursuitImpl.SendFlowers();
        }
    }
}
