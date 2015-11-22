using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class Maybe<T>
    {
        public static readonly Maybe<T> EMPTY = new Maybe<T>(default(T));
        private T MyValue { get; set; }

        public Maybe(T value)
        {
            this.MyValue = value;
        }

        public T Get()
        {
            return MyValue;
        }

        public Maybe<R> Map<R>(Func<T, R> mapFunc)
        {
            if (MyValue == null) {
                return new Maybe<R>(default(R));
            }
            else {
                R result = mapFunc(MyValue);
                return new Maybe<R>(result);
            }
        }

        public Maybe<R> FlatMap<R>(Func<T, Maybe<R>> mapOptFunc)
        {
            if (MyValue == null) {
                return new Maybe<R>(default(R));
            }
            else {
                return mapOptFunc(MyValue);
            }
        }

        public override bool Equals(object obj)
        {
            Maybe<T> other = obj as Maybe<T>;
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
