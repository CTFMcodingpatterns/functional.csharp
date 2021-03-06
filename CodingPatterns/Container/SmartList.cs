﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class SmartList<T> : ISmartContainer<T>
    {
        private IEnumerable<T> MyList { get; set; }

        private SmartList(IEnumerable<T> list)
        {
            this.MyList = list;
        }

        static public SmartList<T> Of(IEnumerable<T> list)
        {
            return new SmartList<T>(list);
        }

        public ISmartContainer<T> Filter(Predicate<T> filterFunc)
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

        public ISmartContainer<R> Map<R>(Func<T, R> mapFunc)
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

        public ISmartContainer<T2> FlatMap<T2>(Func<T, IEnumerable<T2>> selectFunc)

        {
            IEnumerable<T2> resultList = GetFlatMapIterator(selectFunc);
            return new SmartList<T2>(resultList);
        }

        public IEnumerable<T2> GetFlatMapIterator<T2>(Func<T, IEnumerable<T2>> selectFunc)
        {
            foreach (T element in MyList)
            {
                foreach (T2 innerElement in selectFunc(element))
                {
                    yield return innerElement;
                }
            }
        }

        public ISmartContainer<R> FlatMap<T2, R>(Func<T, IEnumerable<T2>> selectFunc,
            Func<T, T2, R> mapFunc)
        {
            IEnumerable<R> resultList = GetFlatMapIterator(selectFunc, mapFunc);
            return new SmartList<R>(resultList);
        }

        public IEnumerable<R> GetFlatMapIterator<T2,R>(Func<T, IEnumerable<T2>> selectFunc,
            Func<T, T2, R> mapFunc)
        {
            foreach (T element in MyList) {
                foreach (T2 innerElement in selectFunc(element)) {
                    yield return mapFunc(element, innerElement);
                }
            }
        }

        public R Reduce<R>(R seed, Func<R,T,R> foldFunc) 
        {
            //TODO
            R result = MyList.Aggregate(seed, foldFunc); //TODO: no seed!!!
            return result;
        }

        public IEnumerable<T> ToList()
        {
            return Reduce<List<T>>(
                new List<T>(),
                (acc, item) => { acc.Add(item); return acc; });
        }

        public override string ToString()
        {
            return MyList                
                .Aggregate("", (acc, el) => acc + "\r\n" + el);
        }

    }
}
