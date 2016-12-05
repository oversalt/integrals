using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathArea
{
    class Program
    {
        public delegate double fx(double x);

        public static double equation1(double dVal)
        {
            return ((1.0 / 3.0) * Math.Pow(dVal, 2) - (1.0 / 300.0) * Math.Pow(dVal, 4));
        }
         
        public static double equation2(double dVal)
        {
            return (1.0 / 4.0) * dVal;
        }

        static double LRR(double xLeft, double xRight, double prec, fx dFunction)
        {
            int MAX_COUNT = 100;

            double totalWidth = (xRight - xLeft);
            double estimate = totalWidth * dFunction(xLeft);
            int count = 0;
            int numRect = 1;
            double oldEstimate;
            double currentLeft;
            double currentArea;

            while(count++ < MAX_COUNT)
            {
                oldEstimate = estimate;
                estimate = 0;
                numRect *= 2;
                totalWidth /= numRect;
                for (int i = 0; i < numRect; i++)
                {
                    currentLeft = xLeft + i * totalWidth;
                    currentArea = totalWidth * dFunction(currentLeft);
                    estimate = estimate + currentArea;
                }

                if( Math.Abs(estimate - oldEstimate) < prec)
                {
                    return estimate;
                }
            }

            return -1;

        }
        static void Main(string[] args)
        {
            Console.WriteLine(LRR(3, 9, 0.001, equation1));
            Console.WriteLine("\n\n");
            Console.WriteLine(LRR(0, 8, 0.001, equation2));
        }
    }
}
