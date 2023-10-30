using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calculator
{
    public static class MathOperations
    {
        public static double Sum(double num1, double num2)
        {
            return num1 + num2;
        }

        public static double Subt(double num1, double num2)
        {
            return num1 - num2;
        }

        public static double Mult(double num1, double num2)
        {
            return num1 * num2;
        }

        public static double Devide(double num1, double num2)
        {
            if (Validation.CheckDevision(num2)) return num1 / num2;
            throw new ArgumentException("Can not devide by zero");
        }

    }
}
