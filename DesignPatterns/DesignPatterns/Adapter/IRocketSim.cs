//***********************************************************************************
// 文件名称：IRocketSim.cs
// 功能描述：火箭仿真功能---接口(接口命名以I开头，呼应interface)
// 数据表：
// 作者：killer
// 日期：2017/01/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
namespace DesignPatterns.Adapter
{
    /// <summary>
    /// 火箭仿真功能---接口(接口命名以I开头，呼应interface)
    /// </summary>
    interface IRocketSim
    {
        /// <summary>
        /// 获取质量
        /// </summary>
        /// <returns></returns>
        Double GetMass();

        /// <summary>
        /// 获取推力（θrʌst）
        /// </summary>
        /// <returns></returns>
        Double GetThrust();

        /// <summary>
        /// 获取时间
        /// </summary>
        /// <returns></returns>
        Double GetSimTime();
    }
}
