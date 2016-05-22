using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using z99.Database.Models;

namespace z01_LINQ
{
    public class ExpressionTree : IDisposable
    {
        private Northwind _db = new Northwind();

        /// <summary>
        /// 參考https://dotblogs.com.tw/jamis/2015/12/28/155438
        /// 使用Expression Tree產生Lambda
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <param name="data3"></param>
        public void ExpressionTreeQuery(
            out IQueryable<Products> data1,
            out IQueryable<Products> data2,
            out IQueryable<Products> data3)
        {
            //一般查詢
            data1 = _db.Products.Where(x => x.ProductID == 10 || x.UnitPrice > 120);

            //IQueryable<TSource> Where<TSource>(
            //      this IQueryable<TSource> source, 
            //      Expression<Func<TSource, bool>> predicate);
            Expression<Func<Products, bool>> iExpression = x => x.ProductID == 10 || x.UnitPrice > 120;
            data2 = _db.Products.Where(iExpression);

            int value = 10;
            string propertyName = "ProductID";
            //x.ProductID == 10
            ParameterExpression iParameterExpression = Expression.Parameter(typeof(Products), "x");
            MemberExpression iLeft = Expression.Property(iParameterExpression, propertyName);
            ConstantExpression iRight = Expression.Constant(value, typeof(int));
            BinaryExpression iEqual1 = Expression.Equal(iLeft, iRight);

            decimal? unitPrice = 120m;
            propertyName = "UnitPrice";
            //x.UnitPrice > 120
            iLeft = Expression.Property(iParameterExpression, propertyName);
            iRight = Expression.Constant(unitPrice, typeof(decimal?));
            BinaryExpression iEqual2 = Expression.GreaterThan(iLeft, iRight);

            //{0} || {1}
            BinaryExpression iBinaryExpression = Expression.Or(iEqual1, iEqual2);

            //Expression<TDelegate>
            var predicate = Expression.Lambda<Func<Products, bool>>(
                                iBinaryExpression,
                                iParameterExpression);
            data3 = _db.Products.Where(predicate);
        }


        //參考https://dotblogs.com.tw/yc421206/archive/2012/11/14/83217.aspx
        //[C#.NET] 利用 Expression Tree 提昇反射效率
        private static string ASSEMBLY_NAME = "z01_LINQ";
        private static string CLASS_NAME = "z01_LINQ.Infrastructure.MyCalculator";
        private static string METHOD_NAME = "Sum";
        private int _iteration = 1000000;

        public void ExpressionTreeReflection(
            out long? data1,
            out long? data2,
            out long? data3)
        {
            data1 = GetReflectionSum();
            data2 = GetDynamicSum();
            data3 = GetExpressionSum();
        }
        private long? GetReflectionSum()
        {
            //載入組件
            var assembly = Assembly.Load(ASSEMBLY_NAME);

            //取得組件類別
            var assemblyType = assembly.GetType(CLASS_NAME);
            if (assemblyType == null) return null;

            //建立執行個體
            var instance = Activator.CreateInstance(assemblyType);
            if (instance == null) return null;

            var methodInfo = assemblyType.GetMethod(METHOD_NAME);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < this._iteration; i++)
            {
                //建立參數
                object[] para = new object[] {1,2};

                //調用方法
                var result = (int)methodInfo.Invoke(instance, para);
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private long? GetDynamicSum()
        {
            //載入組件
            var assembly = Assembly.Load(ASSEMBLY_NAME);

            //建立執行個體
            dynamic instance = assembly.CreateInstance(CLASS_NAME);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < this._iteration; i++)
            {
                var result = instance.Sum(1,2);
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private long? GetExpressionSum()
        {
            //載入組件
            var assembly = Assembly.Load(ASSEMBLY_NAME);

            //取得組件類別
            var assemblyType = assembly.GetType(CLASS_NAME);
            if (assemblyType == null) return null;

            //建立執行個體
            var instance = Activator.CreateInstance(assemblyType);
            if (instance == null)
                return null;

            //取得方法
            var methodInfo = assemblyType.GetMethod(METHOD_NAME);

            //建立參數
            ParameterExpression param1 = Expression.Parameter(typeof(int), "a");
            ParameterExpression param2 = Expression.Parameter(typeof(int), "b");

            //public static MethodCallExpression Call(
            //    Expression instance, 
            //    MethodInfo method, 
            //    params Expression[] arguments);
            MethodCallExpression call = Expression.Call(
                Expression.Constant(instance), 
                methodInfo, 
                new ParameterExpression[] { param1, param2 });

            var action = Expression.Lambda<Func<int, int, int>>(call, param1, param2).Compile();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < this._iteration; i++)
            {
                //調用方法
                var result = action(1,2);
            }

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }















        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    _db.Dispose();
                }
                disposed = true;
            }
        }
    }
}
