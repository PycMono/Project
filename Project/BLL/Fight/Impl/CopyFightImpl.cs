//***********************************************************************************
//文件名称：MultiQeueFightBase.cs
//功能描述：副本战斗逻辑类（触发战斗的地方，new对象）
//数据表：Nothing
//作者：pyc
//日期：2017/12/07 17:32:33
//修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;

namespace FightHander.Fight
{
    using PycMono.Project.Model;
    using Moqikaka.GameServer.SGH5.FightModel;

    /// <summary>
    /// 副本战斗逻辑类（触发战斗的地方，new对象）
    /// </summary>
    internal class CopyFightImpl : MultiQeueFightBase
    {
        /// <summary>
        /// 玩家对象
        /// </summary>
        private Player mPlayer { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        private Int32 mNodeID { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="NodeID">副本节点ID</param>
        public CopyFightImpl(Player player, Int32 NodeID)
        {
            this.mPlayer = player;
            this.mNodeID = NodeID;
        }

        /// <summary>
        /// 组装攻击方数据
        /// </summary>
        /// <returns>战斗对象</returns>
        protected override List<ClientQueue> GetAtkQueue()
        {
            return new List<ClientQueue>();
        }

        /// <summary>
        /// 组装防御方数据
        /// </summary>
        /// <returns>战斗对象</returns>
        protected override List<ClientQueue> GetDefQueue()
        {
            return new List<ClientQueue>();
        }
    }
}
