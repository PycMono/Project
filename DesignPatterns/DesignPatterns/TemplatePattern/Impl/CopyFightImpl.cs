//***********************************************************************************
// 文件名称：CopyFightImpl.cs
// 功能描述：副本战斗实现类
// 数据表：无
// 作者：killer
// 日期：2018/010/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;

namespace DesignPatterns.TemplatePattern
{
    /// <summary>
    /// 副本战斗实现类
    /// </summary>
    public class CopyFightImpl : FightBase
    {
        /// <summary>
        ///  攻击方数据
        /// </summary>
        /// <returns></returns>
        protected override List<Int32> GetAtkObj()
        {
            return new List<Int32>() { 1 };
        }

        /// <summary>
        /// 防御方数据
        /// </summary>
        /// <returns></returns>
        protected override List<Int32> GetDefObj()
        {
            return new List<Int32>() { 2 };
        }
    }
}
