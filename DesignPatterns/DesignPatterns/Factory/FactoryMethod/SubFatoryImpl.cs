//***********************************************************************************
// 文件名称：SubFatoryImpl.cs
// 功能描述：减法工厂
// 数据表：
// 作者：killer
// 日期：2017/01/14 17:02:48
// 修改记录：
//***********************************************************************************

namespace DesignPatterns.Factory.FactoryMethod
{
    using DesignPatterns.Factory.Impl;

    /// <summary>
    /// 减法工厂
    /// </summary>
    class SubFatoryImpl : IFactory
    {
        /// <summary>
        /// 减法工厂
        /// </summary>
        /// <returns></returns>
        public CalcBase Create()
        {
            return new CalcSubImpl();
        }
    }
}
