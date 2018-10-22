//***********************************************************************************
// 文件名称：GameResourceHandleBLL.cs
// 功能描述：资源处理类
// 数据表：无
// 作者：pyc
// 日期：2018/10/21 11:16:37
// 修改记录：
//***********************************************************************************

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Transactions;

namespace PycMono.Project.GameResource
{
    using Moqikaka.Util;
    using PycMono.Project.Model;
    using Moqikaka.Util.Conversion;
    using PycMono.Project.Model.Enum;

    /// <summary>
    /// 资源处理类
    /// </summary>
    class GameResourceHandleBLL
    {
        #region 字段

        //类名称，用于错误标识
        private const String mClassName = "GameResourceBLL";
        private static Dictionary<ResourceTypeSubEnum, ResourceBase> mResourceTypeImpl = new Dictionary<ResourceTypeSubEnum, ResourceBase>();//资源处理集合

        #endregion

        #region 初始化

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static GameResourceHandleBLL()
        {
            lock (mClassName)
            {
                mResourceTypeImpl.Clear();

                var types = Assembly.GetExecutingAssembly().GetTypes();
                if (types == null || types.Count() == 0)
                {
                    throw new Exception("没有任何资源实现。");
                }

                foreach (var type in types)
                {
                    if (!type.IsSubclassOf(typeof(ResourceBase)) || type.IsAbstract != false)
                    {
                        continue;
                    }

                    var tempImpl = type.Assembly.CreateInstance(type.FullName) as ResourceBase;
                    if (mResourceTypeImpl.ContainsKey(tempImpl.ResourceType))
                    {
                        throw new Exception(String.Format("ResourceType={0}的资源已经存在实现。", tempImpl.ResourceType.ToString()));
                    }

                    mResourceTypeImpl[tempImpl.ResourceType] = tempImpl;
                }
            }
        }

        #endregion

        /// <summary>
        /// 将ResourceTypeSub转化为ResourceType
        /// </summary>
        /// <param name="resourceTypeSub">ResourceTypeSub</param>
        /// <returns>ResourceType</returns>
        public static ResourceTypeEnum GetResourceType(ResourceTypeSubEnum resourceTypeSub)
        {
            Int32 typeID = ((Int32)resourceTypeSub) / 100;

            return (ResourceTypeEnum)typeID;
        }

        /// <summary>
        /// 处理重复的资源
        /// </summary>
        /// <param name="gameResourceList">游戏资源列表</param>
        /// <returns>处理重复后的游戏资源列表</returns>
        public static List<GameResourceObject> HandleDuplicateResource(IEnumerable<GameResourceObject> gameResourceList)
        {
            //判断是否为空列表
            if (gameResourceList == null)
            {
                return null;
            }

            if (!gameResourceList.Any())
            {
                return new List<GameResourceObject>();
            }

            var resultList = new List<GameResourceObject>();
            foreach (var item in gameResourceList)
            {
                var findItem = resultList.FirstOrDefault(t => t.ModelId == item.ModelId);
                if (findItem == null)
                {
                    resultList.Add(item.Clone());
                    continue;
                }

                findItem.Count += item.Count;
            }

            return resultList;
        }

        /// <summary>
        /// 资源相减
        /// </summary>
        /// <param name="motherSet">被减的资源集合</param>
        /// <param name="sonSet">要减去的资源集合</param>
        /// <returns>减去之后的资源集合</returns>
        public static List<GameResourceObject> Difference(IEnumerable<GameResourceObject> motherSet,
            IEnumerable<GameResourceObject> sonSet)
        {
            // 先去重复
            var motherList = HandleDuplicateResource(motherSet);
            var sonList = HandleDuplicateResource(sonSet);

            if (motherList == null && sonList == null)
            {
                return null;
            }

            if (motherList != null && sonList == null)
            {
                return motherList;
            }

            if (motherList == null && sonList != null)
            {
                throw new Exception("要减的资源集合不能为null");
            }

            if (motherList.Count < sonList.Count)
            {
                throw new Exception("要减的资源集合不能为大于被减的资源集合");
            }

            // 查找差值
            var result = new List<GameResourceObject>();
            foreach (var item in motherList)
            {
                var findItem = sonList.FirstOrDefault(t => t.ModelId == item.ModelId);
                if (findItem == null)
                {
                    result.Add(item.Clone());
                    continue;
                }

                if (item.Count > findItem.Count)// 做一个小于兼容
                {
                    result.Add(new GameResourceObject(item.ResourceTypeSub, item.ModelId, item.Count - findItem.Count, false));
                }
            }

            return result;
        }

        /// <summary>
        /// 判断资源是否足够
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="resourceList">游戏资源列表</param>
        /// <returns>资源是否足够</returns>
        public static Boolean CheckIfGameResourceEnough(Player player,
            IEnumerable<GameResourceObject> resourceList)
        {
            foreach (var item in resourceList)
            {
                lock (mClassName)
                {
                    return GetResourceImpl(item.ResourceTypeSub).IsEnough(player, item);
                }
            }

            return true;
        }

        #region 格式转变

        /// <summary>
        /// 检测转换资源是否成功
        /// </summary>
        /// <param name="strGameResource">资源</param>
        /// <param name="ifJudgeEmpty">是否判断空的</param>
        /// <returns>是否转换成功(null：成功，否则为错误信息)</returns>
        public static String CheckConvertToGameResourceObject(String strGameResource, Boolean ifJudgeEmpty = true)
        {
            if (!ifJudgeEmpty && String.IsNullOrEmpty(strGameResource))
            {
                return null;
            }

            try
            {
                ConvertToGameResourceObject(strGameResource);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return null;
        }

        /// <summary>
        /// 将资源配置字符串转化为游戏资源列表(GameResourceTypeSub,ModelID,Count||GameResourceTypeSub,ModelID,Count||GameResourceTypeSub,ModelID,Count)
        /// </summary>
        /// <param name="strGameResource">资源配置字符串</param>
        /// <param name="isLock">是否锁定创建的资源</param>
        /// <returns>游戏资源列表</returns>
        public static List<GameResourceObject> ConvertToGameResourceObject(String strGameResource, Boolean isLock = true)
        {
            // 检查输入字符串
            if (String.IsNullOrEmpty(strGameResource))
            {
                throw new Exception("参数为空");
            }

            // 将字符串切割
            var strArray = StringUtil.Split(strGameResource, new String[] { ",", "||" });

            // 判断数量是否足够
            if (strArray.Length % 3 != 0)
            {
                throw new Exception($"参数{strGameResource}的格式不正确");
            }

            // 处理切割后的字符串
            var gameResourceList = new List<GameResourceObject>(strArray.Length / 3);
            for (Int32 i = 0; i < strArray.Length; i += 3)
            {
                // 取出对应的值
                var resourceTypeSub = 0;
                var modelId = 0;
                var count = 0;
                if (ConvertUtil.TryParseToInt32(strArray[i], out resourceTypeSub) == false
                    || ConvertUtil.TryParseToInt32(strArray[i + 1], out modelId) == false
                    || ConvertUtil.TryParseToInt32(strArray[i + 2], out count) == false
                )
                {
                    throw new Exception($"参数{strGameResource}的格式不正确");
                }

                // 判断数量是否正确
                if (count == 0)
                {
                    throw new Exception($"数量：{count}不正确");
                }

                // 判断模型数据是否存在，此处不用throw，因为内部会进行throw
                if (IfGameResourceModelExists((ResourceTypeSubEnum)resourceTypeSub, modelId) == false)
                    throw new Exception($"资源类型对应的模型不存在,ResourceType={resourceTypeSub.ToString()},ModelId={modelId}");

                // 构造对象，并添加到列表中
                var gameResourceObject = new GameResourceObject((ResourceTypeSubEnum)resourceTypeSub, modelId, count, isLock);
                gameResourceList.Add(gameResourceObject);
            }

            return gameResourceList;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="resourceList">资源集合</param>
        /// <param name="ifTriggerException">是否促发异常</param>
        /// <returns></returns>
        public static String ConvertToString(IEnumerable<GameResourceObject> resourceList, Boolean ifTriggerException = true)
        {
            var sourceString = String.Empty;
            if (resourceList == null || resourceList.Count() <= 0)
            {
                if (ifTriggerException)
                {
                    throw new Exception("参与转换的资源集合不能为null或数量为0。");
                }

                return String.Empty;
            }
            else
            {
                foreach (var item in resourceList)
                {
                    sourceString += $"{(Int32)item.ResourceTypeSub},{item.ModelId},{item.Count}||";
                }
            }

            return sourceString.TrimEnd('|');
        }

        /// <summary>
        /// 判断游戏资源模型是否存在
        /// </summary>
        /// <param name="resourceTypeSub">游戏资源子类型</param>
        /// <param name="modelId">资源模型Id</param>
        /// <returns>是否存在</returns>
        public static Boolean IfGameResourceModelExists(ResourceTypeSubEnum resourceTypeSub, Int32 modelId)
        {
            return GetResourceImpl(resourceTypeSub).IfModelExists(modelId);
        }

        #endregion

        #region 数据处理

        /// <summary>
        /// 处理游戏资源的获取和消耗（同步返回）
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="getGameResourceList">获取的游戏资源列表</param>
        /// <param name="consumeGameResourceList">消耗的游戏资源列表</param>
        /// <param name="subModuleID">游戏子模块枚举</param>
        /// <param name="funcAfterResource">在处理完资源后需要执行的方法</param>
        /// <param name="isCommitSql">是否提交sql</param>
        /// <returns>获得的资源</returns>
        public static List<GameResourceObject> HandleGameResource(Player player, IEnumerable<GameResourceObject> getGameResourceList, IEnumerable<GameResourceObject> consumeGameResourceList,
            Int32 subModuleID, Action funcAfterResource, Boolean isCommitSql = true)
        {
            return Handle(player, getGameResourceList, consumeGameResourceList, subModuleID, false, funcAfterResource, null, isCommitSql);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取实现类
        /// </summary>
        /// <param name="type">资源类型</param>
        /// <returns>实现类</returns>
        private static ResourceBase GetResourceImpl(ResourceTypeSubEnum type)
        {
            lock (mClassName)
            {
                if (!mResourceTypeImpl.ContainsKey(type))
                {
                    throw new Exception($"ResourceTypeSub={(Int32)type}的资源未实现");
                }

                return mResourceTypeImpl[type];
            }
        }

        /// <summary>
        /// 处理资源
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="getGameResourceList">获取资源</param>
        /// <param name="consumeGameResourceList">消耗资源</param>
        /// <param name="subModuleID">模块id</param>
        /// <param name="isAsyn">是否异步</param>
        /// <param name="func1">资源处理完成回掉方法1</param>
        /// <param name="func1">资源处理完成回掉方法2</param>
        /// <param name="func2">是否提交sql</param>
        /// <returns>处理完成获得的资源</returns>
        private static List<GameResourceObject> Handle(Player player, IEnumerable<GameResourceObject> getGameResourceList, IEnumerable<GameResourceObject> consumeGameResourceList,
            Int32 subModuleID, Boolean isAsyn, Action func1, Action<IEnumerable<GameResourceObject>> func2, Boolean isCommitSql)
        {
            var resultList = new List<GameResourceObject>();
            //去除重复的资源
            var newGetGameResourceList = HandleDuplicateResource(getGameResourceList);
            var newConsumeGameResourceList = HandleDuplicateResource(consumeGameResourceList);
            newGetGameResourceList = BeforeHandleResource(player, newGetGameResourceList, subModuleID);

            //判断玩家资源是否足够
            if (newConsumeGameResourceList != null && newConsumeGameResourceList.Count() > 0)
            {
                if (CheckIfGameResourceEnough(player, newConsumeGameResourceList))
                {
                    throw new Exception(String.Format($"玩家资源不足"));
                }
            }

            //记录资源流水日志 --玩家不为null，并且有消耗 or 获得资源才记录资源流水日志
            var conRsString = ConvertToString(newConsumeGameResourceList, false);
            var getRsString = ConvertToString(newGetGameResourceList, false);
            if (player != null && (String.IsNullOrWhiteSpace(conRsString) == false || String.IsNullOrWhiteSpace(getRsString) == false))
            {
                // 可以记录日志
            }

            #region 处理资源匿名函数

            Action tempAction = () =>
            {
                var allHandlerResource = new HashSet<ResourceBase>();
                Object lockObj = new Object();
                lock (lockObj)
                {
                    //处理资源消耗
                    if (newConsumeGameResourceList != null && newConsumeGameResourceList.Count() > 0)
                    {
                        var typeDict = GetTypeDict(newConsumeGameResourceList);
                        foreach (var item in typeDict)
                        {
                            var impl = GetResourceImpl(item.Key);
                            impl.ConsumeResources(player, subModuleID, item.Value, isAsyn);

                            allHandlerResource.Add(impl);
                        }
                    }

                    //处理资源获得
                    if (newGetGameResourceList != null && newGetGameResourceList.Count() > 0)
                    {
                        var typeDict = GetTypeDict(newGetGameResourceList);
                        foreach (var item in typeDict)
                        {
                            var impl = GetResourceImpl(item.Key);
                            resultList.AddRange(impl.GetResources(player, subModuleID, item.Value, isAsyn));
                            allHandlerResource.Add(impl);
                        }
                    }

                    //处理收尾工作
                    foreach (var endHanlder in allHandlerResource)
                    {
                        endHanlder.EndHandle(player, getGameResourceList, consumeGameResourceList);
                    }
                }
            };

            #endregion

            #region 处理逻辑

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead }))
            {
                try
                {
                    tempAction();
                    func1?.Invoke();
                    func2?.Invoke(resultList);

                    //触发资源变动事件
                    if ((newGetGameResourceList != null && newGetGameResourceList.Count() > 0)
                        || (newConsumeGameResourceList != null && newConsumeGameResourceList.Count() > 0))
                    {
                        // 事件促发
                    }

                    //提交player对象的最新信息
                    if (isCommitSql)
                    {
                        // sql提交
                    }

                    //提交事务                                
                    scope.Complete();
                }
                catch (Exception)
                {
                    // sql回滚
                    throw;
                }
            }

            #endregion

            return resultList;
        }

        /// <summary>
        /// 预处理资源
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="list">资源集合</param>
        /// <param name="subModuleID">模块ID</param>
        /// <returns></returns>
        private static List<GameResourceObject> BeforeHandleResource(Player player, IEnumerable<GameResourceObject> list, Int32 subModuleID)
        {
            var result = new List<GameResourceObject>();

            // 处理资源获得
            if (list == null || list.Count() <= 0)
            {
                return result;
            }

            var typeDict = GetTypeDict(list);
            foreach (var item in typeDict)
            {
                var impl = GetResourceImpl(item.Key);
                result.AddRange(impl.GetBeforeHandle(player, subModuleID, item.Value));
            }

            return result;
        }

        /// <summary>
        /// 根据资源类型对资源进行分类
        /// </summary>
        /// <param name="list">资源类型</param>
        /// <returns>分类之后的资源类型</returns>
        private static Dictionary<ResourceTypeSubEnum, List<GameResourceObject>> GetTypeDict(IEnumerable<GameResourceObject> list)
        {
            if (list == null || list.Count() == 0)
            {
                return new Dictionary<ResourceTypeSubEnum, List<GameResourceObject>>();
            }

            /**
             *  注意：
             *      先分组，然后再遍历分组以后的对象，以便准确设定字典和内部集合的数量
             * */
            var groupObj = list.GroupBy(g => g.ResourceTypeSub);
            var typeDict = new Dictionary<ResourceTypeSubEnum, List<GameResourceObject>>(groupObj.Count());
            foreach (var item in groupObj)
            {
                var tempList = new List<GameResourceObject>(item.Count());
                foreach (var innerItem in item)
                {
                    tempList.Add(innerItem);
                }

                typeDict[item.Key] = tempList;
            }

            return typeDict;
        }

        #endregion
    }
}
