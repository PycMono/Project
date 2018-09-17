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
using System.Collections.Generic;

namespace FightHander.FightReport
{
    using Moqikaka.Util.Json;
    using PycMono.Project.Model;
    using Moqikaka.GameServer.SGH5.FightModel;

    /// <summary>
    /// 战报父类
    /// </summary>
    internal abstract class FightReportBase
    {
        #region 字段

        /// <summary>
        /// 战报信息
        /// </summary>
        protected TeamCalcResponse FightResponseInfo { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fightImpl">战斗对象</param>
        protected FightReportBase(TeamCalcResponse fightImpl)
        {
            FightResponseInfo = fightImpl;
        }

        #endregion 

        #region 实现方法

        /// <summary>
        /// 是否保存战报，有些模块不需要保存战报的
        /// </summary>
        /// <returns>true：保存战报，false，不保存战报</returns>
        protected abstract Boolean IsSave();

        #endregion

        #region 重载方法

        /// <summary>
        /// 组装攻击方基本信息
        /// </summary>
        /// <param name="queueBattleInfo">攻击方战报信息</param>
        /// <returns>攻击方基本信息</returns>
        protected virtual PlayerBaseInfo GetAtkBaseInfo(QueueBattleInfo queueBattleInfo)
        {
            // TODO 获取玩家基本信息，这里我没有地方获取只能临时创建了对象
            var player = new Player();
            return new PlayerBaseInfo()
            {
                PlayerID = player.ID,
                Name = player.Name,
                Country = 0,
                HeadImageID = 0,
                FAP = 0,
                Lv = 0,
                QueueID = queueBattleInfo.AtkQueueID,

                // 组装攻击方带兵数量
                ArmyCount = queueBattleInfo.FightObj.AtkTeam.Sum(p => p.HP),
                KillCount = queueBattleInfo.AtkRemainHpList.Sum(p => p.Damage),
            };
        }

        /// <summary>
        /// 获取防御方基本信息
        /// </summary>
        /// <returns></returns>
        protected virtual PlayerBaseInfo GetDefBaseInfo(QueueBattleInfo queueBattleInfo)
        {
            // TODO 获取玩家基本信息，这里我没有地方获取只能临时创建了对象
            var player = new Player();

            return new PlayerBaseInfo()
            {
                PlayerID = player.ID,
                Name = player.Name,
                Country = 0,
                HeadImageID = 0,
                FAP = 0,
                Lv = 0,
                QueueID = queueBattleInfo.DefQueueID,

                // 组装防御方带兵数量
                ArmyCount = queueBattleInfo.FightObj.DefTeam.Sum(p => p.HP),
                KillCount = queueBattleInfo.DefRemainHpList.Sum(p => p.Damage),
            };
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取武将基本信息
        /// </summary>
        /// <param name="queueID">队列ID</param>
        /// <param name="cHeroList">战斗基本信息</param>
        /// <param name="remainHpList">剩余血量信息</param>
        /// <returns>武将信息</returns>
        private CQueueBaseInfo GetQueueBaseInfo(Int32 queueID, List<ClientHero> cHeroList, List<RemainHp> remainHpList)
        {
            var queueBaseInfo = new CQueueBaseInfo()
            {
                QueueID = queueID,
                CHeroBaseInfoList = new List<CHeroBaseInfo>(),
            };

            // 构造武将信息
            foreach (var item in cHeroList)
            {
                queueBaseInfo.CHeroBaseInfoList.Add(new CHeroBaseInfo()
                {
                    HeroID = item.HeroID,
                    Name = item.Name,
                    Formation = item.Formation,
                    Exp = item.Exp,
                    Lv = item.Lv,
                    ArmyCount = item.HP,
                    FAP = item.Fap,
                    KillCount = (remainHpList == null || remainHpList.Count <= 0) ? 0 : remainHpList.FirstOrDefault(p => p.Formation == item.Formation).Damage,
                    StarNum = item.StarNum,
                    StepNum = item.StepNum,
                    StepStarNum = item.StepStarNum
                });
            }

            return queueBaseInfo;
        }

        /// <summary>
        /// 组装战斗队列信息
        /// </summary>
        /// <returns></returns>
        private Dictionary<Guid, List<CQueueBaseInfo>> GetQueueBaseInfo()
        {
            // 组装战斗初始数据
            var dict = new Dictionary<Guid, List<CQueueBaseInfo>>();
            Action<List<QueueRemainHp>> action = (queueRemainHPList) =>
            {
                foreach (var item in queueRemainHPList)
                {
                    if (dict.ContainsKey(item.PlayerID) == false)
                    {
                        dict[item.PlayerID] = new List<CQueueBaseInfo>();
                    }

                    // 组装队列信息
                    var queueInfo = new CQueueBaseInfo
                    {
                        QueueID = item.QueueID,
                        CHeroBaseInfoList = new List<CHeroBaseInfo>(),
                    };

                    dict[item.PlayerID].Add(queueInfo);

                    // 组装基本数据
                    foreach (var remainHp in item.RemainHpList)
                    {
                        queueInfo.CHeroBaseInfoList.Add(
                        new CHeroBaseInfo()
                        {
                            ArmyCount = remainHp.MHP,
                            Formation = remainHp.Formation,
                            HeroID = remainHp.HeroID,
                            Name = String.Empty,
                            StepStarNum = remainHp.StepStarNum,
                        });
                    }
                }
            };

            // 组装基本数据
            action(FightResponseInfo.AtkRemainHpList);
            action(FightResponseInfo.DefRemainHpList);

            return dict;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 战报组装战报，并且保存战报，战报入口
        /// </summary>
        /// <returns>战报信息</returns>
        internal List<GlobalFightReport> BuildReportInfo()
        {
            var list = new List<GlobalFightReport>();
            if (FightResponseInfo.ErrorCode != ErrorCode.Success)
            {
                return list;
            }

            // 组装战斗初始数据
            var dict = GetQueueBaseInfo();
            foreach (var queueBattleInfo in FightResponseInfo.QueueBattleInfoList)
            {
                // 组装战报数据
                var reprot = new GlobalFightReport()
                {
                    ID = queueBattleInfo.ReportID,
                    AtkBaseInfoObj = GetAtkBaseInfo(queueBattleInfo),
                    DefBaseInfoObj = GetDefBaseInfo(queueBattleInfo),
                    IsWin = queueBattleInfo.IsWin,
                    AtkWinQueuNum = FightResponseInfo.AtkWinQueuNum,
                    DefWinQueuNum = FightResponseInfo.DefWinQueuNum,
                    Crdate = queueBattleInfo.Crdate,
                    FightReport = queueBattleInfo.FightReport,
                    FightReqObj = JsonUtil.Serialize(queueBattleInfo.FightObj),
                    ExtendInfo = new FightExtendInfo()
                    {
                        Round = queueBattleInfo.Round,

                        // 组装攻击方和防御方队列基本信息（武将信息）
                        AtkCQueueBaseInfo = GetQueueBaseInfo(queueBattleInfo.AtkQueueID, queueBattleInfo.FightObj.AtkTeam, queueBattleInfo.AtkRemainHpList),
                        DefCQueueBaseInfo = GetQueueBaseInfo(queueBattleInfo.DefQueueID, queueBattleInfo.FightObj.DefTeam, queueBattleInfo.DefRemainHpList),
                    }
                };

                if (dict.ContainsKey(queueBattleInfo.AtkPlayerID))
                {
                    // 组装战斗前队列信息
                    reprot.ExtendInfo.InitAtkCQueueBaseInfo = dict[queueBattleInfo.AtkPlayerID];
                }

                if (dict.ContainsKey(queueBattleInfo.DefPlayerID))
                {
                    // 组装战斗前队列信息
                    reprot.ExtendInfo.InitDefCQueueBaseInfo = dict[queueBattleInfo.DefPlayerID];
                }

                list.Add(reprot);
            }

            // 是否保存战报
            if (IsSave())
            {
                // 保存战报
            }

            return list;
        }

        #endregion
    }
}
