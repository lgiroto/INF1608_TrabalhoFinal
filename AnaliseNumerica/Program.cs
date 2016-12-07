using System;
using System.IO;
using MathNet.Numerics.Integration;

namespace AnaliseNumerica
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] ImageContent = ReadRawFile();

            DefinePixels(ImageContent);

            WritePGMFile();
        }

        private static byte[] ReadRawFile()
        {
            try
            {
                byte[] data = File.ReadAllBytes(@"..\..\Assets\head-8bit.raw");
                return data;
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error);
                throw;
            }
        }

        private static void DefinePixels(byte [] ImageContent)
        {
            //double Valor = SimpsonRule.IntegrateComposite(x => x * x, 0.0, 10.0, 4);
            //Console.WriteLine(Valor);
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
