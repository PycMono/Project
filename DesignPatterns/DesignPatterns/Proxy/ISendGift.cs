//***********************************************************************************
// 文件名称：ISendGift.cs
// 功能描述：送礼父类接口
// 数据表：无
// 作者：killer
// 日期：2018/06/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
namespace DesignPatterns.Proxy
{
    /// <summary>
    /// 送礼父类接口
    /// </summary>
    public interface ISendGift
    {
        /// <summary>
        /// 送玩偶
        /// </summary>
        void SendDolls();

        /// <summary>
        /// 送鲜花
        /// </summary>
        void SendFlowers();
    }
}
