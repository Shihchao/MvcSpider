using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcSpider.IOC
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ImplementAttribute : Attribute
    {
        public ImplementAttribute(Type implementClass)
        {
            this.ImplementClass = implementClass;
        }

        public Type ImplementClass { get; }
    }
}
