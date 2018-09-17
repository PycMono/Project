//***********************************************************************************
// 文件名称：Hero.cs
// 功能描述：武将对象
// 数据表：无
// 作者：Allen 彭亚川
// 日期：2017/5/23 13:09:47
// 修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;
namespace PycMono.Project.Slot
{
    /// <summary>
    /// 玩家武将信息表
    /// </summary>
    public sealed class Hero
    {
        #region 属性

        /// <summary>
        /// 武将唯一ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 武将模型iD
        /// </summary>
        public Int32 ModelID { get; set; }

        /// <summary>
        /// 武将等级
        /// </summary>
        public Int32 Lv { get; set; }

        #endregion

        #region 辅助属性

        /// <summary>
        /// 卡槽属性集合（模块对应的属性对象，只是方便查看数据，哪些模块加了多少，好定为属性来源）
        /// </summary>
        public Dictionary<SlotAttrTypeEnum, PartAttr> PartAttrs { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public PartAttr PartAttr { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public Hero()
        {
            PartAttrs = new Dictionary<SlotAttrTypeEnum, PartAttr>();
            PartAttr = new PartAttr();
        }

        #endregion
    }
}