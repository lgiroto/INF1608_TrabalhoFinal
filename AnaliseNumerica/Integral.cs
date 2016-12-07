using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaliseNumerica
{
    class Integral
    {
        public static double Simpson(ImageContent imagem, int position, int layer, double a, double b, double h)
        {

            double x = a;
            double soma = 0;

            while (x < b)
            {
                soma += (h / 6) * (imagem.OpacityValue(position, layer, x) + 4 * imagem.OpacityValue(position, layer, x + (h / 2)) + imagem.OpacityValue(position, layer, x + h));
                x += h;
            }

            return soma;
        }

        public static void DoubleSimpson(double a, double b, Func<double, double> f, SimpsonResult result)
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

        public static double AdaptiveSimpson(double a, double b, Func<double, double> f, double tol)
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

    // classe criada porque pareceu a forma mais facil de implementar o metodo adptativo que precisa retornar dois valores
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
