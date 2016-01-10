using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace Container
{
    public class SmartCollection<T> : ISmartContainer<T>
    {
        private ImmutableStack<T> MyStack { get; set; }

        public SmartCollection(ImmutableStack<T> stack) 
        {
            this.MyStack = stack;
        }

        public static SmartCollection<T> Of(ImmutableStack<T> stack) 
        {
            return new SmartCollection<T>(stack);
        }

        public static SmartCollection<T> Of(IEnumerable<T> list)
        {
            ImmutableStack<T> stack = ImmutableStack.CreateRange<T>(list);
            return new SmartCollection<T>(stack);
        }


        public ISmartContainer<T> Filter(Predicate<T> filterFunc)
        {
            ImmutableStack<T> resultStack = Reduce<ImmutableStack<T>>(
                ImmutableStack<T>.Empty,
                (acc,item) => filterFunc(item) ? acc.Push(item) : acc
            );
            return new SmartCollection<T>(resultStack);
        }

        public ISmartContainer<R> Map<R>(Func<T, R> mapFunc)
        {
            ImmutableStack<R> resultStack = Reduce<ImmutableStack<R>>(
                ImmutableStack<R>.Empty,
                (acc,item) => acc.Push(mapFunc(item))
            );
            return new SmartCollection<R>(resultStack);
        }

        public ISmartContainer<R> FlatMap<T2, R>(Func<T, IEnumerable<T2>> selectFunc, Func<T, T2, R> mapFunc)
        {
            throw new NotImplementedException();
        }

        public R Reduce<R>(R acc, Func<R, T, R> foldFunc)
        {
            ImmutableStack<T> rightStack = Reverse();
            return FoldLeft<R>(acc, rightStack, foldFunc);
        }

        public IEnumerable<T> ToList()
        {
            return Reduce<List<T>>(
                new List<T>(),
                (acc, item) => { acc.Add(item); return acc; });
        }


        internal static R FoldLeft<R>(R acc, ImmutableStack<T> stack, Func<R, T, R> foldFunc)
        {
            return (stack.IsEmpty)
                ? acc
                : FoldLeft(
                    foldFunc(acc, stack.Peek()),  //operation filling acc, using stack.head
                    stack.Pop(),                  //pass tail of stack
                    foldFunc);                    //pass foldFunc
        }

        internal ImmutableStack<T> Reverse()
        {
            return FoldLeft<ImmutableStack<T>>(
                ImmutableStack<T>.Empty, 
                MyStack, 
                (acc, item) => acc.Push(item));
        }
    }
}
