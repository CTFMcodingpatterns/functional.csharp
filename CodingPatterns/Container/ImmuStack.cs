using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class ImmuStack<T>
    {
        public T Head { get; private set;}

        public ImmuStack<T> Tail { get; private set; }

        private ImmuStack()
        {
        }

        public ImmuStack(T head, ImmuStack<T> tail)
        {
            this.Head = head;
            this.Tail = tail;
        }

        public static ImmuStack<T> Empty()
        {
            return new ImmuStack<T>(default(T), null);
        }

        public bool IsEmpty()
        {
            return (this.Head == null && this.Tail == null);
        }

        public ImmuStack<T> Push(T item)
        {
            return new ImmuStack<T>(item, this);
        }

        public R FoldLeft<R>(R acc, Func<R, T, R> foldFunc)
        {
            return FoldLeft<R>(acc, this, foldFunc);
        }

        public R FoldRight<R>(R acc, Func<R, T, R> foldFunc)
        {
            return this.Reverse().FoldLeft(acc, foldFunc);
        }

        public static ImmuStack<T> FromList(IEnumerable<T> list)
        {
            ImmuStack<T> stack = ImmuStack<T>.Empty();
            foreach (T item in list) {
                stack = stack.Push(item);
            }
            return stack.Reverse();
        }

        public IEnumerable<T> ToList()
        {
            IEnumerable<T> list = this.FoldLeft(new List<T>(),
                (acc, item) => { acc.Add(item); return acc; });
            return list;
        }

        public override string ToString()
        {
            const string sep = "\r\n";
            string result = ""
                + this.Head // + " (head)"
                + this.Tail.FoldLeft<string>("", (acc, item) => acc + sep + item);
            return result;
        }

        #region private helpers

        private static R FoldLeft<R>(R acc, ImmuStack<T> stack, Func<R, T, R> foldFunc)
        {
            return stack.IsEmpty()
                ? acc
                : FoldLeft(
                    foldFunc(acc, stack.Head), // the new accumulator
                    stack.Tail,                // the remaining stack
                    foldFunc);                 // pass foldFunc through
        }

        private ImmuStack<T> Reverse()
        {
            return FoldLeft(ImmuStack<T>.Empty(), (acc, item) => acc.Push(item));
        }

        private string ToString(T item)
        {
            try {
                return item as string;
            }
            catch (Exception ex) {
            }
            return null;
        }

        #endregion
    }
}
