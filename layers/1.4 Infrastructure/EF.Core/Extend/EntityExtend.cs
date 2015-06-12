using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using EntityFramework.Extensions;
using EntityFramework.Caching;

namespace EF.Core.LinqExtend
{
    public static class EntityExtend
    {
        #region Private expression tree helpers
        private static LambdaExpression GenerateSelector<TEntity>(String propertyName, out Type resultType) where TEntity : class
        {
            PropertyInfo property;
            Expression propertyAccess;
            var parameter = Expression.Parameter(typeof(TEntity), "Entity");

            if (propertyName.Contains('.'))
            {
                String[] childProperties = propertyName.Split('.');
                property = typeof(TEntity).GetProperty(childProperties[0]);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i]);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = typeof(TEntity).GetProperty(propertyName);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }

            resultType = property.PropertyType;

            return Expression.Lambda(propertyAccess, parameter);
        }
        private static MethodCallExpression GenerateMethodCall<TEntity>(IQueryable<TEntity> source, string methodName, String fieldName) where TEntity : class
        {
            Type type = typeof(TEntity);
            Type selectorResultType;
            LambdaExpression selector = GenerateSelector<TEntity>(fieldName, out selectorResultType);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
            new Type[] { type, selectorResultType },
            source.Expression, Expression.Quote(selector));
            return resultExp;
        }

        public static MethodCallExpression GenerateWhereMethodCall<TEntity>(IQueryable<TEntity> source, string methodName, String fieldName, object Value) where TEntity : class
        {
            IQueryable first = source;
            Type type = typeof(TEntity);
            PropertyInfo property = type.GetProperty(fieldName);
            ParameterExpression param = Expression.Parameter(type, "c");
            Expression right = Expression.Constant(Value);
            Expression left = Expression.Property(param, property);
            Expression filter = Expression.Call
                       (
                         left,  //c.DataSourceName
                          Value.GetType().GetMethod(methodName, new Type[] { Value.GetType() }),// 反射使用.Contains()方法                         
                          right
                       );

            Expression pred = Expression.Lambda(filter, param);
            MethodCallExpression call = Expression.Call(typeof(Queryable), "Where", new Type[] { type },
                Expression.Constant(first), pred);
            return call;
        }
        #endregion

        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string fieldName) where TEntity : class
        {
            MethodCallExpression resultExp = GenerateMethodCall<TEntity>(source, "OrderBy", fieldName);
            return source.Provider.CreateQuery<TEntity>(resultExp) as IOrderedQueryable<TEntity>;
        }

        public static IOrderedQueryable<TEntity> OrderByDescending<TEntity>(this IQueryable<TEntity> source, string fieldName) where TEntity : class
        {
            MethodCallExpression resultExp = GenerateMethodCall<TEntity>(source, "OrderByDescending", fieldName);
            return source.Provider.CreateQuery<TEntity>(resultExp) as IOrderedQueryable<TEntity>;
        }

        public static IOrderedQueryable<TEntity> ThenBy<TEntity>(this IOrderedQueryable<TEntity> source, string fieldName) where TEntity : class
        {
            MethodCallExpression resultExp = GenerateMethodCall<TEntity>(source, "ThenBy", fieldName);
            return source.Provider.CreateQuery<TEntity>(resultExp) as IOrderedQueryable<TEntity>;
        }

        public static IOrderedQueryable<TEntity> ThenByDescending<TEntity>(this IOrderedQueryable<TEntity> source, string fieldName) where TEntity : class
        {
            MethodCallExpression resultExp = GenerateMethodCall<TEntity>(source, "ThenByDescending", fieldName);
            return source.Provider.CreateQuery<TEntity>(resultExp) as IOrderedQueryable<TEntity>;
        }
        public static IOrderedQueryable<TEntity> OrderUsingSortExpression<TEntity>(this IQueryable<TEntity> source, string sortExpression) where TEntity : class
        {
            String[] orderFields = sortExpression.Split(',');
            IOrderedQueryable<TEntity> result = null;
            for (int currentFieldIndex = 0; currentFieldIndex < orderFields.Length; currentFieldIndex++)
            {
                String[] expressionPart = orderFields[currentFieldIndex].Trim().Split(' ');
                String sortField = expressionPart[0];
                Boolean sortDescending = (expressionPart.Length == 2) && (expressionPart[1].Equals("DESC", StringComparison.OrdinalIgnoreCase));
                if (sortDescending)
                {
                    result = currentFieldIndex == 0 ? source.OrderByDescending(sortField) : result.ThenByDescending(sortField);
                }
                else
                {
                    result = currentFieldIndex == 0 ? source.OrderBy(sortField) : result.ThenBy(sortField);
                }
            }
            return result;
        }

        public static IQueryable<TEntity> Query<TEntity>(this IQueryable<TEntity> source, string methodName, string fieldName, object value) where TEntity : class
        {
            MethodCallExpression exp = GenerateWhereMethodCall(source, methodName, fieldName, value);
            return source.Provider.CreateQuery<TEntity>(exp) as IQueryable<TEntity>;
        }

        public static IQueryable<TEntity> OrQuery<TEntity>(this IQueryable<TEntity> source, string methodName, string fieldName, object Value) where TEntity : class
        {
            IQueryable first = source;
            Type type = typeof(TEntity);
            PropertyInfo property = type.GetProperty(fieldName);
            ParameterExpression param = Expression.Parameter(type, "c");
            Expression right = Expression.Constant(Value);
            Expression left = Expression.Property(param, property);
            Expression filter = Expression.Call
                       (
                          Expression.Property(left, property),  //c.DataSourceName
                          Value.GetType().GetMethod(methodName, new Type[] { Value.GetType() }),// 反射使用.Contains()方法                         
                          right
                       );

            Expression pred = Expression.Lambda(filter, param);
            MethodCallExpression call = Expression.Call(typeof(Queryable), "Where", new Type[] { type },
                Expression.Constant(first), pred);
            return source.Provider.CreateQuery<TEntity>(call) as IQueryable<TEntity>;
        }

        public static List<string> Merge(this List<string> str, List<string> newStr)
        {
            foreach (string s in newStr)
            {
                str.Add(s);
            }
            return str;
        }

        public static string JoinToString(this IEnumerable<string> value, char splitor)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in value)
            {
                sb.Append(str + splitor);
            }
            return sb.ToString().TrimEnd(splitor);
        }

        /// <summary>
        /// 转换为一个DataTable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TResult>(this IEnumerable<TResult> value) where TResult : class
        {
            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(TResult);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType.BaseType); });
            foreach (var item in value)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }

        public delegate object[] CreateRowDelegate<T>(T t);


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <param name="fn"></param>
        /// <example>
        /// /*
        /// * sample:
        /// * var query = from ....;
        /// * DataTable dt = query.ToDataTable(rec => new object[] { query });
        /// *
        ///*/
        /// 
        /// </example>
        /// <returns></returns>
        static public DataTable ToDataTable<T>(this IEnumerable<T> varlist, CreateRowDelegate<T> fn)
        {

            DataTable dtReturn = new DataTable();

            // column names

            PropertyInfo[] oProps = null;

            // Could add a check to verify that there is an element 0

            foreach (T rec in varlist)
            {

                // Use reflection to get property names, to create table, Only first time, others will follow

                if (oProps == null)
                {

                    oProps = ((Type)rec.GetType()).GetProperties();

                    foreach (PropertyInfo pi in oProps)
                    {

                        Type colType = pi.PropertyType; if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {

                            colType = colType.GetGenericArguments()[0];

                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));

                    }

                }

                DataRow dr = dtReturn.NewRow(); foreach (PropertyInfo pi in oProps)
                {

                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);

                }

                dtReturn.Rows.Add(dr);

            }

            return (dtReturn);

        }




        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 扩展的IF
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="predicate"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <example>
        /// People people2 = new People { Name = "ldp615", IsHungry = true, IsThirsty = true, IsTired = true }
        ///        .If(p => p.IsHungry, p => p.Eat())
        ///        .If(p => p.IsThirsty, p => p.Drink())
        ///        .If(p => p.IsTired, p => p.Rest());
        ///    people2.Work();
        /// </example>
        public static T If<T>(this T t, Predicate<T> predicate, Action<T> action) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            if (predicate(t)) action(t);
            return t;
        }

        /// <summary>
        /// Switch扩展方法
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="inputSource"></param>
        /// <param name="outputSource"></param>
        /// <param name="defaultOutput"></param>
        /// <example>
        /// string englishName = "apple";
        /// string chineseName = englishName.Switch(
        ///        new string[] { "apple", "orange", "banana", "pear" },
        ///        new string[] { "苹果", "桔子", "香蕉", "梨" },
        ///        "未知"
        ///        );
        /// </example>
        /// <returns></returns>
        public static TOutput Switch<TOutput, TInput>(this TInput input, IEnumerable<TInput> inputSource, IEnumerable<TOutput> outputSource, TOutput defaultOutput)
        {
            IEnumerator<TInput> inputIterator = inputSource.GetEnumerator();
            IEnumerator<TOutput> outputIterator = outputSource.GetEnumerator();

            TOutput result = defaultOutput;
            while (inputIterator.MoveNext())
            {
                if (outputIterator.MoveNext())
                {
                    if (input.Equals(inputIterator.Current))
                    {
                        result = outputIterator.Current;
                        break;
                    }
                }
                else break;
            }
            return result;
        }

        public static TEntity CacheFirstOrDefault<TEntity>(this IQueryable<TEntity> query, long Seccond=1) where TEntity : class
        {
            return query.FromCacheFirstOrDefault(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(Seccond)));
        }
        public static IEnumerable<TEntity> Cache<TEntity>(this IQueryable<TEntity> query, long Seccond = 1) where TEntity : class
        {
            return query.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(Seccond)));
        }
    }

    #region Distinct扩展方法
    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
    {
        private Func<T, V> keySelector;
        private IEqualityComparer<V> comparer;

        public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public CommonEqualityComparer(Func<T, V> keySelector)
            : this(keySelector, EqualityComparer<V>.Default)
        { }

        public bool Equals(T x, T y)
        {
            return comparer.Equals(keySelector(x), keySelector(y));
        }

        public int GetHashCode(T obj)
        {
            return comparer.GetHashCode(keySelector(obj));
        }
    }

    public static class DistinctExtensions
    {
        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }

        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector, comparer));
        }
    }
    #endregion
}
