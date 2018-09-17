//***********************************************************************************
// 文件名称：BattlePositionObj.cs
// 功能描述：槽位对象
// 数据表：无
// 作者：pyc
// 日期：2017/12/4 17:22:04
//修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PycMono.Project.Model
{
    /// <summary>
    /// 槽位对象
    /// </summary>
    public class SlotObj
    {
        /// <summary>
        /// 位置Id
        /// </summary>
        public Int32 SlotID { get; set; }

        /// <summary>
        /// 武将ID
        /// </summary>
        public Guid HeroID { get; set; }

        /// <summary>
        /// 军队数据量
        /// </summary>
        public Int64 ArmyCount { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Crdate { get; set; }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SlotObj()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="positionID">位置ＩＤ</param>
        /// <param name="heroID">武将ID</param>
        /// <param name="armyCount">补兵数量</param>
        /// <param name="crdate">创建时间</param>
        public SlotObj(Int32 positionID,Guid heroID,Int32 armyCount,DateTime crdate)
        {
            this.SlotID = positionID;
            this.HeroID = heroID;
            this.ArmyCount = armyCount;
            this.Crdate = crdate;
        }
    }
}
