//***********************************************************************************
//文件名称：GTreasureEventImpl.cs
//功能描述：宝藏寻访事件实现类
//数据表：Nothing
//作者：彭亚川
//日期：2018/11/21 17:17:04
//修改记录：
//***********************************************************************************

using System;

namespace PycMono.Project.Event
{
    using Moqikaka.GameServer.SGH5.Model;

    /// <summary>
    /// 宝藏寻访事件逻辑实现类
    /// </summary>
    internal class GTreasureEventImpl : GEventBase
    {
        // 类名称，用于错误标识
        private const String mClassName = "GTreasureEventImpl";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="globalEvent">事件对象</param>
        internal GTreasureEventImpl(GlobalEvent globalEvent)
        : base(globalEvent)
        {
            this.globalEvent = globalEvent;
        }

        /// <summary>
        /// 实现父类run方法
        /// </summary>
        internal override void Run()
        {
            Console.Write($"GTreasureEventImpl.Run:宝藏寻访事件成功到达");
        }
    }
}
