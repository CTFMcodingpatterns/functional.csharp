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

    }
}
