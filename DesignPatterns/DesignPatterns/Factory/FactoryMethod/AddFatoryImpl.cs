//***********************************************************************************
// 文件名称：AddFatoryImpl.cs
// 功能描述：加法工厂
// 数据表：
// 作者：killer
// 日期：2017/01/14 17:02:48
// 修改记录：
//***********************************************************************************

namespace DesignPatterns.Factory.FactoryMethod
{
    using DesignPatterns.Factory.Impl;

    class AddFatoryImpl : IFactory
    {
        public CalcBase Create()
        {
            return new CalcAddImpl();
        }
    }
}
