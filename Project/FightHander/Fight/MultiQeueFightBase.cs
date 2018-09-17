//***********************************************************************************
//文件名称：MultiQeueFightBase.cs
//功能描述：多队列战斗父类(可能还存在单队列，或者车轮战、等等类型的战斗父类)
//数据表：Nothing
//作者：pyc
//日期：2017/12/07 17:32:33
//修改记录：
//***********************************************************************************

using System.Collections.Generic;

namespace FightHander.Fight
{
    using Moqikaka.GameServer.SGH5.FightModel;

    /// <summary>
    /// 多队列战斗父类(可能还存在单队列，或者车轮战、等等类型的战斗父类)
    /// </summary>
    internal abstract class MultiQeueFightBase : FightBase
    {
        #region 可重载方法

        /// <summary>
        /// 获取攻击方信息
        /// </summary>
        /// <returns></returns>
        protected abstract List<ClientQueue> GetAtkQueue();// 这里可以改成virtual函数

        /// <summary>
        /// 获取防御方队列
        /// </summary>
        /// <returns></returns>
        protected abstract List<ClientQueue> GetDefQueue();

        #endregion

        #region 公共方法

        /// <summary>
        /// 校验战斗
        /// </summary>
        /// <returns>战斗结果</returns>
        internal TeamCalcResponse Verify()
        {
            var atkFightObjs = GetAtkQueue(); // 攻击方数据
            var defFightObjs = GetDefQueue(); // 防御方参战对象

            // 组装战斗对象
            var teamCalcReq = new TeamCalcRequest
            {
                FullName = this.GetType().FullName,
                IsSaveHP = true,
                AtkQueueList = atkFightObjs,
                DefQueueList = defFightObjs,
            };

            var calcRes = TeamVerifyFight(teamCalcReq, "TeamFight");// 战斗校验
            if (calcRes.ErrorCode == ErrorCode.Success)
            {
                // 事件触发等操作
            }

            return calcRes;
        }

        #endregion
    }
}
