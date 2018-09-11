using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Observer;

namespace DesignPatterns.Decorator
{
    /// <summary>
    /// 
    /// </summary>
    public class ConcreteDecoratorAImpl : DecoratorImpl
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("装饰对象操作");
        }

        private void AddedBehavior()
        {
            Console.WriteLine("dsafdafdsads");
        }
    }
}
