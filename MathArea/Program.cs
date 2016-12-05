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

        public static double question3a(double dVal)
        {
            return (3.0 / 4.0) * Math.Sin(dVal);
        }

        public static double question3b(double dVal)
        {
            return (1.0 / (Math.Sqrt(2.0 * Math.PI))) * Math.Pow(Math.E, (-1.0 * (Math.Pow(dVal, 2)) / 2.0));
        }

        static double LRR(double xLeft, double xRight, double prec, fx dFunction)
        {
            int MAX_COUNT = 100;

            double totalWidth = Math.Abs(xRight - xLeft);
            double estimate = totalWidth * dFunction(xLeft);
            int count = 0;
            int numRect = 1;
            double oldEstimate;
            double currentLeft;
            double currentArea;
            double width;

            while(count++ < MAX_COUNT)
            {
                oldEstimate = estimate;
                estimate = 0;
                numRect *= 2;
                width = totalWidth/numRect;
                for (int i = 0; i < numRect; i++)
                {
                    currentLeft = xLeft + i * width;
                    currentArea = width * dFunction(currentLeft);
                    estimate = estimate + currentArea;
                }

                if( Math.Abs(estimate - oldEstimate) < prec)
                {
                    return estimate;
                }
            }

            return -1;

        }

        static double Trap(double leftEdge, double rightEdge, double prec, fx dFunction)
        {
            int MAX_LOOPS = 100;
            int numTrap = 1;
            double totalWidth = Math.Abs(rightEdge - leftEdge);
            double estimate = totalWidth * 0.5 * dFunction(leftEdge) + dFunction(rightEdge);
            int count = 1;
            double oldEstimate;
            double width;
            double currentRight;
            double currentLeft;
            double area;

            while(count++ < MAX_LOOPS)
            {
                oldEstimate = estimate;
                estimate = 0;

                numTrap *= 2;
                width = totalWidth / numTrap;
                for (int i = 0; i < numTrap; i++)
                {
                    currentLeft = leftEdge + i * width;
                    currentRight = currentLeft + width;
                    area = 0.5 * width * (dFunction(currentLeft) + dFunction(currentRight));
                    estimate = estimate + area;
                }
                if(Math.Abs(estimate - oldEstimate) < prec)
                {
                    return estimate;
                }
            }

            throw new ApplicationException("Did not converge");
        }

        static void Main(string[] args)
        {
            //Console.WriteLine(LRR(3, 9, 0.001, equation1));
            //Console.WriteLine("\n\n");
            //Console.WriteLine(LRR(0, 8, 0.001, equation2));
            Console.WriteLine("\n-------------------------Trapezoid Rule------------------------");
            Console.WriteLine(Trap(0, Math.PI, .0001, question3a));
            Console.WriteLine(Trap(-3, 3, .0001, question3b));
            Console.WriteLine("\n----------------------Left Rectangle Rule----------------------");
            Console.WriteLine(LRR(0, Math.PI, .0001, question3a));
            Console.WriteLine(LRR(-3, 3, .0001, question3b));
        }
    }
}
