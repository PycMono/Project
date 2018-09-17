//***********************************************************************************
// 文件名称：BattleReportBase.cs
// 功能描述：战报父类信息
// 数据表：无
// 作者：pyc
// 日期：2017/12/8 11:16:37
// 修改记录：
//***********************************************************************************
using System;
using System.Linq;

namespace FightHander.FightReport
{
    using PycMono.Project.Model;
    using Moqikaka.GameServer.SGH5.FightModel;

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

        protected override bool IsSave()
        {
            return false;
        }
    }
}
