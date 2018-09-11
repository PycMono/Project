//***********************************************************************************
// 文件名称：CalcAddImpl.cs
// 功能描述：加法实现类
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

namespace DesignPatterns.Factory.Impl
{
    /// <summary>
    /// 加法实现类
    /// </summary>
    internal class CalcAddImpl : CalcBase
    {
        public override double GetResult()
        {
            return NumberA + NumberB;
        }
    }
}
