//***********************************************************************************
// 文件名称：ComponentBase.cs
// 功能描述：类容装饰父类
// 数据表：无
// 作者：killer
// 日期：2018/06/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
namespace DesignPatterns.Decorator
{
    /// <summary>
    /// 类容装饰父类
    /// </summary>
    public abstract class ComponentBase
    {
        /// <summary>
        /// 具体操作
        /// </summary>
        public abstract void Operation();
    }
}
