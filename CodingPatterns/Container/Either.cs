using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class Either<L,R>
    {
        public L Left { get; private set; }
        public R Right { get; private set; }

        private Either(L left, R right)
        {
            this.Left = left;
            this.Right = right;
        }

        public static Either<L, R> OfLeft(L left)
        {
            return new Either<L, R>(left, default(R));
        }

        public static Either<L, R> OfRight(R right)
        {
            return new Either<L, R>(default(L), right);
        }

        public override string ToString()
        {
            return ""
                + "\r\nLeft:  " + Left
                + "\r\nRight: " + Right;
        }
    }
}
