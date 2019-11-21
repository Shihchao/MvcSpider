using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace MvcSpider.EntityFrameworkFExtend
{
    public static class ModelRegister
    {
        public static void Regist(DbModelBuilder modelBuilder, string nameSpace)
        {
            var typesToRegister = Assembly.GetCallingAssembly().GetTypes()
           .Where(type => !String.IsNullOrEmpty(type.Namespace) && type.Namespace.StartsWith(nameSpace))
           .Where(type => GenericTypeEqual(type, (typeof(EntityTypeConfiguration<>))));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }

        /// <summary>
        /// 递归调用 判断类或其基类是否为指定泛型类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool GenericTypeEqual(Type type, Type equalType)
        {
            if (type == null || equalType == null)
                return false;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == equalType)
                return true;
            return GenericTypeEqual(type.BaseType, equalType);
        }
    }
}
