//***********************************************************************************
// 文件名称：PartAttr.cs
// 功能描述：属性处理逻辑类
// 数据表：无
// 作者：Allen 彭亚川
// 日期：2017/5/23 13:09:47
// 修改记录：
//***********************************************************************************

using System;
using System.Linq;
using System.Collections.Generic;

namespace PycMono.Project.Model
{
    using Moqikaka.Util.Json;
    using PycMono.Project.Model.Enum;

    /// <summary>
    /// 属性处理逻辑类
    /// </summary>
    public class PartAttr
    {
        #region 字段

        /// <summary>
        /// 枚举属性值对应具体的加成数量
        /// </summary>
        public Dictionary<SlotAttrEnum, Int32> FightParAttrDict =
            new Dictionary<SlotAttrEnum, Int32>();

        #endregion

        #region 辅助属性

        /// <summary>
        /// 属性类型ID
        /// </summary>
        public Int32 SlotAttrTypeID { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public PartAttr()
        {
            InitFightDict();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">属性枚举</param>
        public PartAttr(SlotAttrTypeEnum type)
        {
            this.SlotAttrTypeID = (Int32)type;
            InitFightDict();
        }

        /// <summary>
        /// 初始化战斗字典
        /// </summary>
        private void InitFightDict()
        {
            // 设置各个模块属性
            foreach (SlotAttrEnum attrType in System.Enum.GetValues(typeof(SlotAttrEnum)))
            {
                FightParAttrDict[attrType] = 0;
            }
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
        /// 重置属性
        /// </summary>
        public void ResetAttr()
        {
            foreach (var enumID in FightParAttrDict.Keys.ToList())
            {
                FightParAttrDict[enumID] = 0;
            }
        }

        /// <summary>
        /// 合并属性
        /// </summary>
        /// <param name="partAttr">属性</param>
        public void MergeAttr(PartAttr partAttr)
        {
            foreach (var keyOrValue in partAttr.FightParAttrDict)
            {
                FightParAttrDict[keyOrValue.Key] += keyOrValue.Value;
            }
        }

        /// <summary>
        /// 设置单个属性的值
        /// </summary>
        /// <param name="fae">属性枚举</param>
        /// <param name="value">增加的值</param>
        public void AddSingleValue(SlotAttrEnum fae, Int32 value)
        {
            if (!FightParAttrDict.ContainsKey(fae))
            {
                throw new Exception($"PartAttr.AddSingleValue未实现FightAttrEnum={fae.ToString()}的属性计算。");
            }

            // 设置值
            FightParAttrDict[fae] = value;
        }

        /// <summary>
        /// 增加属性
        /// </summary>
        /// <param name="fae">属性枚举</param>
        /// <param name="value">增加的值</param>
        public void AddValue(SlotAttrEnum fae, Int32 value)
        {
            if (!FightParAttrDict.ContainsKey(fae))
            {
                throw new Exception($"PartAttr.AddValue未实现FightAttrEnum={fae.ToString()}的属性计算。");
            }

            // 累加值
            FightParAttrDict[fae] += value;
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
        /// <param name="addAttr">属性集合</param>
        public void AddValue(List<Dictionary<Int32, Int32>> addAttr)
        {
            foreach (var item in addAttr)
            {
                AddValue(item);
            }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="fae">属性枚举</param>
        public Int32 GetValue(SlotAttrEnum fae)
        {
            if (!FightParAttrDict.ContainsKey(fae))
            {
                throw new Exception($"PartAttr.GetValue未实现FightAttrEnum={fae.ToString()}的属性计算。");
            }

            // 设置值
            return FightParAttrDict[fae];
        }

        #endregion
    }
}
