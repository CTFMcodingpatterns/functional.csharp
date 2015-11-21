using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class Optional<T>
    {
        public static readonly Optional<T> EMPTY = new Optional<T>(default(T));
        private T MyValue { get; set; }

        public Optional(T value)
        {
            this.MyValue = value;
        }

        public T Get()
        {
            return MyValue;
        }

        public Optional<R> Map<R>(Func<T, R> mapFunc)
        {
            if (MyValue == null) {
                return new Optional<R>(default(R));
            }
            else {
                R result = mapFunc(MyValue);
                return new Optional<R>(result);
            }
        }

        public Optional<R> FlatMap<R>(Func<T, Optional<R>> mapOptFunc)
        {
            if (MyValue == null) {
                return new Optional<R>(default(R));
            }
            else {
                return mapOptFunc(MyValue);
            }
        }

        public override bool Equals(object obj)
        {
            Optional<T> other = obj as Optional<T>;
            if (other == null) {
                return false;
            }
            if (this.MyValue == null && other.MyValue == null) {
                return true;
            }
            if (this.MyValue.Equals(other.MyValue)) {
                return true;
            }
            return false;
        }

    }
}
