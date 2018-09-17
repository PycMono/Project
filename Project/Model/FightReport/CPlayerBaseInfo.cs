//***********************************************************************************
// 文件名称：PlayerBaseInfo.cs
// 功能描述：客服端玩家基本数据
// 数据表：无
// 作者：pyc
// 日期：2017/12/8 11:16:37
// 修改记录：
//***********************************************************************************
using System;
using System.Collections.Generic;

namespace PycMono.Project.Model
{
    /// <summary>
    /// 客服端玩家基本数据（战报）
    /// </summary>
    public class PlayerBaseInfo
    {
        /// <summary>
        /// 玩家ID
        /// </summary>
        public Guid PlayerID { get; set; }

        /// <summary>
        /// 队列ID
        /// </summary>
        public Int32 QueueID { get; set; }

        /// <summary>
        /// 玩家名字
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public Int32 Country { get; set; }

        /// <summary>
        /// 玩家头像
        /// </summary>
        public Int32 HeadImageID { get; set; }

        /// <summary>
        /// 战力
        /// </summary>
        public Int64 FAP { get; set; }

        /// <summary>
        /// 玩家等级
        /// </summary>
        public Int32 Lv { get; set; }

        /// <summary>
        /// 击杀数量
        /// </summary>
        public Int64 KillCount { get; set; }

        /// <summary>
        /// 带兵数量
        /// </summary>
        public Int64 ArmyCount { get; set; }

        /// <summary>
        /// 玩家经验
        /// </summary>
        public Int64 PlayerExp { get; set; }

        public PlayerBaseInfo()
        {
            this.Name = String.Empty;
        }
    }
}
