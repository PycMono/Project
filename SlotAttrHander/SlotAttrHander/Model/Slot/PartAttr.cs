//***********************************************************************************
// 文件名称：PartAttr.cs
// 功能描述：属性处理逻辑类
// 数据表：无
// 作者：Allen 彭亚川
// 日期：2017/5/23 13:09:47
// 修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;

namespace PycMono.Project.Slot
{
    using Moqikaka.Util.Json;

    /// <summary>
    /// 属性处理逻辑类
    /// </summary>
    public class PartAttr
    {
        #region 字段

        private Int64 mHP = 0;
        private Int32 mATK = 0;
        private Int32 mDEF = 0;

        // 你可以定义其他字段

        #endregion

        #region 属性

        /// <summary>
        /// 生命              
        /// </summary>       
        public Int64 HP
        {
            get { return mHP; }
            set
            {
                if (value != mHP)
                {
                    mHP = value;
                }
            }
        }

        /// <summary>
        /// 攻击
        /// </summary>
        public Int32 ATK
        {
            get { return mATK; }
            set
            {
                if (value != mATK)
                {
                    mATK = value;
                }
            }
        }

        /// <summary>
        /// 防御              
        /// </summary>       
        public Int32 DEF
        {
            get { return mDEF; }
            set
            {
                if (value != mDEF)
                {
                    mDEF = value;
                }
            }
        }

        #endregion

        #region 辅助属性

        /// <summary>
        /// 属性类型ID
        /// </summary>
        public Int32 SlotAttrTypeID { get; set; }

        #endregion

        #region 构造方法

        public PartAttr()
        {

        }

        public PartAttr(SlotAttrTypeEnum type)
        {
            this.SlotAttrTypeID = (Int32)type;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        public PartAttr DeepClone()
        {
            var content = JsonUtil.Serialize(this);
            return JsonUtil.Deserialize<PartAttr>(content);
        }

        /// <summary>
        /// 重置对象为初始化数据
        /// </summary>
        public void ResetAttr()
        {
            this.HP = 0;
            this.ATK = 0;
            this.DEF = 0;
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="partAttr">属性</param>
        public void SetAttr(PartAttr partAttr)
        {
            this.HP += partAttr.HP;
            this.ATK += partAttr.ATK;
            this.DEF += partAttr.DEF;
        }

        /// <summary>
        /// 增加属性
        /// </summary>
        /// <param name="fae">属性枚举</param>
        /// <param name="value">增加的值</param>
        public void AddValue(SlotAttrEnum fae, Int32 value)
        {
            switch (fae)
            {
                case SlotAttrEnum.HP:
                    this.HP += value;
                    break;
                case SlotAttrEnum.ATK:
                    this.ATK += value;
                    break;
                case SlotAttrEnum.DEF:
                    this.DEF += value;
                    break;
                default:
                    throw new Exception($"Slot.AddValue未实现SlotAttrEnum={fae.ToString()}的属性计算。");
            }
        }

        /// <summary>
        /// 增加属性
        /// </summary>
        /// <param name="fae">属性枚举值</param>
        /// <param name="value">增加的值</param>
        public void AddValue(Int32 fae, Int32 value)
        {
            AddValue((SlotAttrEnum)fae, value);
        }

        /// <summary>
        /// 增加属性
        /// </summary>
        /// <param name="addAttr">增加的属性</param>
        public void AddValue(Dictionary<Int32, Int32> addAttr)
        {
            foreach (var item in addAttr)
            {
                AddValue(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 增加属性
        /// </summary>
        /// <param name="addAttr"></param>
        public void AddValue(List<Dictionary<Int32, Int32>> addAttr)
        {
            foreach (var item in addAttr)
            {
                AddValue(item);
            }
        }

        /// <summary>
        /// 增加属性
        /// </summary>
        /// <param name="addAttr">增加的属性</param>
        public void AddValue(Dictionary<SlotAttrEnum, Int32> addAttr)
        {
            foreach (var item in addAttr)
            {
                AddValue(item.Key, item.Value);
            }
        }

        #endregion
    }
}
