//***********************************************************************************
// 文件名称：ObserverBase.cs
// 功能描述：观察者父类
// 数据表：无
// 作者：killer
// 日期：2018/06/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Observer
{
    /// <summary>
    /// 观察者父类
    /// </summary>
    public abstract class ObserverBase
    {
        /// <summary>
        /// 更新状态
        /// </summary>
        public abstract void UpdateState();
    }
}
