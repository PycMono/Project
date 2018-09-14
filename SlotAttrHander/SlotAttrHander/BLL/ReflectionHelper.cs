//***********************************************************************************
//文件名称：ReflectionHelper.cs
//功能描述：
//数据表：Nothing
//作者：彭亚川
//日期：2016/7/8 16:56:32
//修改记录：
//***********************************************************************************

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace PycMono.Project.Slot
{
    /// <summary>
    /// 反射辅助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 获取实现了接口的类型列表
        /// </summary>
        /// <param name="interfaceTypes">接口类型集合</param>
        /// <returns>类型</returns>
        public static List<Type> GetTypeListOfImplementedInterface(params Type[] interfaceTypes)
        {
            var typeList = new List<Type>();

            //获取整个应用程序集的类型数组
            var types = Assembly.GetExecutingAssembly().GetTypes();
            if (types == null || types.Length == 0) return typeList;

            //遍历每一个类型，看看是否实现了指定的接口
            foreach (Type type in types)
            {
                //获得此类型所实现的所有接口列表
                Type[] allInterfaces = type.GetInterfaces();

                //判断给出的接口类型列表是否存在于实现的所有接口列表中
                if (interfaceTypes.All(item => allInterfaces.Contains(item)))
                {
                    typeList.Add(type);
                }
            }

            return typeList;
        }
    }
}
