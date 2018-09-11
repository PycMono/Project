using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Decorator
{
    public class ConcreteComponent : ComponentBase
    {
        public override void Operation()
        {
            Console.WriteLine("123456");
        }
    }
}
