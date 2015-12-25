using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class IntParser
    {
        public static bool Parse(string numberStr, out int result)
        {
            try {
                result = Int32.Parse(numberStr);
                return true;
            }
            catch (Exception ex) {
                result = 0;
                return false;
            }
        }
    }
}
