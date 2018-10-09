//***********************************************************************************
// 文件名称：ConcreteBuilderImpl.cs
// 功能描述：实现类（构造者类）
// 数据表：
// 作者：killer
// 日期：2018/10/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;

namespace DesignPatterns.BuilderPattern
{
    /// <summary>
    /// 实现类（构造者类）
    /// </summary>
    internal class ConcreteBuilderImpl : Builder
    {
        internal override void BuildBody()
        {
            product.Body = "胖身体";
        }

        internal override void BuildFace()
        {
            product.Body = "小脸";
        }

        internal override void BuildHead()
        {
            product.Body = "方头";
        }

        internal override Product GetResult()
        {
            return product;
        }

        /// <summary>
        /// 获取身体对象
        /// </summary>
        /// <returns>身体对象</returns>
        internal String GetBody()
        {
            return product.Body;
        }

        /// <summary>
        /// 获取脸部对象
        /// </summary>
        /// <returns>脸部对象</returns>
        internal String GetFace()
        {
            return product.Face;
        }

        /// <summary>
        /// 获取头部信息
        /// </summary>
        /// <returns>头部信息</returns>
        internal String GetHead()
        {
            return product.Face;
        }
    }
}
