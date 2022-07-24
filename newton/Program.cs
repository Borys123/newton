using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newton
{
	class Program
	{
        public static double h = 0.00001;

		static double f(double x1, double x2)
		{
            return 4 * x2 * x2 * x2 + x1 * x1 - 3 * x1 * x2 - x1 + 9;
		}

		public static double px1(double x1, double x2)
		{
            return (1 / h) * (f(x1 + h, x2) - f(x1, x2));
		}

		static double px2(double x1, double x2)
		{
            return (1 / h) * (f(x1, x2 + h) - f(x1, x2));
		}

		static double p2x1(double x1, double x2)
		{
            return (1 / (h * h)) * ((f(x1 + h, x2) - (2 * f(x1, x2)) + f(x1 - h, x2)));
		}

		static double p2x2(double x1, double x2)
		{
            return (1 / (h * h)) * ((f(x1, x2 + h) - 2 * f(x1, x2)) + f(x1, x2 - h));
		}

		static double px1x2(double x1, double x2)
		{
            return (1 / h) * ((1 / h) * (f(x1 + h, x2 + h) - f(x1 + h, x2)) - (1 / h) * (f(x1, x2 + h) - f(x1, x2)));
		}

        static double[] g(double[] x0)
        {
            return new double[] {px1(x0[0], x0[1]), px2(x0[0], x0[1])};
        }
        
        static double[] g2(double[] x0)
        {
            return new double[] { p2x1(x0[0], x0[1]), px1x2(x0[0], x0[1]), px1x2(x0[0], x0[1]), p2x2(x0[0], x0[1]) };
        }

        static double[] g21(double[] x0)
        {
            double wyznacznik = (1 / (g2(x0)[0] * g2(x0)[3] - g2(x0)[1] * g2(x0)[2]));
            return new double[] {
            wyznacznik * p2x2(x0[0], x0[1]), wyznacznik * -1 * px1x2(x0[0], x0[1]),
            wyznacznik * (-1) * px1x2(x0[0], x0[1]), wyznacznik * p2x1(x0[0], x0[1])};
        }
		static void Main(string[] args)
		{
			double[] xk = { 1.0, 1.0 };
			double[] x0 = { 1.0, 1.0 };
            double eps = 0.0001;
            Console.WriteLine("Gradient = " + g(x0)[0] + " " + g(x0)[1]);
            Console.WriteLine("Gradient2 = " + g2(x0)[0] + " " + g2(x0)[1] + " " + g2(x0)[2] + " " + g2(x0)[3]);
            int it = 0;
			while ((Math.Abs(g(x0)[0]) >= eps && Math.Abs(g(x0)[1]) >= eps) || (Math.Abs(xk[1] - x0[1]) >= eps && Math.Abs(xk[0] - x0[0]) >= eps))
            {
                it++;
				x0[0] = xk[0];
				x0[1] = xk[1];
                xk[0] = (x0[0] - ((g21(x0)[0] * g(x0)[0]) + g21(x0)[1] * g(x0)[1]));
                xk[1] = (x0[1] - ((g21(x0)[2] * g(x0)[0]) + g21(x0)[3] * g(x0)[1]));
                
                if (Math.Abs(xk[1] - xk[0]) <= eps)
                    break;
                Console.WriteLine("xk = " + xk[0] + " , " + xk[1]);

            }
            Console.WriteLine("xk = " + xk[0] + " , " + xk[1]);
            Console.WriteLine("Iteracji: " + it);
            Console.ReadKey();
        }
	}
}
