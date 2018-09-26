//***********************************************************************************
//文件名称：GEventBase.cs
//功能描述：全局事件父类
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
    /// 全局事件父类
    /// </summary>
    internal abstract class GEventBase
    {
        /// <summary>
        /// 战斗事件字段 
        /// </summary>
        protected GlobalEvent globalEvent = null;

        /// <summary>
        /// 事件是否在运行
        /// </summary>
        internal Boolean IsRun { get; set; }

        /// <summary>
        /// 是否完成（框架层处理，如果这字段设置成true，将自动删除这个事件）
        /// </summary>
        internal Boolean IsFinish { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="globalEvent">事件对象</param>
        protected GEventBase(GlobalEvent globalEvent)
        {
            this.globalEvent = globalEvent;
        }

        #region 需要实现的方法

        /// <summary>
        /// 运行代码逻辑
        /// </summary>
        internal abstract void Run();

        #endregion

        #region 对外提供方法

        /// <summary>
        /// 获取事件
        /// </summary>
        /// <returns></returns>
        internal GlobalEvent GetEvent()
        {
            return this.globalEvent;
        }

        /// <summary>
        /// 获取事件ID
        /// </summary>
        /// <returns>事件ID</returns>
        internal String GetEventID()
        {
            return this.globalEvent.ID;
        }

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <returns>结束时间</returns>
        internal DateTime GetEndTime()
        {
            return this.globalEvent.EndTime;
        }

        /// <summary>
        /// 获取开始时间
        /// </summary>
        /// <returns>开始时间</returns>
        internal DateTime GetStartTime()
        {
            return this.globalEvent.StartTime;
        }

        #endregion
    }
}
