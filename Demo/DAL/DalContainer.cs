using MvcSpider.IOC;

namespace DAL
{
    public class DalContainer : IOCcontainer
    {
        private DalContainer() : base() { }
        static DalContainer instance;

        public static T Get<T>() where T : class
        {
            if (instance == null)
            {
                instance = new DalContainer();
            }
            return instance.Create<T>();
        }
    }
}
