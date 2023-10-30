using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_Calculator
{
    public static  class OtherHelperFunctions
    {
        public static double CastNumber(string numberText)
        {
            return double.Parse(numberText);
        }

        public static void CultureDot()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }


        public static string FirstOperationInstanceText(double number, string operationName)
        {

            string operationText = "";
            switch (operationName)
            {
                case "btnSum": operationText = " + "; break;
                case "btnSubt": operationText = " - "; break;
                case "btnMult": operationText = " * "; break;
                case "btnDevide": operationText = " / "; break;
                default: break;
            }
            return $"{number}{operationText}";
        }

        public static string EqualOperationInstanceText(double number)
        {
            return $"{number} = ";
        }
    }
}
