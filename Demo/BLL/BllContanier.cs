using BLL.Interface;
using MvcSpider.IOC;

namespace BLL
{
    public partial class BllContanier
    {
        static ITestBll testBll;
    }

    public partial class BllContanier : IOCcontainer
    {
        private BllContanier() : base(true, typeof(BllContanier)) { }
        static BllContanier instance;

        public static T Get<T>() where T : class
        {
            if (instance == null)
            {
                instance = new BllContanier();
            }
            return instance.Create<T>();
        }
    }
}
