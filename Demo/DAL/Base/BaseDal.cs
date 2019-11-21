using DAL.Context;
using Model;
using MvcSpider.ExpressionExtend;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Base
{
    public class BaseDal : IDisposable
    {
        protected TestContext ctx;

        public BaseDal()
        {
            ctx = new TestContext();
        }

        /// <summary>
        /// 通用查询方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ps"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(IEnumerable<QueryParam> ps) where T : BaseModel
        {
            var data = ctx.Set<T>().Where(ps).ToList();
            return data;
        }

        /// <summary>
        /// 通用添加方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Add<T>(T model) where T : BaseModel
        {
            ctx.Set<T>().Add(model);
        }

        /// <summary>
        /// 通用删除方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public void Del<T>(string pkid) where T : BaseModel
        {
            ctx.Set<T>().Remove(this.Find<T>(pkid));
        }

        /// <summary>
        /// 通用范围删除方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public void DelRange<T>(IEnumerable<string> pkids) where T : BaseModel
        {
            foreach (var pkid in pkids)
            {
                ctx.Set<T>().Remove(this.Find<T>(pkid));
            }
        }

        /// <summary>
        /// 通用查找方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public T Find<T>(string pkid) where T : BaseModel
        {
            return ctx.Set<T>().Find(pkid);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public int SaveChanges()
        {
            return ctx.SaveChanges();
        }

        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}
