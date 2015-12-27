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

        public ImmuStack<T> FromList(IEnumerable<T> list)
        {
            ImmuStack<T> stack = ImmuStack<T>.Empty();
            foreach (T item in list) {
                stack = new ImmuStack<T>(item, stack);
            }
            return stack;
        }

        public bool IsEmpty()
        {
            return (this.Head == null && this.Tail == null);
        }

        public ImmuStack<T> Push(T item)
        {
            return new ImmuStack<T>(item, this);
        }

        public IEnumerable<T> ToList()
        {
            IList<T> list = new List<T>();
            ImmuStack<T> stack = ImmuStack<T>.Empty();
            while (! stack.IsEmpty()) {
                list.Add(stack.Head);
                stack = stack.Tail;
            }
            return list;
        }

        public static R Fold<R>(R acc, ImmuStack<T> stack, Func<R,T,R> foldFunc)
        {
            return stack.IsEmpty()
                ? acc
                : Fold(
                    foldFunc(acc, stack.Head), // the new accumulator
                    stack.Tail,                // the remaining stack
                    foldFunc);                 // pass foldFunc through
        }

        public override string ToString()
        {
            string result = "";
            int i = 0;
            for (ImmuStack<T> stack = this; !stack.IsEmpty(); stack = stack.Tail)
            {
                result = result + "\r\n[" + i++ + "]: " + stack.Head;
            }
            return result;
        }

        public string ToString2(ImmuStack<T> stack)
        {
            const string sep = " | ";
            string result = stack.IsEmpty()
                ? ""
                : stack.Head + sep + ToString2(stack.Tail);
            return result;
        }

        public string ToString3(string acc, ImmuStack<T> stack)
        {
            const string sep = " | ";
            return stack.IsEmpty()
                ? ""
                : ToString3(stack.Head + sep + acc, stack.Tail);
        }
    }
}
