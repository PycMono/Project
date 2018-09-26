//***********************************************************************************
//文件名称：GEventHandle.cs
//功能描述：事件逻辑处理类
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
    /// 事件逻辑处理类--这个类对于外界一般不直接操作的
    /// </summary>
    internal class GEventHandle
    {
        // 类名称，用于错误标识
        private const String mClassName = "EvImplHandler";
        private static ReaderWriterLockUtil mLock = new ReaderWriterLockUtil();// 锁对象
        private static Dictionary<String, GEventBase> GEventDict = new Dictionary<String, GEventBase>();// 事件集合
        private static Dictionary<GEventTypeEnum, Func<GlobalEvent, GEventBase>> CreateImplDict = new Dictionary<GEventTypeEnum, Func<GlobalEvent, GEventBase>>();

        #region 数据初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        static GEventHandle()
        {
            CreateImplDict[GEventTypeEnum.Treasure] = g => new GTreasureEventImpl(g);
        }

        #endregion

        /// <summary>
        /// 运行事件处理器
        /// </summary>
        internal static void RunHandle()
        {
            var t = new Thread(new ThreadStart(HandleImpl)) { IsBackground = true };
            t.Start();
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="globalEvent">全局事件</param>
        public static void AddEvent(GlobalEvent globalEvent)
        {
            var funImpl = CreateImplDict[(GEventTypeEnum)globalEvent.EventType];
            AddImpl(funImpl(globalEvent));
        }

        /// <summary>
        /// 移除事件ID
        /// </summary>
        /// <param name="eventID">移除事件</param>
        internal static void RemoveImpl(String eventID)
        {
            using (mLock.GetLock(mClassName, ReaderWriterLockUtil.LockTypeEnum.Writer, 500))
            {
                if (!GEventDict.ContainsKey(eventID))
                {
                    return;
                }

                GEventDict.Remove(eventID);
            }
        }

        /// <summary>
        /// 增加处理事件
        /// </summary>
        /// <param name="impl">事件实现类</param>
        private static void AddImpl(GEventBase impl)
        {
            using (mLock.GetLock(mClassName, ReaderWriterLockUtil.LockTypeEnum.Writer, 500))
            {
                GEventDict[impl.GetEventID()] = impl;
            }
        }

        #region 事件运行

        /// <summary>
        /// 运行事件
        /// </summary>
        private static void HandleImpl()
        {
            while (true)
            {
                Thread.Sleep(1000);

                RunImpl();
            }
        }

        /// <summary>
        /// 运行实现类
        /// </summary>
        private static void RunImpl()
        {
            var eImplList = new List<GEventBase>();
            using (mLock.GetLock(mClassName, ReaderWriterLockUtil.LockTypeEnum.Writer, 500))
            {
                foreach (var gEventBase in GEventDict.Values)
                {
                    if (gEventBase.IsRun
                        && gEventBase.GetEndTime() > DateTime.Now)
                    {
                        continue;
                    }

                    gEventBase.IsRun = true;
                    eImplList.Add(gEventBase);
                }
            }

            // 并行处理事件
            foreach (var item in eImplList)
            {
                Task.Factory.StartNew(SafeRun, item);
            }
        }

        /// <summary>
        /// 安全运行事件处理器
        /// </summary>
        /// <param name="obj">事件处理器对象</param>
        private static void SafeRun(Object obj)
        {
            var impl = obj as GEventBase;
            try
            {
                impl.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GEventHandle.SafeRun方法报错ex={ex.StackTrace + Environment.NewLine + ex.StackTrace}");
                impl.GetEvent().EndTime = DateTime.Now.AddSeconds(3000);// 时间看自己项目里面设置

                // TODO 更新数据库
                GlobalEventBLL.UpdateEvent(impl.GetEvent());
            }
            finally
            {
                if (impl.IsFinish)
                {
                    RemoveImpl(impl.GetEventID());

                    // TODO 删除事件
                    GlobalEventBLL.DelEvent(impl.GetEventID());
                }
                else
                {
                    impl.IsRun = false;
                }
            }
        }

        #endregion
    }
}
