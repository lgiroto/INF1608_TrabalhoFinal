using System;
using System.IO;

namespace AnaliseNumerica
{
    public class ImageContent
    {
        public const int sizeX = 256;
        public const int sizeY = 256;
        public const int resolutionX = 128;
        public const int resolutionZ = 99;
        public const int numLayer = 99;
        public byte[] content;
        
        public ImageContent()
        {
            content = ReadRawFile();
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

        public double DensityValue (int position, int layer, double value)
        {
            int index = 0;
            if((value % 1) == 0)
            {
                index = Convert.ToInt32((layer * sizeX * sizeY) + (value * sizeX) + position);
                return content[index];
            }
            else
            {
                int firstIndex = Convert.ToInt32((layer * sizeX * sizeY) + (Math.Floor(value) * sizeX) + position);
                int lastIndex = Convert.ToInt32((layer * sizeX * sizeY) + (Math.Ceiling(value) * sizeX) + position);
                return ((value - Math.Floor(value)) * content[firstIndex]) + ((Math.Ceiling(value) - value) * content[lastIndex]);
            }
        }

        public double OpacityValue (int position, int layer, double value)
        {
            var densityVal = DensityValue(position, layer, value)/255.0;
            return (densityVal < 0.3) ?  0 : (0.05 * (densityVal - 0.3));
        }
    }
}
