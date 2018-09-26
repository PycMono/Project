//***********************************************************************************
// 文件名称：GlobalEvent.cs
// 功能描述：游戏事件表
// 数据表：g_event
// 作者：byron
// 日期：2017/12/12 14:33:53
// 修改记录：
//***********************************************************************************

using System;

namespace Moqikaka.GameServer.SGH5.Model
{
    using Moqikaka.Util.Lock;

    /// <summary>
    /// 游戏事件表
    /// </summary>
    public sealed class GlobalEvent
    {
        #region 变量

        /// <summary>
        /// 锁对象
        /// </summary>
        private ReaderWriterLockUtil mLockObj = new ReaderWriterLockUtil();

        #endregion

        #region 属性

        /// <summary>
        /// 事件ID
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public Int32 EventType { get; set; }
        
        /// <summary>
        /// 是否完成
        /// </summary>
        public Boolean IsDone { get; set; }

        /// <summary>
        /// 玩家ID
        /// </summary>
        public Guid PlayerID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// EndTime
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 事件数据
        /// </summary>
        public String Content { get; set; }

        #endregion

        #region 辅助属性

        /// <summary>
        /// 是否保存数据(true:不保存数据库 fasle:需要保存到数据库)
        /// </summary>
        public Boolean NoSaveDB { get; set; }

        /// <summary>
        /// 事件执行错误次数
        /// </summary>
        public Int32 ErrorCount { get; set; }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取事件锁
        /// </summary>
        /// <param name="lockType">锁定类型</param>
        /// <param name="timeOut">锁超时时间</param>
        /// <returns>事件锁对象</returns>
        public IDisposable GetLock(ReaderWriterLockUtil.LockTypeEnum lockType, Int32 timeOut = 500)
        {
            return mLockObj.GetLock(this.GetHashCode().ToString(), lockType, timeOut);
        }

        #endregion
    }
}