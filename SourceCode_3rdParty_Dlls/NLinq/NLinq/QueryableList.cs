using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq
{
    public class QueryableList<T> : IQueryable<T>
    {
        protected IEnumerable<T> elements;

        public QueryableList(IEnumerable<T> elements)
        {
            this.elements = elements;
        }

        public IQueryable<T> Select(Action<T> action)
        {
            List<T> filtered = new List<T>(elements);
            filtered.ForEach(action);
            return new QueryableList<T>(filtered);
        }

        public IQueryable<T> Where(Predicate<T> predicate)
        {
            List<T> filtered = new List<T>(elements);
            return new QueryableList<T>(filtered.FindAll(predicate));
        }

        public IQueryable<T> OrderBy(Comparison<T> comparison)
        {
            List<T> filtered = new List<T>(elements);
            filtered.Sort(comparison);
            return new QueryableList<T>(filtered);
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        #endregion
    }
}
