//***********************************************************************************
// 文件名称：ResourceBase.cs
// 功能描述：资源处理父类
// 数据表：无
// 作者：pyc
// 日期：2018/10/21 11:16:37
// 修改记录：
//***********************************************************************************

using System;
using System.Linq;
using System.Collections.Generic;

namespace PycMono.Project.GameResource
{
    using PycMono.Project.Model;
    using PycMono.Project.Model.Enum;

    /// <summary>
    /// 资源处理父类
    /// </summary>
    internal abstract class ResourceBase
    {
        /// <summary>
        /// 获取资源类型
        /// </summary>
        internal abstract ResourceTypeSubEnum ResourceType { get; }

        /// <summary>
        /// 资源是否足够
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="obj">资源对象</param>
        /// <returns>ture：足够，反之不足够（最好换成自己的错误码，提示更准确的信息）</returns>
        internal abstract Boolean IsEnough(Player player, GameResourceObject obj);

        /// <summary>
        /// 检查资源模型是否存在
        /// </summary>
        /// <param name="modelID">模型ID</param>
        /// <returns>检查资源是否存在，如果存在，则返回true；否则返回false</returns>
        internal abstract Boolean IfModelExists(Int32 modelID);

        /// <summary>
        /// 玩家获得资源前处理函数
        /// </summary>
        /// <param name="player">玩家实例</param>
        /// <param name="subModuleID">模块ID</param>
        /// <param name="list">得到的资源集合</param>
        /// <returns>处理之后的资源集合</returns>
        internal abstract List<GameResourceObject> GetBeforeHandle(Player player, Int32 subModuleID, List<GameResourceObject> list);

        /// <summary>
        /// 玩家获得资源函数
        /// </summary>
        /// <param name="player">玩家实例</param>
        /// <param name="subModuleID">模块ID</param>
        /// <param name="list">得到的资源集合</param>
        /// <param name="isAsyn">是否异步</param>
        /// <returns>处理之后的资源集合</returns>
        internal abstract List<GameResourceObject> GetResources(Player player, Int32 subModuleID, List<GameResourceObject> list, Boolean isAsyn);

        /// <summary>
        /// 玩家消耗资源函数
        /// </summary>
        /// <param name="player">玩家实例</param>
        /// <param name="subModuleID">模块ID</param>
        /// <param name="isAsyn">是否异步</param>
        /// <param name="list">消耗的资源集合</param>
        internal abstract void ConsumeResources(Player player, Int32 subModuleID, List<GameResourceObject> list, Boolean isAsyn);

        /// <summary>
        /// 获取可以消耗的资源
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="obj">要组装的消耗资源</param>
        /// <returns>组装之后的资源</returns>
        internal abstract List<GameResourceObject> BuildCanConsumeResource(Player player, GameResourceObject obj);

        /// <summary>
        /// 获取资源中文名字（去对应的模型配置里面找）
        /// </summary>
        /// <param name="modelID">模型ID</param>
        /// <returns>资源对应的中文名字</returns>
        internal virtual String GetChineseName(Int32 modelID)
        {
            return String.Empty;
        }

        /// <summary>
        /// 组装消耗数据的实体id
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="obj">要组装的消耗资源</param>
        /// <param name="consumeIds">要消耗的实体id</param>
        /// <returns>组装之后的资源</returns>
        internal virtual List<GameResourceObject> BuildConsumeResource(Player player, GameResourceObject obj, Guid[] consumeIds)
        {
            throw new Exception($"未实现的资源组装。ResourceTypeSub={obj.ResourceTypeSub.ToString()}");
        }

        /// <summary>
        /// 资源处理结束以后的收尾操作
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="getGameResourceList">获得资源</param>
        /// <param name="consumeGameResourceList">消耗资源</param>
        internal virtual void EndHandle(Player player, IEnumerable<GameResourceObject> getGameResourceList, IEnumerable<GameResourceObject> consumeGameResourceList)
        {

        }

        /// <summary>
        /// 组装客户端数据
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="obj">资源对象</param>
        /// <returns>组装客服端数据</returns>
        internal abstract Dictionary<String, Object> AssembleToClient(Player player, GameResourceObject obj);
    }
}
