using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluant.NLinq
{
    public interface IQueryable<T> : IEnumerable<T>
    {
        IQueryable<T> Select(Action<T> action);
        IQueryable<T> Where(Predicate<T> predicate);
        IQueryable<T> OrderBy(Comparison<T> comparison);
    }
}
