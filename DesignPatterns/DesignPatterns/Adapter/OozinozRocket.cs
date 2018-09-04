//***********************************************************************************
// 文件名称：OozinozRocket.cs
// 功能描述：臭氧火箭
// 数据表：
// 作者：killer
// 日期：2017/01/14 17:02:48
// 修改记录：
//***********************************************************************************

using System;
namespace DesignPatterns.Adapter
{
    /**
    *该类的需求
    *  需要将PhysicalRocket类放到仿真功能中， 该类提供的方法类似于仿真器需要的功能行为，此时可以
    *  运用适配器功能，创建PhysicalRocket的子类，并且实现IRocketSim接口
    *  PhysicalRocket拥有仿真器需要的信息，但是他的方法不完全匹配IRocketSim接口声明的方法
    *  ，主要的差异是保留了一个内部时钟,不会不时调用setSimTime方法
    *  更新仿真对方，若要适配PhysicalRocket想满足当前仿真器的需求OozinozRocket对象可以维持一个实例变量，用来传递PhysicalRocket
    *  类需要的方法
    *
    *  重点：你是无权限修改IRocketSim接口PhysicalRocket类的
    *
    */

    /// <summary>
    /// 臭氧火箭
    /// </summary>
    class OozinozRocket : PhysicalRocket, IRocketSim
    {
        public Double GetMass()
        {
            return GetMass();
        }
    }
}
