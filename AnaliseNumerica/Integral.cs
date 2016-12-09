namespace AnaliseNumerica
{
    class Integral
    {
        public static double Simpson(ImageContent imagem, int position, int layer, double a, double b, double h)
        {

            double x = a, sum = 0;

            while (x < b)
            {
                sum += (h / 6) * (imagem.OpacityValue(position, layer, x) + 
                                   4 * imagem.OpacityValue(position, layer, x + (h / 2)) +
                                   imagem.OpacityValue(position, layer, x + h));
                x += h;
            }

            return sum;
        }
    }
}
