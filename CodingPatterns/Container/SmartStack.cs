using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class SmartStack<T> : ISmartContainer<T>
    {
        private ImmuStack<T> MyStack { get; set; }

        private SmartStack(ImmuStack<T> stack)
        {
            this.MyStack = stack;
        }

        public static SmartStack<T> Of(ImmuStack<T> stack)
        {
            return new SmartStack<T>(stack);
        }

        public static SmartStack<T> Of(IEnumerable<T> list)
        {
            return new SmartStack<T>(ImmuStack<T>.FromList(list));
        }

        public ISmartContainer<T> Filter(Predicate<T> filterFunc)
        {
            ImmuStack<T> resultStack = MyStack.FoldRight(
                ImmuStack<T>.Empty(),                                   //init accumulator
                (acc, item) => filterFunc(item) ? acc.Push(item) : acc); //add item to accu or pass old accu
            return SmartStack<T>.Of(resultStack);
        }

        public ISmartContainer<R> Map<R>(Func<T, R> mapFunc)
        {
            ImmuStack<R> resultStack = MyStack.FoldRight(
                ImmuStack<R>.Empty(),                      //init accumulator
                (acc, item) => acc.Push(mapFunc(item)));   //build accumulator and apply mapFunc
            return SmartStack<R>.Of(resultStack);
        }

        public ISmartContainer<R> FlatMap<T2, R>(Func<T, IEnumerable<T2>> selectFunc, Func<T, T2, R> mapFunc)
        {
            throw new NotImplementedException();
        }

        public R Reduce<R>(R acc, Func<R, T, R> foldFunc)
        {
            return MyStack.FoldRight<R>(acc, foldFunc);
        }

        public IEnumerable<T> ToList()
        {
            return MyStack.FoldLeft<IList<T>>(
                new List<T>(),
                (acc, item) => { acc.Add(item); return acc; }
                );
        }

        public override string ToString()
        {
            return (MyStack != null)
                ? "\r\n" + MyStack.ToString()
                : null;
        }


    }
}
