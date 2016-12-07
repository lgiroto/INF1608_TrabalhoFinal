using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaliseNumerica
{
    class Integral
    {
        public double Simpson(Func<double, double> f, double a, double b, int n)
        {

            double h = (b - a) / n;
            double x = a;
            double soma = 0;

            while (x < b)
            {
                soma += (h / 6) * (f(x) + 4 * f(x + (h / 2)) + f(x + h));
                x += h;
            }

            return soma;
        }

        void DoubleSimpson(double a, double b, Func<double, double> f, SimpsonResult result)
        {

            double Sab, Sac, Scb;
            double c = (b + a) / 2;
            double fa = f(a);
            double fb = f(b);
            double fc = f(c);

            Sab = ((b - a) / 6) * (fa + 4 * fc + fb);

            Sac = ((c - a) / 6) * (fa + 4 * f((c + a) / 2) + fc);

            Scb = ((b - c) / 6) * (fc + 4 * f((c + b) / 2) + fb);

            result.resultado = Sac + Scb;
            result.erro = Math.Abs(Sab - Sac - Scb) / 15;
        }

        double AdaptiveSimpson(double a, double b, Func<double, double> f, double tol)
        {
            SimpsonResult result = new SimpsonResult();

            DoubleSimpson(a, b, f, result);

            if (result.erro <= tol)
            {
                return result.resultado;
            }
            else
            {
                double c = (a + b) / 2;
                return AdaptiveSimpson(a, c, f, tol) + AdaptiveSimpson(c, b, f, tol);
            }
        }
    }

    class SimpsonResult
    {
        public double resultado;
        public double erro;

        public SimpsonResult()
        {
            resultado = 0;
            erro = Double.MaxValue;
        }

        public SimpsonResult(double result, double err)
        {
            resultado = result;
            erro = err;
        }
    }
}
