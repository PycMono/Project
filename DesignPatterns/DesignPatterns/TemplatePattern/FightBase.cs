//***********************************************************************************
// 文件名称：FightBase.cs
// 功能描述：战斗父类（模拟模板模式）
// 数据表：无
// 作者：killer
// 日期：2018/010/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;

namespace DesignPatterns.TemplatePattern
{
    using Moqikaka.Util.Json;

    /// <summary>
    /// 战斗父类（模拟模板模式）
    /// </summary>
    public abstract class FightBase
    {
        /// <summary>
        /// 获取攻击方数据
        /// </summary>
        protected abstract List<Int32> GetAtkObj();

        /// <summary>
        /// 获取防御方数据
        /// </summary>
        protected abstract List<Int32> GetDefObj();

        /// <summary>
        /// 构造战斗信息
        /// </summary>
        public void BuildFight()
        {
            var atkList = GetAtkObj();
            var defList = GetDefObj();
            Console.WriteLine($"攻击方数据{JsonUtil.Serialize(atkList)}");
            Console.WriteLine($"防御方数据{JsonUtil.Serialize(defList)}");
        }
    }
}
