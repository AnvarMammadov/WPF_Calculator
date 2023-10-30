using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calculator
{
    public static class Validation
    {
        public static bool CheckDevision(double num)
        {
            if (num != 0) return true;
            return false;
        }

        public static bool CheckSquareRoot(double num)
        {
            if (num >= 0) return true;
            return false;
        }

    }
}
