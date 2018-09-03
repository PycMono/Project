//***********************************************************************************
// 文件名称：ISlotPartAttr.cs
// 功能描述：改变属性的模块接口
// 数据表：无
// 作者：Allen 彭亚川
// 日期：2017/5/23 13:09:47
// 修改记录：
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PycMono.Project.Slot
{
    /// <summary>
    /// 改变属性的模块接口
    /// </summary>
    internal interface ISlotPartAttr
    {
        /// <summary>
        /// 卡槽属性类型
        /// </summary>
        SlotAttrTypeEnum AttrType { get; }

        /// <summary>
        /// 计算卡槽属性
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="info">卡槽对象信息</param>
        /// <returns>卡槽计算属性</returns>
        Dictionary<Guid, PartAttr> CalcAttr(Player player, SlotCalcObj info);
    }
}
