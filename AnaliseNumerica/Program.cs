using System;
using System.IO;
using AnaliseNumerica.Models;
using System.Collections.Generic;
using System.Linq;

namespace AnaliseNumerica
{
    public class Program
    {
        public const double h = 4.5;

        static void Main(string[] args)
        {
            Console.WriteLine("Lendo arquivo do CT Scan...");
            ImageContent image = new ImageContent();
            Console.WriteLine("Leitura do arquivo do CT Scan finalizada!");
            Console.WriteLine("Realizando cálculos...");
            List<Pixel> CTScanPixels = GeneratePGMData(image);
            Console.WriteLine("Cálculos finalizados!");
            Console.WriteLine("Gerando arquivo PGM...");
            WritePGMFile(CTScanPixels);
            Console.WriteLine("Arquivo PGM gerado. Veja na pasta 'Results'. Fim :)");
        }

        private static List<Pixel> GeneratePGMData(ImageContent imagem)
        {
            List<Pixel> ImagePixels = new List<Pixel>();

            for (int layer = 0; layer < ImageContent.numLayer; layer++)
            {
                Console.Write("\r{0}%", layer);
                for (int position = 0; position < ImageContent.resolutionX; position++)
                {
                    double intPixel = DefinePixel(imagem, 2*position, layer, h);
                    double intPixelNext = DefinePixel(imagem, 2*position + 1, layer, h);
                    double pixelIntensity = 255 * (intPixel + intPixelNext) / 2.0;

                    Pixel pixel = new Pixel();
                    pixel.PosX = position;
                    pixel.PosZ = layer;
                    pixel.Value = Convert.ToInt32(Math.Floor(pixelIntensity));
                    ImagePixels.Add(pixel);
                }
            }
            Console.Write("\r100%\n");
            return ImagePixels;
        }

        private static double DefinePixel (ImageContent imagem, int position, int layer, double h)
        {
            double sum = 0, s = 0;
            while (s < (ImageContent.sizeY - h))
            {
                double innerIntegralX = Integral.Simpson(imagem, position, layer, 0, s, h / 2);
                double innerIntegralXH_2 = Integral.Simpson(imagem, position, layer, 0, s + (h / 2), h / 2);
                double innerIntegralXH = Integral.Simpson(imagem, position, layer, 0, s + h, h / 2);

                sum += (h / 6) * ((imagem.OpacityValue(position, layer, s) * Math.Exp(-innerIntegralX)) +
                                   4 * (imagem.OpacityValue(position, layer, s + (h / 2)) * Math.Exp(-innerIntegralXH_2)) +
                                   (imagem.OpacityValue(position, layer, s + h) * Math.Exp(-innerIntegralXH)));
                s += h;
            }
            return sum;
        }

        private static void WritePGMFile(List<Pixel> CTScanPixels)
        {
            string currentLine = "P2";

            try
            {
                StreamWriter file = new StreamWriter(@"..\..\Results\Resultado.pgm");
                file.WriteLine(currentLine);

                currentLine = "# Resultado de um CT Scan";
                file.WriteLine(currentLine);

                currentLine = $"{ImageContent.resolutionX} {ImageContent.resolutionZ}";
                file.WriteLine(currentLine);

                var maxValue = CTScanPixels.Max(x => x.Value);
                currentLine = maxValue.ToString();
                file.WriteLine(currentLine);

                for (int z = 0; z < ImageContent.resolutionZ; z++)
                {
                    currentLine = "";
                    for (int x = 0; x < ImageContent.resolutionX; x++)
                    {
                        var value = CTScanPixels.FirstOrDefault(p => p.PosZ == z && p.PosX == x)?.Value;
                        if (value == null)
                            throw new Exception($"Erro: pixel ({x},{z}) nao encontrado");

                        string lineSpace = (x == ImageContent.resolutionX - 1) ? "" : " ";
                        currentLine += (value.ToString() + lineSpace);
                    }
                    file.WriteLine(currentLine);
                }

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
