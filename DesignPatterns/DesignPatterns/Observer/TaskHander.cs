//***********************************************************************************
// 文件名称：TaskHander.cs
// 功能描述：任务处理类
// 数据表：无
// 作者：killer
// 日期：2018/06/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;

namespace DesignPatterns.Observer
{
    /// <summary>
    /// 任务处理类
    /// </summary>
    public class TaskHander
    {
        /// <summary>
        /// 任务处理方法
        /// </summary>
        public delegate void TaskHandleFunc();

        /// <summary>
        /// 处理方法集合
        /// </summary>
        private static List<TaskHandleFunc> TaskHandleFuncList = new List<TaskHandleFunc>();

        /// <summary>
        /// 锁定对象
        /// </summary>
        private static Object LockObj = new Object();

        /// <summary>
        /// 注册处理方法
        /// </summary>
        /// <param name="func">方法</param>
        public static void RegisterHandleFunc(TaskHandleFunc func)
        {
            lock (LockObj)
            {
                TaskHandleFuncList.Add(func);
            }
        }

        /// <summary>
        /// 触发方法
        /// </summary>
        private static void TriggerFunc()
        {
            lock (LockObj)
            {
                // 遍历执行方法
                foreach (var taskHandleFunc in TaskHandleFuncList)
                {
                    taskHandleFunc();
                }
            }
        }
    }
}
