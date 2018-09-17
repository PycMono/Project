//***********************************************************************************
// 文件名称：BattleFormation.cs
// 功能描述：战斗布阵对象
// 数据表：
// 作者：pyc
// 日期：2017/12/4 17:22:04
//修改记录：
//***********************************************************************************
using System;
using System.Collections.Generic;

namespace PycMono.Project.Common
{
    using Moqikaka.Util.Lock;
    using Moqikaka.Util.Json;

    /// <summary>
    /// 玩家阵容配置信息
    /// </summary>
    public sealed class BattleQueueInfo
    {
        #region 属性

        /// <summary>
        /// 玩家Id
        /// </summary>
        public Guid PlayerID { get; private set; }

        /// <summary>
        /// #队列ID
        /// </summary>
        public Int32 QueueID { get; set; }

        /// <summary>
        /// 槽位对象1
        /// </summary>
        public SlotObj SlotObj1 { get; set; }

        /// <summary>
        /// 槽位对象2
        /// </summary>
        public SlotObj SlotObj2 { get; set; }

        /// <summary>
        /// 槽位对象3
        /// </summary>
        public SlotObj SlotObj3 { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Crdate { get; set; }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取槽位对象集合（返回list）
        /// </summary>
        /// <returns>位置对象集合</returns>
        public List<SlotObj> GetItem_ToList()
        {
            var list = new List<SlotObj>();
            Action<List<SlotObj>, SlotObj> addList = (param1, param2) =>
            {
                if (param2 == null)
                {
                    return;
                }

                param1.Add(param2);
            };

            addList(list, SlotObj1);
            addList(list, SlotObj2);
            addList(list, SlotObj3);

            return list;
        }

        /// <summary>
        /// 获取槽位对象集合（返回字典）
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int32, SlotObj> GetItem_ToDict()
        {
            var dict = new Dictionary<Int32, SlotObj>();
            Action<Dictionary<Int32, SlotObj>, SlotObj> addList = (param1, param2) =>
            {
                if (param2 == null)
                {
                    return;
                }

                param1[param2.SlotID] = param2;
            };

            addList(dict, SlotObj1);
            addList(dict, SlotObj2);
            addList(dict, SlotObj3);

            return dict;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BattleQueueInfo()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="player">玩家相关数据对象</param>
        public BattleQueueInfo(Player player)
        {
        }

        /// <summary>
        /// 序列化对象为字符串
        /// </summary>
        /// <param name="obj">槽位对象</param>
        /// <returns>槽位字符串</returns>
        public String BuildToStr(Object obj)
        {
            return obj == null ? String.Empty : JsonUtil.Serialize(obj);
        }

        #endregion
    }
}