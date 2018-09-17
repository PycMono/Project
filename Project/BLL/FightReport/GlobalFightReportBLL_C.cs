//***********************************************************************************
// 文件名称：GlobalBattleReportBLL.cs
// 功能描述：战报信息
// 数据表：g_battle_report
// 作者：pyc
// 日期：2017/12/8 11:16:37
// 修改记录：
//***********************************************************************************
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using Moqikaka.Util.Log;

namespace FightHander.FightReport
{
    using PycMono.Project.Model;
    using Moqikaka.GameServer.SGH5.FightModel;

    /// <summary>
    /// 战报信息
    /// </summary>
    public class GlobalFightReportBLL
    {
        #region 字段

        //类名称，用于错误标识
        private const String mClassName = "GlobalBattleReportBLL";
        private static Dictionary<Guid, GlobalFightReport> mData = new Dictionary<Guid, GlobalFightReport>();

        #endregion

        #region 初始化数据

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            //using (lockObj.GetLock(mClassName, ReaderWriterLockUtil.LockTypeEnum.Writer))
            //{
            //    mData.Clear();
            //    var tempList = GlobalBattleReportDAL.GetBattleReportToID(Guid.Empty);
            //    foreach (var item in tempList)
            //    {
            //        mData[item.ID] = item;
            //    }
            //}
        }

        #endregion

        #region 内部或其它类调用的方法

        /// <summary>
        /// 获取模型数据
        /// </summary>
        /// <returns>模型数据</returns>
        public static Dictionary<Guid, GlobalFightReport> GetData()
        {
            return mData;
        }

        /// <summary>
        /// 一小时清理一次流寇战报
        /// </summary>
        /// <param name="dtNow"></param>
        public static void ClearExpiredDataToOneHour(DateTime dtNow)
        {
            try
            {
                // 清理流寇战报1小时清理一次
            }
            catch (Exception ex)
            {
                LogUtil.Write($"清理过期战报报错{ex.Message + Environment.NewLine + ex.StackTrace}", LogType.Error);
            }
        }

        #region 私有方法

        /// <summary>
        /// 获取战报数据
        /// </summary>
        /// <param name="reportID">战报ID</param>
        /// <returns>战报数据</returns>
        private static GlobalFightReport GetReport(Guid reportID)
        {
            // 从数据库加载战报

            return new GlobalFightReport();
        }

        #endregion

        #endregion

        #region 插入战报

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list"></param>
        public static void AddReport(List<GlobalFightReport> list)
        {
            // TODO 添加战报
        }

        #endregion

        //#region 组装客服端数据

        ///// <summary>
        ///// 组装战斗结果(使用在pvp之类的，打完后不进入战斗结果的场景)
        ///// </summary>
        ///// <param name="list">战报信息</param>
        ///// <returns>客服端数据</returns>
        //public static FightResponse AssembleToClient(List<GlobalFightReport> list)
        //{
        //    var clientInfo = new FightResponse();
        //    foreach (var fightReport in list)
        //    {
        //        // 组装武将基本信息
        //        var fightBaseInfo = new FightBaseInfo
        //        {
        //            FightObjReq = fightReport.FightReqObj,
        //            FightReport = fightReport.FightReport,
        //            AtkQueueID = fightReport.AtkBaseInfoObj.QueueID,
        //            DefQueueID = fightReport.DefBaseInfoObj.QueueID,
        //            IsWin = fightReport.IsWin,

        //            // 组装攻击方和防御方队列信息
        //            AtkQueueInfo = AssembleToClient(fightReport.ExtendInfo.AtkCQueueBaseInfo),
        //            DefQueueInfo = AssembleToClient(fightReport.ExtendInfo.DefCQueueBaseInfo)
        //        };

        //        clientInfo.FightInfoList.Add(fightBaseInfo);
        //    }

        //    var tempReport = list.FirstOrDefault();
        //    if (tempReport == null)
        //    {
        //        return clientInfo;
        //    }

        //    clientInfo.AtkBaseInfo = AssembleToClient(tempReport.AtkBaseInfoObj, 0);
        //    clientInfo.DefBaseInfo = AssembleToClient(tempReport.DefBaseInfoObj, 0);

        //    // 组装战斗前基本数据
        //    clientInfo.AtkQueueInfoList.Add(AssembleToClient(tempReport.ExtendInfo.InitAtkCQueueBaseInfo));
        //    clientInfo.DefQueueInfoList.Add(AssembleToClient(tempReport.ExtendInfo.InitDefCQueueBaseInfo));

        //    return clientInfo;
        //}

        ///// <summary>
        ///// 组装战斗结果(使用在副本，世界BOSS，直接展示全部战斗)
        ///// </summary>
        ///// <param name="teamCalcResponse">战斗返回结果</param>
        ///// <param name="fightReportList">战报信息</param>
        ///// <param name="getList">资源获得</param>
        ///// <returns>战斗结果</returns>
        //public static FightResponse AssembleToClient(TeamCalcResponse teamCalcResponse, List<GlobalFightReport> fightReportList, List<GameResourceObject> getList)
        //{
        //    var clientInfo = new FightResponse
        //    {
        //        IsWin = teamCalcResponse.IsWin,
        //        BaseDisplayResourceInfo = new ClientResourceDisplayInfo(),
        //        AtkWinQueuNum = teamCalcResponse.AtkWinQueuNum,
        //        DefWinQueuNum = teamCalcResponse.DefWinQueuNum
        //    };

        //    // 组装资源
        //    clientInfo.BaseDisplayResourceInfo.BaseInfo.AddRange(GameResourceBLL.AssembleToClient(getList));
        //    foreach (var item in fightReportList)
        //    {
        //        // 组装战斗基本信息
        //        var fightBaseInfo = new FightBaseInfo
        //        {
        //            FightObjReq = item.FightReqObj,
        //            FightReport = item.FightReport,
        //            AtkQueueID = item.AtkBaseInfoObj.QueueID,
        //            DefQueueID = item.DefBaseInfoObj.QueueID,
        //            IsWin = item.IsWin,

        //            // 组装战斗后队列数据
        //            AtkQueueInfo = AssembleToClient(item.ExtendInfo.AtkCQueueBaseInfo),
        //            DefQueueInfo = AssembleToClient(item.ExtendInfo.DefCQueueBaseInfo)
        //        };

        //        // 组装战报
        //        clientInfo.FightInfoList.Add(fightBaseInfo);
        //    }

        //    var atkReport = fightReportList.FirstOrDefault(t => t.AtkBaseInfoObj.QueueID > 0);
        //    var defReport = fightReportList.FirstOrDefault(t => t.DefBaseInfoObj.QueueID > 0);
        //    if (atkReport == null
        //        || defReport == null)
        //    {
        //        return clientInfo;
        //    }

        //    // 获取经验
        //    var expList = getList.Where(p => p.ResourceTypeSub == ResourceTypeSubEnum.PlayerEXP);

        //    // 组装攻击方基本信息
        //    clientInfo.AtkBaseInfo = AssembleToClient(atkReport.AtkBaseInfoObj, expList.Sum(p => p.Count));
        //    clientInfo.AtkBaseInfo.ArmyCount = teamCalcResponse.AtkMHP;// todo pyc特殊处理了兵力的
        //    clientInfo.AtkBaseInfo.KillCount = fightReportList.Sum(p => p.AtkBaseInfoObj.KillCount);

        //    // 组装防御方基本信息
        //    clientInfo.DefBaseInfo = AssembleToClient(defReport.DefBaseInfoObj, 0);
        //    clientInfo.DefBaseInfo.ArmyCount = teamCalcResponse.DefMHP;// todo pyc特殊处理了兵力的
        //    clientInfo.DefBaseInfo.KillCount = fightReportList.Sum(p => p.DefBaseInfoObj.KillCount);

        //    clientInfo.AtkQueueInfoList.Add(AssembleToClient(atkReport.ExtendInfo.InitAtkCQueueBaseInfo));
        //    clientInfo.DefQueueInfoList.Add(AssembleToClient(defReport.ExtendInfo.InitDefCQueueBaseInfo));
        //    clientInfo.AtkWinQueuNum = atkReport.AtkWinQueuNum;
        //    clientInfo.DefWinQueuNum = defReport.DefWinQueuNum;

        //    // 组装经验
        //    clientInfo.HeroExp = atkReport.ExtendInfo.HeroExp;

        //    return clientInfo;
        //}

        ///// <summary>
        ///// 组装队列基本信息（武将信息）
        ///// </summary>
        ///// <param name="queueBaseInfoList">队列基本信息</param>
        ///// <returns>武将基本信息</returns>
        //public static List<FightQueueBaseInfo> AssembleToClient(List<CQueueBaseInfo> queueBaseInfoList)
        //{
        //    var clientInfoList = new List<FightQueueBaseInfo>();
        //    if (queueBaseInfoList == null)
        //    {
        //        return clientInfoList;
        //    }

        //    foreach (var queueBaseInfo in queueBaseInfoList)
        //    {
        //        clientInfoList.Add(AssembleToClient(queueBaseInfo));
        //    }

        //    return clientInfoList;
        //}

        ///// <summary>
        ///// 组装队列基本信息（武将信息）
        ///// </summary>
        ///// <param name="queueBaseInfo">队列基本信息</param>
        ///// <returns>武将基本信息</returns>
        //public static FightQueueBaseInfo AssembleToClient(CQueueBaseInfo queueBaseInfo)
        //{
        //    var clientInfo = new FightQueueBaseInfo
        //    {
        //        QueueID = queueBaseInfo.QueueID
        //    };

        //    foreach (var item in queueBaseInfo.CHeroBaseInfoList)
        //    {
        //        clientInfo.FightHeroBaseInfoList.Add(new FightHeroBaseInfo()
        //        {
        //            HeroID = item.HeroID,
        //            Formation = item.Formation,
        //            Exp = HeroBLL.GetHeroExpOfCurrentLv(item.HeroID, item.Lv, item.Exp),// todo killer 特殊处理武将验证，前端用于进度条展示
        //            Lv = item.Lv,
        //            Name = item.Name,
        //            StarNum = item.StarNum,
        //            StepNum = item.StepNum,
        //            StepStarNum = item.StepStarNum
        //        });

        //        clientInfo.FAP += item.FAP;
        //        clientInfo.KillCount += item.KillCount;
        //        clientInfo.ArmyCount += item.ArmyCount;
        //    }

        //    return clientInfo;
        //}

        ///// <summary>
        ///// 组装玩家基本数据
        ///// </summary>
        ///// <param name="playerBaseInfo">玩家基本数据</param>
        ///// <param name="playerExp">玩家经验</param>
        ///// <returns>客服端数据</returns>
        //public static FightPlayerBaseInfo AssembleToClient(PlayerBaseInfo playerBaseInfo, Int64 playerExp)
        //{
        //    return new FightPlayerBaseInfo
        //    {
        //        PlayerID = playerBaseInfo.PlayerID.ToString(),
        //        Name = playerBaseInfo.Name,
        //        FAP = playerBaseInfo.FAP,
        //        Country = playerBaseInfo.Country,
        //        HeadImageID = playerBaseInfo.HeadImageID,
        //        Lv = playerBaseInfo.Lv,
        //        PlayerExp = playerBaseInfo.PlayerExp,
        //        AddPlayerExp = playerExp,
        //        ArmyCount = playerBaseInfo.ArmyCount,
        //        KillCount = playerBaseInfo.KillCount
        //    };
        //}

        //#endregion

        //#region 客服端调用方法

        ///// <summary>
        ///// 获取战报回放处理
        ///// </summary>
        ///// <param name="context">上下文对象</param>
        ///// <param name="paramData">武将上阵参数数据</param>
        //[ModuleInfo(GameModuleConfigAlias.CombatReplay, GameModuleSubConfigAlias.CombatReplay)]
        //public static ResultDataObject C_GetReport(Context context, GlobalFightReportGetReportRequest paramData)
        //{
        //    ResultDataObject result = new ResultDataObject() { ResultStatus = ResultStatus.ClientDataError };

        //    #region 参数校验

        //    var list = new List<GlobalFightReport>();
        //    foreach (var str in paramData.IDs)
        //    {
        //        var id = Guid.Parse(str);
        //        if (id == Guid.Empty)
        //        {
        //            continue;
        //        }

        //        // 验证战报是否存在
        //        var fightReport = GetReport(id);
        //        if (fightReport == null)
        //        {
        //            continue;
        //        }

        //        list.Add(fightReport);
        //    }

        //    // 验证战报是否存在
        //    if (list.Count <= 0)
        //    {
        //        result.ResultStatus = ResultStatus.BattleReportNotExists;

        //        return result;
        //    }

        //    #endregion

        //    #region 组装数据返还

        //    // 构造参数返回
        //    var clientResponse = new GlobalFightReportGetReportResponse
        //    {
        //        FightResponseInfo = AssembleToClient(list)
        //    };

        //    result.ResultStatus = ResultStatus.Success;
        //    result.Value = clientResponse;

        //    return result;

        //    #endregion
        //}

        //#endregion
    }
}