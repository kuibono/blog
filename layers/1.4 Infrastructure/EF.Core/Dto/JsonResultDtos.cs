using EF.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Dto
{
    public class JsonMessage
    {
        /// <summary>
        /// application/json
        /// </summary>
        public const string ContentType = "application/json";

        /// <summary>
        /// text/plain
        /// </summary>
        public const string PlainContentType = "text/plain";

        /// <summary>
        /// text/html
        /// </summary>
        public const string HtmlContentType = "text/html";

        /// <summary>
        /// 默认返回true,登录超时返回false
        /// </summary>
        public bool IsLogined { get; set; }

        public string LoginUrl { get; set; }

        /// <summary>
        /// 默认返回false, 无权限时返回true
        /// </summary>
        public bool NoAuthorize { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public JsonMessage()
        {
            IsLogined = true;
        }

        public JsonMessage(bool sucess, string message = null)
            : this()
        {
            this.Success = sucess;
            this.Message = message;
        }

        public JsonMessage AsSuccess(string message = "")
        {
            this.Success = true;
            this.Message = message;
            return this;
        }

        public JsonMessage AsFail(string message = "")
        {
            this.Success = false;
            this.Message = message;
            return this;
        }
    }

    public class JsonDataMessage : JsonMessage
    {
        public JsonDataMessage()
            :base()
        {
        }

        public JsonDataMessage(bool sucess, string message = null)
            :base(sucess, message)
        {
        }

        public object Data { get; set; }

        public long Total { get; set; }
    }

    public class JsonTextValue
    {
        public JsonTextValue()
        {
        }

        public JsonTextValue(INamedEntity entity)
            : this(entity.Id.ToString(), entity.Name)
        {
        }

        public JsonTextValue(string value, string text)
        {
            this.value = value;
            this.text = text;
        }

        public static JsonTextValue FromEntity<TEntity>
            (TEntity entity, Func<TEntity, string> textFunc)
            where TEntity : IEntity
        {
            return new JsonTextValue(entity.Id.ToString(), textFunc(entity));
        }

        public string text { get; set; }

        public string value { get; set; }

        public bool @selected { get; set; }
    }

    public static class JsonTextValueArrayExtentions
    {
        public static void SetSelect(this IEnumerable<JsonTextValue> jsonArray, params string[] vals)
        {
            foreach (var json in jsonArray)
            {
                if (vals.Contains(json.value)) { json.selected = true; }
            }
        }
    }

    public class JsonTree
    {
        public object id { get; set; }

        public string text { get; set; }

        public int count { get; set; }

        public bool issystem { get; set; }

        public string remark { get; set; }

        public bool @checked { get; set; }

        public long parentid { get; set; }

        public object attributes { get; set; }

        public JsonTree[] children { get; set; }

        /// <summary>
        /// 转换为JsonTreeModel数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">数据</param>
        /// <param name="setPropertyAction">自定义属性赋值</param>
        /// <param name="filterPredicate">条件</param>
        /// <returns></returns>
        public static JsonTree[] Convert<T>(IEnumerable<T> data,
            Action<T, JsonTree> setPropertyAction = null,
            Func<T, bool> filterPredicate = null)
            where T : ITreeEntity<T>
        {
            var list = new List<JsonTree>();
            if (filterPredicate != null)
            {
                data = data.Where(filterPredicate);
            }
            foreach (T d in data)
            {
                var j = new JsonTree();
                if (setPropertyAction != null)
                {
                    setPropertyAction(d, j);
                }
                j.children = (d.Children != null && d.Children.Count > 0) ? Convert(d.Children, setPropertyAction, filterPredicate) : null;
                list.Add(j);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 转换为JsonTreeModel数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">数据</param>
        /// <param name="setPropertyAction">自定义属性赋值</param>
        /// <param name="filterPredicate">条件</param>
        /// <returns></returns>
        public static TR[] Convert<T, TR>(IEnumerable<T> data,
            Action<T, TR> setPropertyAction = null,
            Func<T, bool> filterPredicate = null)
            where T : ITreeEntity<T>
            where TR : JsonTree, new()
        {
            var list = new List<TR>();
            if (filterPredicate != null)
            {
                data = data.Where(filterPredicate);
            }
            foreach (T d in data)
            {
                var j = new TR();
                if (setPropertyAction != null)
                {
                    setPropertyAction(d, j);
                }
                j.children = (d.Children != null && d.Children.Count > 0) ? Convert(d.Children, setPropertyAction, filterPredicate) : null;
                list.Add(j);
            }
            return list.ToArray();
        }
    }
}
