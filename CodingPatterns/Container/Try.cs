using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class Try<T>
    {
        public Exception Error { get; private set; }
        public T Value { get; private set; }

        private Try(Exception err, T val)
        {
            this.Error = err;
            this.Value = val;
        }

        public static Try<T> OfError(Exception err)
        {
            return new Try<T>(err, default(T));
        }

        public static Try<T> OfValue(T val)
        {
            return new Try<T>(null, val);
        }

        public Try<R> Map<R>(Func<T, R> mapFunc)
        {
            try {
                if (this.Error == null) {
                    R result = mapFunc(this.Value);
                    return Try<R>.OfValue(result);
                }
                else {
                    return Try<R>.OfError(this.Error);
                }
            }
            catch (Exception exceptionAfterMap) {
                return Try<R>.OfError(exceptionAfterMap);
            }
        }

        public override string ToString()
        {
            return ""
                + "\r\nError: " + this.Error
                + "\r\nValue: " + this.Value;
        }
    }
}
