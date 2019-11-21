using DAL.Base;
using System.Data.Entity;

namespace DAL.Context
{
    public partial class TestContext
    {
        private static string configName = "TestContext";
        private static string mappingsPath = "DAL.DomainMappings";
    }

    public partial class TestContext : BaseContext
    {
        //static TestContext()
        //{
        //    Database.SetInitializer<TestContext>(null);
        //}
        public TestContext() : base(configName, mappingsPath) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
