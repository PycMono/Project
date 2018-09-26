//***********************************************************************************
//文件名称：GlobalEventBLL.cs
//功能描述：全局事件处理类
//数据表：Nothing
//作者：彭亚川
//日期：2018/11/21 17:17:04
//修改记录：
//***********************************************************************************

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PycMono.Project.Event
{
    using Moqikaka.Util.Lock;
    using PycMono.Project.Model.Enum;
    using Moqikaka.GameServer.SGH5.Model;

    /// <summary>
    /// 全局事件处理类
    /// </summary>
    public class GlobalEventBLL
    {
        #region 基本字段

        private const String mClassName = "GlobalEventBLL";
        private static readonly ReaderWriterLockUtil lockObj = new ReaderWriterLockUtil();

        /// <summary>
        /// 事件集合
        /// </summary>
        private static Dictionary<String, GlobalEvent> mData = new Dictionary<String, GlobalEvent>();

        #endregion

        // 数据从数据库中加载就不写了，这个事件会保存在数据库中的，启动服务器的时候加载数据


        #region 外部调用方法

        public static void AddEvent()
        {

        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="gEvent">事件</param>
        public static void AddEvent(GlobalEvent gEvent)
        {
            using (lockObj.GetLock(mClassName, ReaderWriterLockUtil.LockTypeEnum.Writer))
            {
                if (mData.ContainsKey(gEvent.ID))
                {
                    throw new Exception($"GlobalEventBLL.AddEvent方法中，已经存在ID={gEvent.ID}的事件");
                }

                mData[gEvent.ID] = gEvent;
                InsertInfo(gEvent);

                // 加入处理队列
                GEventHandle.AddEvent(gEvent);
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="eventID">事件ID</param>
        public static void DelEvent(String eventID)
        {
            using (lockObj.GetLock(mClassName, ReaderWriterLockUtil.LockTypeEnum.Writer))
            {
                if (!mData.ContainsKey(eventID))
                {
                    return;
                }

                var gEvent = mData[eventID];
                mData.Remove(eventID);

                DeleteInfo(gEvent);
                GEventHandle.RemoveImpl(gEvent.ID);
            }
        }

        /// <summary>
        /// 添加或更新事件
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="playerId">玩家id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">EndTime</param>
        /// <param name="content">事件内容</param>
        public static void AddOrUpdateEvent(String id, Int32 eventType, Guid playerId, DateTime startTime, DateTime endTime, String content)
        {
            using (lockObj.GetLock(mClassName, ReaderWriterLockUtil.LockTypeEnum.Writer, 500))
            {
                if (mData.ContainsKey(id))
                {
                    // 事件存在,更新事件(添加事件锁)
                    var item = mData[id];
                    using (item.GetLock(ReaderWriterLockUtil.LockTypeEnum.Writer, 10000))
                    {
                        item.StartTime = startTime;
                        item.EndTime = endTime;
                        item.IsDone = false;

                        UpdateInfo(item);
                    }

                    return;
                }

                // 事件不存在，添加事件
                var newEvent = new GlobalEvent
                {
                    ID = id,
                    EventType = eventType,
                    IsDone = false,
                    PlayerID = playerId,
                    StartTime = startTime,
                    EndTime = endTime,
                    Content = content
                };

                // 插入数据
                mData[id] = newEvent;
                InsertInfo(newEvent);

                // 加入处理队列
                GEventHandle.AddEvent(newEvent);
            }
        }

        /// <summary>
        /// 更新事件
        /// </summary>
        /// <param name="gEvent">事件对象</param>
        public static void UpdateEvent(GlobalEvent gEvent)
        {
            UpdateInfo(gEvent);
        }

        #endregion

        #region 数据库操作

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data">GlobalEvent对象</param>
        private static void InsertInfo(GlobalEvent data)
        {
            if (!data.NoSaveDB)
            {
                return;
            }

            // 插入数据库
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="data">GlobalEvent对象</param>
        private static void UpdateInfo(GlobalEvent data)
        {
            if (!data.NoSaveDB)
            {
                return;
            }

            // 更新数据
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data">GlobalEvent对象</param>
        private static void DeleteInfo(GlobalEvent data)
        {
            if (!data.NoSaveDB)
            {
                return;
            }

            // 删除数据
        }

        #endregion
    }
}
