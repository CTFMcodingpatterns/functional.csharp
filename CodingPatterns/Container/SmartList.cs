using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class SmartList<T>
    {
        private IEnumerable<T> MyList { get; set; }

        public SmartList(IEnumerable<T> list)
        {
            this.MyList = list;
        }

        public SmartList<R> Map<R>(Func<T, R> mapFunc)
        {
            IEnumerable<R> resultList = GetMappingIterator<R>(this.MyList, mapFunc);
            return new SmartList<R>(resultList);
        }

        public IEnumerable<R> GetMappingIterator<R>(IEnumerable<T> list, Func<T, R> mapFunc)
        {
            foreach (T element in MyList) {
                R result = mapFunc(element);
                yield return result;
            }
        }

        public SmartList<T> Filter(Predicate<T> filterFunc)
        {
            IEnumerable<T> resultList = GetFilteringIterator(this.MyList, filterFunc);
            return new SmartList<T>(resultList);
        }

        public IEnumerable<T> GetFilteringIterator(IEnumerable<T> list, Predicate<T> filterFunc)
        {
            foreach (T element in MyList) {
                if (filterFunc(element)) {
                    yield return element;
                }
            }
        }

        public override string ToString()
        {
            return MyList                
                .Aggregate("", (acc, el) => acc + "\r\n" + el);
        }

    }
}
