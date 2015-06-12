using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Core.Domain
{
    public interface ITreeEntity<T> where T : ITreeEntity<T>
    {
        int Level { get; set; }

        T Parent { get; set; }

        IList<T> Children { get; set; }

    }

    public static class ITreeEntityExtentions
    {
        public static IList<T> ToList<T>(this ITreeEntity<T> treeEntity)
             where T : ITreeEntity<T>
        {
            List<T> list = new List<T>();
            list.Add((T)treeEntity);
            if (treeEntity.Children != null && treeEntity.Children.Count > 0)
            {
                foreach (var c in treeEntity.Children)
                {
                    list.AddRange(ToList(c as ITreeEntity<T>));
                }
            }
            return list;
        }
    }

    public class TreeEntityConst
    {
        public const int TopLevel = 1;
    }
}
