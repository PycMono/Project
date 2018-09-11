//***********************************************************************************
// 文件名称：FactoryHandle.cs
// 功能描述：工厂处理类（简单工厂）
// 数据表：
// 作者：killer
// 日期：2017/01/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
namespace DesignPatterns.Factory
{
    using DesignPatterns.Factory.Impl;// 内部自己写的命名空间写在命名空间内部和框架自带的保持区别

    /// <summary>
    /// 工厂处理类（简单工厂）
    /// </summary>
    class SimpleFactoryHandle
    {
        /// <summary>
        /// 创建工厂
        /// </summary>
        /// <param name="operateStr">操作字符串</param>
        /// <returns>具体的操作</returns>
        public static CalcBase Create(String operateStr)
        {
            /**
             * 简单工厂需要用switch或者if语句进行判断，每次增加一个实现类，需要改工厂方法，耦合度非常高，违背了
             * 对修改关闭、对扩展开放原则
             * 改进版本1：在FactoryMethod文件下，采用依赖接口模型，提取出他们共同的特征，移除掉switch语句
             */
            switch (operateStr)
            {
                case "+":
                    return new CalcAddImpl();
                case "-":
                    return new CalcSubImpl();
                default:
                    Console.WriteLine($"为实现指定的计算操作{operateStr}");
                    return null;// 外部需要做判断
            }
        }
    }
}
