using Model;
using System.Data.Entity.ModelConfiguration;

namespace DAL.Base
{
    abstract class BaseDomainMapping<T> : EntityTypeConfiguration<T>
        where T : BaseModel, new()
    {
        public BaseDomainMapping()
        {
            Init();
        }
        public virtual void Init() { }
    }
}
