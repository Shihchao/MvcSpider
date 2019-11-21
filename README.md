简介
==

MvcSpider是我个人整合的一个框架，包含一个IOC容器、一个EntityFramework自注册模型的扩展以及一个LinqExpression的扩展。<br>
使用MvcSpider可以快速开发一个基于AspMvc的CRUD项目，使用者不需要再考虑项目间的依赖、实体的注册甚至多参数的查询，这些都集成在了框架之中，只要写几个基类就可以减少大量的代码工作。<br>


使用
==
---
IocContainer
---

    命名空间：MvcSpider.IOC
    变量：fromStaticProp(是否从静态变量中寻找并实例化实体，可以减少点实例化操作吧虽然感觉没那么缺性能)
          extendClass(子类)
          
例子：

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
    
或

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
    
---
EntityFrameworkFExtend
---
    命名空间：MvcSpider.EntityFrameworkFExtend

用于EF框架的模型自注册, 当前使用版本6.2.0, 使用时候需对实体创建继承于·EntityTypeConfiguration<T>·的Mapping类。
  
Mapping类：

···C#

    class TEST_MODEL_MAPPING : EntityTypeConfiguration<TEST_MODEL>
    {
        public override void Init()
        {
            base.Init();
        }
    }
      
···

使用(在DbContext中)：

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelRegister.Regist(modelBuilder, mappingPath);
            base.OnModelCreating(modelBuilder);
        }


---
ExpressionExtend
---
    命名空间：MvcSpider.ExpressionExtend
    
用于对多参数的查询自动创建·Expression·表达式树

使用：

        public IEnumerable<T> Query<T>(IEnumerable<QueryParam> ps) where T : class, new()
        {
            var data = ctx.Set<T>().Where(ps).ToList();
            return data;
        }
