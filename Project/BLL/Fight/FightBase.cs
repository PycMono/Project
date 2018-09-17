//***********************************************************************************
//文件名称：FightBase.cs
//功能描述：GameServer战斗父类
//数据表：Nothing
//作者：彭亚川
//日期：2018/11/9 17:17:04
//修改记录：
//***********************************************************************************

using System;
using System.IO;
using System.Collections.Generic;


namespace FightHander.Fight
{
    using Moqikaka.Util.Log;
    using Moqikaka.Util.Web;
    using Moqikaka.Util.Json;
    using PycMono.Project.Model;
    using PycMono.Project.Model.Enum;
    using Moqikaka.GameServer.SGH5.FightModel;

    /// <summary>
    /// GameServer战斗父类
    /// </summary>
    internal abstract class FightBase
    {
        #region 字段

        //类名称，用于错误标识
        private const String mClassName = "FightBase";
        private static readonly String mVerifyFightUrl = String.Empty;
        protected static String mFightInfoFolder = String.Empty;//战报存放的根目录
        private static readonly String splitConten = "==============================================================================";
        private static readonly String splitConten1 = "------------------------------------------------------------------------------------------------------------------------------------";

        #endregion

        #region 初始化

        /// <summary>
        /// 静态构造方法（我们游戏里面会有逻辑，在服务器启动的时候就调用这个静态方法的，这边没有这种逻辑，所以是首次调用这个类的方法的时候调用这个静态构造方法）
        /// </summary>
        static FightBase()
        {
            mVerifyFightUrl = // "http://localhost:57937/Fight.aspx";// 战斗服务器地址，开发的时候替换成自己的战斗服务器配置地址
            mFightInfoFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BattleReport");
        }

        #endregion

        #region 组装战斗数据

        /// <summary>
        /// 组装战斗队列
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="queueIDList">要组装的队伍ID</param>
        /// <returns></returns>
        protected List<ClientQueue> BuildQueue(Player player, List<Int32> queueIDList)
        {
            return BuildQueue(player, queueIDList);
        }

        /// <summary>
        /// 组装战斗队列
        /// </summary>
        /// <param name="player">玩家对象</param> 
        /// <param name="queueList">要组装的队伍id</param>
        /// <returns></returns>
        protected List<ClientQueue> BuildQueue(Player player, List<BattleQueueInfo> queueList)
        {
            var list = new List<ClientQueue>();
            foreach (var battleFormation in queueList)
            {
                // 构造队列对象
                var cQueue = new ClientQueue() { PlayerID = player.ID, PlayerName = player.Name, QueueList = new List<ClientHero>(), QueueID = battleFormation.QueueID };
                var psotList = battleFormation.GetItem_ToList();
                foreach (var item in psotList)
                {
                    // 如果武将ID为null不用组装
                    if (item.HeroID == Guid.Empty || item.ArmyCount == 0)
                    {
                        continue;
                    }

                    // TODO 从玩家缓存里面获取hero对象，我这里级直接new了
                    var hero = new Hero();

                    // 组装槽位数据
                    cQueue.QueueList.Add(BuildClientHero(hero));
                }

                if (cQueue.QueueList.Count > 0)
                {
                    list.Add(cQueue);
                }
            }

            return list;
        }

        /// <summary>
        /// 组装hero数据
        /// </summary>
        /// <returns>槽位数据</returns>
        protected ClientHero BuildClientHero()
        {
            /**
             * 参数判断......此处省略
             *
             *
             **/

            // 获取学员信息，这里面参数省略......
            return BuildClientHero();
        }

        /// <summary>
        /// 组装武将数据
        /// </summary>
        /// <returns>客户端hero对象</returns>
        protected ClientHero BuildClientHero(Hero hero)
        {
            var cHero = new ClientHero
            {
                BuffList = new List<Int32>(),
                HeroID = hero.ModelID,
            };

            var partAttr = hero.PartAttr;

            // 如果是副本特殊处理,当前血量就是最大血量
            cHero.HP = cHero.MHP = partAttr.GetValue(SlotAttrEnum.MHP);

            // 设置基本属性
            cHero.ATK = partAttr.GetValue(SlotAttrEnum.ATK);
            cHero.DEF = partAttr.GetValue(SlotAttrEnum.DEF);
            cHero.AtkDistance = 0;// 这里只是包含几个典型的对象，其他的结合自己项目处理
            cHero.CRI = 0;
            cHero.DOD = 0;
            cHero.HIT = 0;
            cHero.NATKR = 0;
            cHero.NDEFR = 0;
            cHero.SATKR = 0;
            cHero.SDEFR = 0;
            cHero.TEN = 0;

            return cHero;
        }

        #endregion

        #region 内部方法

        /// <summary>
        /// 启动游戏世界需要检查战斗服务器是否正常，如果战斗服务器检测不通过，不允许游戏启动
        /// </summary>
        /// <returns>如果战斗服务器正常，则返回true，否则返回false</returns>
        internal static Boolean CheckFightServerStatus()
        {
            try
            {
                WebUtil.GetWebData(mVerifyFightUrl, "", DataCompress.NotCompress);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("验证战斗服务器错误：" + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// 战斗校验
        /// </summary>
        /// <param name="cr"></param>
        /// <returns></returns>
        protected TeamCalcResponse TeamVerifyFight(TeamCalcRequest cr, String tag)
        {
            var verifyResult = new TeamCalcResponse();
            try
            {
                var heards = new Dictionary<String, String>
                {
                    {"Tag",tag}
                };

                verifyResult = Verify<TeamCalcResponse>(cr, heards);
                if (verifyResult.ErrorCode != ErrorCode.Success)// todo 要善于记录日志，方便查看报错来源
                {
                    LogUtil.Write($"FightBase.TeamVerifyFight：调用战斗服务器报错ErrorCode={verifyResult.ErrorCode},ErrorMsg={verifyResult.ErrorMsg},{Environment.NewLine},ReqStr={JsonUtil.Serialize(cr)}", LogType.Error);
                }
            }
            catch (Exception ex)
            {
                LogUtil.Write($"FightBase.TeamVerifyFight：调用战斗服务器报错ErrorMsg={ex.Message + Environment.NewLine + ex.StackTrace}", LogType.Error);
            }

            return verifyResult;
        }

        /// <summary>
        /// 战斗校验
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="req">请求对象</param>
        /// <param name="heards">头部信息</param>
        /// <returns>返回类型的实例对象</returns>
        private T Verify<T>(Object req, Dictionary<String, String> heards)
        {
            //请求获取返回数据
            var requestContent = JsonUtil.Serialize(req);
            var responseContet = WebUtil.PostWebData(mVerifyFightUrl, requestContent, DataCompress.NotCompress, heards);

            return JsonUtil.Deserialize<T>(responseContet);
        }

        #endregion
    }
}
