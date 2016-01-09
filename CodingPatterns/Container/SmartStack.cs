using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class SmartStack<T>
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

        public R Reduce<R>(R acc, Func<R, T, R> foldFunc)
        {
            return MyStack.FoldRight<R>(acc, foldFunc);
        }

        public SmartStack<R> Map<R>(Func<T, R> mapFunc)
        {
            ImmuStack<R> resultStack = MyStack.FoldRight(
                ImmuStack<R>.Empty(),                      //init accumulator
                (acc, item) => acc.Push(mapFunc(item)));   //build accumulator and apply mapFunc
            return SmartStack<R>.Of(resultStack);
        }

        public SmartStack<T> Filter(Func<T, bool> filterFunc)
        {
            ImmuStack<T> resultStack = MyStack.FoldRight(
                ImmuStack<T>.Empty(),                                   //init accumulator
                (acc,item) => filterFunc(item) ? acc.Push(item) : acc); //add item to accu or pass old accu
            return SmartStack<T>.Of(resultStack);
        }

        public IEnumerable<T> ToList()
        {
            IEnumerable<T> result = Reduce<List<T>>(
                new List<T>(),
                (acc, item) => { acc.Add(item); return acc; });
            return MyStack.ToList();
        }

        public override string ToString()
        {
            return (MyStack != null)
                ? "\r\n" + MyStack.ToString()
                : null;
        }

    }
}
