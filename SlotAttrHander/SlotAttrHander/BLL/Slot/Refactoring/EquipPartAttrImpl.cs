//***********************************************************************************
//文件名称：EquipPartAttrImpl.cs
//功能描述：装备属性卡槽逻辑类
//数据表：无
//作者：彭亚川
//日期：2017/3/9 18:11:51
//修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;

namespace PycMono.Project.Slot
{
    /// <summary>
    /// 装备属性卡槽逻辑类
    /// </summary>
    public class EquipPartAttrImpl : ISlotPartAttr
    {
        public SlotAttrTypeEnum AttrType
        {
            get { return SlotAttrTypeEnum.Equip; }
        }

        /// <summary>
        /// 返回受影响的卡槽
        /// </summary>
        /// <param name="info">受影响的队列信息</param>
        /// <returns>卡槽对应的属性</returns>
        public Dictionary<Guid, PartAttr> CalcAttr(SlotCalcObj info)
        {
            var result = new Dictionary<Guid, PartAttr>();
            foreach (var hero in info.ChangeSlotList)
            {
                var pa = new PartAttr(AttrType);
                result[hero.ID] = pa;

                // 增加属性调用pa.AddValue()就ok;
            }

            return result;
        }
    }
}
