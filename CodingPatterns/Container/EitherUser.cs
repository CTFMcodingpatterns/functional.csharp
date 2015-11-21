using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container
{
    public class EitherUser
    {
        public static Either<Exception, int> Parse(string numStr)
        {
            try
            {
                int result = Int32.Parse(numStr);
                return Either<Exception, int>.OfRight(result);
            }
            catch (Exception ex)
            {
                return Either<Exception, int>.OfLeft(ex);
            }
        }
    }
}
