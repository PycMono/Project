//***********************************************************************************
// 文件名称：IFactory.cs
// 功能描述：工厂接口
// 数据表：
// 作者：killer
// 日期：2017/01/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factory.FactoryMethod
{
    /**
     *这里对简单工厂进行了优化，简单工厂，在创建对象的时候，会进行很多switch或者if语句的判断，违背了开放-封闭原则
     *改成工厂方法了之后，对扩展更友好，耦合性没那么大
     *
     */

    /// <summary>
    /// 工厂接口
    /// </summary>
    internal interface IFactory
    {
        /// <summary>
        /// 创建工厂类方法
        /// </summary>
        /// <returns></returns>
        CalcBase Create();
    }
}
