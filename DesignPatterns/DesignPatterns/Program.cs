using System;
using System.Collections.Generic;
using System.Reflection;

namespace DesignPatterns
{
    using DesignPatterns.Proxy;
    using DesignPatterns.Factory;
    using DesignPatterns.Decorator;
    using DesignPatterns.Factory.FactoryMethod;

    class Program
    {
        static void Main(string[] args)
        {
            // 装饰
            //var tt = new ConcreteDecoratorAImpl();
            //var ss = new ConcreteComponent();
            //tt.SetComponet(ss);
            //tt.Operation();

            // 代理
            //var proxyImpl = new ProxyImpl("嘿嘿");
            //proxyImpl.SendFlowers();
            //proxyImpl.SendDolls();

            // 简单工厂
            //var calcSubImpl = SimpleFactoryHandle.Create("-");
            //calcSubImpl.NumberB = 10;
            //calcSubImpl.NumberA = 9;
            //Console.WriteLine(calcSubImpl.GetResult());

            // 工厂方法
            //var addFatoryImpl = new AddFatoryImpl();
            //var calcAddImpl = addFatoryImpl.Create();
            //calcAddImpl.NumberB = 10;
            //calcAddImpl.NumberA = 9;
            //Console.WriteLine(calcAddImpl.GetResult());

            Console.ReadKey();
        }
    }
}
