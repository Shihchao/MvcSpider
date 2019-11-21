using MvcSpider.EntityFrameworkFExtend;
using System.Data.Entity;

namespace DAL.Base
{
    public class BaseContext : DbContext
    {
        string mappingPath;

        public BaseContext(string configName, string mappingPath) : base(configName)
        {
            this.mappingPath = mappingPath;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelRegister.Regist(modelBuilder, mappingPath);
            base.OnModelCreating(modelBuilder);
        }
    }
}
