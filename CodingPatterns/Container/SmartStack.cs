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

        public SmartStack(ImmuStack<T> stack)
        {
            this.MyStack = stack;
        }

        public SmartStack<T> Of(ImmuStack<T> stack)
        {
            return new SmartStack<T>(stack);
        }



    }
}
