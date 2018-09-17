//***********************************************************************************
//文件名称：SlotPartAttrHanlder.cs
//功能描述：自定义描述内容类
//数据表：Nothing
//作者：彭亚川
//日期：2016/11/9 17:17:04
//修改记录：
//***********************************************************************************

using System;
using System.Linq;
using System.Collections.Generic;

namespace PycMono.Project.Slot
{
    using PycMono.Project.Model;
    using PycMono.Project.Model.Enum;

    /// <summary>
    /// 卡槽属性处理
    /// </summary>
    public class SlotPartAttrHanlder
    {
        #region 变量

        private const String mClassName = "SlotPartAttrHanlder";

        // 子属性处理字典
        private static Dictionary<SlotAttrTypeEnum, ISlotPartAttr> partAttrMethodDict = new Dictionary<SlotAttrTypeEnum, ISlotPartAttr>();

        #endregion

        #region 初始化

        /// <summary>
        /// 静态初始化信息
        /// </summary>
        static SlotPartAttrHanlder()
        {
            InitMethodInfo();
        }

        #endregion

        #region 属性计算(公共方法)

        /// <summary>
        /// 计算卡槽属性
        /// </summary>
        /// <param name="heroList">武将集合</param>
        public static void CalcAllPartAttr(List<Hero> heroList)
        {
            SlotCalcObj info = new SlotCalcObj()
            {
                ChangeSlotList = heroList,
            };

            // 计算各个模块属性,以及额外的属性
            foreach (SlotAttrTypeEnum attrType in Enum.GetValues(typeof(SlotAttrTypeEnum)))
            {
                var resultChangeSlotDict = partAttrMethodDict[attrType].CalcAttr(info);
                foreach (var resultItem in resultChangeSlotDict)
                {
                    var tempHero = info.ChangeSlotList.FirstOrDefault(item => item.ID == resultItem.Key);
                    if (tempHero == null)
                    {
                        continue;
                    }

                    tempHero.PartAttrs[attrType] = resultItem.Value;
                }
            }

            // 汇总属性
            foreach (var hero in info.ChangeSlotList)
            {
                // 合并基本属性
                MergeBaseAttr(hero); // 这里写了三个方法，自己可以酌情合并方法，一定要按照这个步奏来处理，有些百分比加成要在所有属性之后处理的

                /**
                 * 这里可以进行其他处理
                 * 
                 * 
                 **/

                SaveAttr(hero);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 合并属性
        /// </summary>
        /// <param name="hero">武将对象</param>
        private static void MergeBaseAttr(Hero hero)
        {
            // 重置属性
            hero.PartAttr.ResetAttr();
            foreach (var item in hero.PartAttrs.Values)
            {
                // 设置属性
                hero.PartAttr.MergeAttr(item);
            }

            // 合并完成之后可以对立面的属性进行其他处理，具体处理看项目需求
        }

        /// <summary>
        /// 保存属性信息
        /// </summary>
        /// <param name="hero">武将信息</param>
        private static void SaveAttr(Hero hero)
        {
            // 具体操作威胁
        }

        #region 扫描存入字典

        /// <summary>
        /// 初始化小红点方法信息
        /// </summary>
        private static void InitMethodInfo()
        {
            partAttrMethodDict.Clear();

            var implTypes = ReflectionHelper.GetTypeListOfImplementedInterface(typeof(ISlotPartAttr));
            if (implTypes == null || !implTypes.Any())
            {
                return;
            }

            foreach (var type in implTypes)
            {
                // 使用接口来生成对象
                var item = type.Assembly.CreateInstance(type.FullName) as ISlotPartAttr;
                if (partAttrMethodDict.ContainsKey(item.AttrType))
                {
                    throw new Exception($"已经存在AttrType={item.AttrType}的卡槽子属性计算实现");
                }

                partAttrMethodDict[item.AttrType] = item;
            }
        }

        #endregion

        #endregion
    }
}
