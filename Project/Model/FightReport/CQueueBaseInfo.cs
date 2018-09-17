//***********************************************************************************
// 文件名称：CQueueBaseInfo.cs
// 功能描述：客服端队列基本数据（战报）
// 数据表：无
// 作者：pyc
// 日期：2017/12/8 11:16:37
// 修改记录：
//***********************************************************************************
using System;
using System.Collections.Generic;

namespace PycMono.Project.Model
{
    /// <summary>
    /// 队列信息
    /// </summary>
    public class CQueueBaseInfo
    {
        /// <summary>
        /// 队列ID
        /// </summary>
        public Int32 QueueID { get; set; }

        /// <summary>
        /// 当前队列的武将信息
        /// </summary>
        public List<CHeroBaseInfo> CHeroBaseInfoList { get; set; }
    }
}
