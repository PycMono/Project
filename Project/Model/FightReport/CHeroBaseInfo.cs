//***********************************************************************************
// 文件名称：CHeroBaseInfo.cs
// 功能描述：客服端武将基本数据（战报）
// 数据表：无
// 作者：pyc
// 日期：2017/12/8 11:16:37
// 修改记录：
//***********************************************************************************
using System;

namespace PycMono.Project.Model
{
    /// <summary>
    /// 客服端武将基本数据（战报）
    /// </summary>
    public class CHeroBaseInfo
    {
        /// <summary>
        /// 武将ID
        /// </summary>
        public Int32 HeroID { get; set; }

        /// <summary>
        /// 武将名字
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 武将站位
        /// </summary>
        public Int32 Formation { get; set; }

        /// <summary>
        /// 武将等级
        /// </summary>
        public Int32 Lv { get; set; }

        /// <summary>
        /// 武将经验
        /// </summary>
        public Int64 Exp { get; set; }

        /// <summary>
        /// 武将进阶值
        /// </summary>
        public Int32 StepNum { get; set; }

        /// <summary>
        /// 进阶星数
        /// </summary>
        public Int32 StepStarNum { get; set; }

        /// <summary>
        /// 武将突破星数
        /// </summary>
        public Int32 StarNum { get; set; }

        /// <summary>
        /// 带兵总数
        /// </summary>
        public Int64 ArmyCount { get; set; }

        /// <summary>
        /// 击杀总数
        /// </summary>
        public Int64 KillCount { get; set; }

        /// <summary>
        /// 战斗力
        /// </summary>
        public Int64 FAP { get; set; }
    }
}
