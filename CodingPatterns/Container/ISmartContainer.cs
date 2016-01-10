using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public interface ISmartContainer<T>
    {
        ISmartContainer<T> Filter(Predicate<T> filterFunc);

        ISmartContainer<R> Map<R>(Func<T, R> mapFunc);

        ISmartContainer<R> FlatMap<T2, R>(Func<T, IEnumerable<T2>> selectFunc, Func<T, T2, R> mapFunc);

        R Reduce<R>(R acc, Func<R, T, R> foldFunc);

        IEnumerable<T> ToList();
    }
}
