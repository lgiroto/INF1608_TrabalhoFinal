using System;
using System.IO;
using MathNet.Numerics.Integration;

namespace AnaliseNumerica
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageContent imagem = new ImageContent();

            DefinePixels(imagem);

            WritePGMFile();
        }

        private static void DefinePixels(ImageContent imagem)
        {
            //double Valor = SimpsonRule.IntegrateComposite(x => x * x, 0.0, 10.0, 4);
            //Console.WriteLine(Valor);

            //Valor determinado pelo Waldemar porque ele disse que seria mais facil

            double h = 4.5;
            for (int layer = 0; layer < ImageContent.numLayer; layer++)
            {
                for (int position = 0; position < ImageContent.sizeX; position++)
                {
                    double s = Integral.Simpson(imagem, position, layer, 0, ImageContent.sizeY, h);
                }
            } 
        }

        private static void WritePGMFile()
        {
            string currentLine = "P2";

            try
            {
                StreamWriter file = new StreamWriter(@"..\..\Results\Resultado.pgm");
                file.WriteLine(currentLine);

                currentLine = "# Descrição da imagem";
                file.WriteLine(currentLine);

                currentLine = "5 5"; // Dimensão dela, largura x altura
                file.WriteLine(currentLine);

                currentLine = "255"; // Maior valor encontrado de pixel na imagem
                file.WriteLine(currentLine);

                // Exemplo
                currentLine = "0 255 255 255 255";
                file.WriteLine(currentLine);
                currentLine = "255 0 255 255 255";
                file.WriteLine(currentLine);
                currentLine = "255 255 0 255 255";
                file.WriteLine(currentLine);
                currentLine = "255 255 255 0 255";
                file.WriteLine(currentLine);
                currentLine = "255 255 255 255 0";
                file.WriteLine(currentLine);

                file.Close();
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error);
                throw;
            }
        }

    }
}
