//***********************************************************************************
// 文件名称：CalcBase.cs
// 功能描述：计算父类
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

namespace DesignPatterns.Factory
{
    /// <summary>
    /// 计算父类
    /// </summary>
    internal class CalcBase
    {
        /// <summary>
        /// 数值A
        /// </summary>
        public Double NumberA { get; set; }

        /// <summary>
        /// 数值B
        /// </summary>
        public Double NumberB { get; set; }

        /// <summary>
        /// 获取计算结果
        /// </summary>
        /// <returns></returns>
        public virtual Double GetResult()
        {
            return 0;
        }
    }
}
