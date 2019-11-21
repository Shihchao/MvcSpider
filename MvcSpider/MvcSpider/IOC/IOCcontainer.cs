using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvcSpider.IOC
{
    public class IOCcontainer
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="fromStaticProp"> 
        /// 是否从静态变量中寻找并实例化实体(可以减少点实例化操作吧虽然感觉没那么缺性能)
        ///  find object from static prop first: true=yes
        ///  </param>
        /// <param name="extendClass">
        /// 子类
        /// subClass
        /// </param>
        public IOCcontainer(bool fromStaticProp, Type extendClass)
        {
            this.fromStaticProp = fromStaticProp;
            this.extendClass = extendClass;
        }
        public IOCcontainer()
        {
            this.fromStaticProp = false;
            this.extendClass = null;
        }


        bool fromStaticProp;
        Type extendClass;


        public T Create<T>() where T : class
        {
            T instance = fromStaticProp ? CreateInstanceFromStaticProp<T>() : CreateInstance<T>();
            return instance == null ? CreateInstance<T>() : instance;
        }

        /// <summary>
        /// 从静态变量中寻找并实例化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T CreateInstanceFromStaticProp<T>() where T : class
        {
            FieldInfo[] props = extendClass.GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            foreach (FieldInfo p in props)
            {
                foreach (Type ip in p.FieldType.GetInterfaces())
                {
                    if (ip.Name == typeof(T).Name)
                    {
                        if (p.GetValue(p) == null)
                        {
                            MemberInfo info = typeof(T);
                            ImplementAttribute atri = info.GetCustomAttribute(typeof(ImplementAttribute)) as ImplementAttribute;
                            p.SetValue(p, Activator.CreateInstance(atri.ImplementClass));
                        }
                        return p.GetValue(p) as T;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 直接实例化实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T CreateInstance<T>() where T : class
        {
            MemberInfo info = typeof(T);
            ImplementAttribute atri = info.GetCustomAttribute(typeof(ImplementAttribute)) as ImplementAttribute;
            return (T)Activator.CreateInstance(atri.ImplementClass);
        }
    }
}
