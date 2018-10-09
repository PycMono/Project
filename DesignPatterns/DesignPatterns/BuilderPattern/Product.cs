//***********************************************************************************
// 文件名称：Product.cs
// 功能描述：产品类
// 数据表：
// 作者：killer
// 日期：2018/10/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;

namespace DesignPatterns.BuilderPattern
{
    /// <summary>
    /// 产品类
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 头部
        /// </summary>
        internal String Head { get; set; }

        /// <summary>
        /// 脸部（脸部依赖于头部）
        /// </summary>
        internal String Face { get; set; }

        /// <summary>
        /// 身体
        /// </summary>
        internal String Body { get; set; }
    }
}
