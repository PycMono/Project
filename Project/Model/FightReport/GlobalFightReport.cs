//***********************************************************************************
// 文件名称：GlobalFightReport.cs
// 功能描述：战报信息
// 数据表：g_fight_report
// 作者：pyc
// 日期：2017/12/8 11:16:37
// 修改记录：
//***********************************************************************************
using System;
using System.Collections.Generic;

namespace PycMono.Project.Model
{
    using Moqikaka.Util.Json;

    /// <summary>
    /// 战报信息
    /// </summary>
    public sealed class GlobalFightReport
    {
        #region 属性

        /// <summary>
        /// 战报ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 战斗模块ID
        /// </summary>
        public Int32 ModuleID { get; set; }

        /// <summary>
        /// 是否胜利
        /// </summary>
        public Boolean IsWin { get; set; }

        /// <summary>
        /// 战斗请求数据
        /// </summary>
        public String FightReqObj { get; set; }

        /// <summary>
        /// 战报数据
        /// </summary>
        public String FightReport { get; set; }

        /// <summary>
        /// 攻击方信息
        /// </summary>
        public PlayerBaseInfo AtkBaseInfoObj { get; set; }

        /// <summary>
        /// 防御方基本信息
        /// </summary>
        public PlayerBaseInfo DefBaseInfoObj { get; set; }

        /// <summary>
        /// 战斗时间
        /// </summary>
        public DateTime Crdate { get; set; }

        /// <summary>
        /// 攻击方胜利队列数
        /// </summary>
        public Int32 AtkWinQueuNum { get; set; }

        /// <summary>
        /// 防御方胜利队列数量
        /// </summary>
        public Int32 DefWinQueuNum { get; set; }


        /// <summary>
        /// 扩展信息
        /// </summary>
        public FightExtendInfo ExtendInfo { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public GlobalFightReport()
        {

        }

        #endregion
    }

    /// <summary>
    /// 战报扩展信息
    /// </summary>
    public class FightExtendInfo
    {
        /// <summary>
        /// 获得资源字符串
        /// </summary>
        public String GetGameResource { get; set; }

        /// <summary>
        /// 武将获得经验
        /// </summary>
        public Int32 HeroExp { get; set; }

        /// <summary>
        /// 当前战报进行的轮次
        /// </summary>
        public Int32 Round { get; set; }

        /// <summary>
        /// 战斗后供给方队列信息
        /// </summary>
        public CQueueBaseInfo AtkCQueueBaseInfo { get; set; }

        /// <summary>
        /// 战斗后防御方队列信息
        /// </summary>
        public CQueueBaseInfo DefCQueueBaseInfo { get; set; }

        /// <summary>
        /// 供给方队列初始信息
        /// </summary>
        public List<CQueueBaseInfo> InitAtkCQueueBaseInfo { get; set; }

        /// <summary>
        /// 防御方队列初始信息
        /// </summary>
        public List<CQueueBaseInfo> InitDefCQueueBaseInfo { get; set; }
    }
}