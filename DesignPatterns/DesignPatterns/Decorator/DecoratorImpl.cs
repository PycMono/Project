//***********************************************************************************
// 文件名称：DecoratorImpl.cs
// 功能描述：装饰者实现类
// 数据表：无
// 作者：killer
// 日期：2018/06/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
using DesignPatterns.Decorator;

namespace DesignPatterns.Decorator
{
    /// <summary>
    /// 装饰者实现类
    /// </summary>
    public abstract class DecoratorImpl : ComponentBase
    {
        /// <summary>
        /// 类容装饰父类对象
        /// </summary>
        protected ComponentBase ComponetObj;

        /// <summary>
        /// 设置装饰对象
        /// </summary>
        /// <param name="componentBase">装饰对象</param>
        public void SetComponet(ComponentBase componentBase)
        {
            this.ComponetObj = componentBase;
        }

        /// <summary>
        /// 重载方法
        /// </summary>
        public override void Operation()
        {
            this.ComponetObj?.Operation();
        }
    }
}
