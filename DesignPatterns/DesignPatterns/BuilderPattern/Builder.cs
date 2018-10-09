//***********************************************************************************
// 文件名称：Builder.cs
// 功能描述：构建者抽象父类
// 数据表：
// 作者：killer
// 日期：2018/10/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;

namespace DesignPatterns.BuilderPattern
{
    /// <summary>
    /// 构建者抽象父类
    /// </summary>
    public abstract class Builder
    {
        /// <summary>
        /// 产品对象
        /// </summary>
        internal Product product = new Product();

        /// <summary>
        /// 构造头部
        /// </summary>
        internal abstract void BuildHead();

        /// <summary>
        /// 构造脸部
        /// </summary>
        internal abstract void BuildFace();

        /// <summary>
        /// 构造身体
        /// </summary>
        internal abstract void BuildBody();

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        internal abstract Product GetResult();
    }
}
