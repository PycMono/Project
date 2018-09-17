﻿//***********************************************************************************
// 文件名称：CopyFightReportImpl.cs
// 功能描述：副本战报逻辑处理类
// 数据表：无
// 作者：pyc
// 日期：2017/12/8 11:16:37
// 修改记录：
//***********************************************************************************

using System;

namespace FightHander.FightReport
{
    using PycMono.Project.Model;
    using Moqikaka.GameServer.SGH5.FightModel;

    /// <summary>
    /// 副本战报逻辑处理类
    /// </summary>
    class CopyFightReportImpl : FightReportBase
    {
        #region 字段

        /// <summary>
        /// 节点ID
        /// </summary>
        private Int32 NodeID { get; set; }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeID">节点ID</param>
        /// <param name="teamCalcResponse">战报集合</param>
        public CopyFightReportImpl(Int32 nodeID, TeamCalcResponse teamCalcResponse)
            : base(teamCalcResponse)
        {
            NodeID = nodeID;
        }

        /// <summary>
        /// 组装防御方基本数据
        /// </summary>
        /// <param name="queueBattleInfo">战报信息</param>
        /// <returns>防御方数据</returns>
        protected override PlayerBaseInfo GetDefBaseInfo(QueueBattleInfo queueBattleInfo)
        {
            return new PlayerBaseInfo();
        }

        /// <summary>
        /// 是否保存血量
        /// </summary>
        /// <returns>true：保存，false：不保存</returns>
        protected override bool IsSave()
        {
            return false;
        }
    }
}
