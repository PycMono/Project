//***********************************************************************************
// 文件名称：Director.cs
// 功能描述：指挥者类
// 数据表：
// 作者：killer
// 日期：2018/10/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;

namespace DesignPatterns.BuilderPattern
{
    /// <summary>
    /// 指挥者类
    /// </summary>
    internal class Director
    {
        /// <summary>
        /// 构造数据
        /// </summary>
        /// <param name="builder">构造者</param>
        public static void Construct(Builder builder)
        {
            builder.BuildBody();
            builder.BuildFace();
            builder.BuildHead();
        }
    }
}
