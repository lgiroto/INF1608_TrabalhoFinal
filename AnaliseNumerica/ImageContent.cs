using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AnaliseNumerica
{
    class ImageContent
    {
        public const int sizeX = 256;
        public const int sizeY = 256;
        public const int resolutionX = 128;
        public const int resolutionZ = 99;
        public const int numLayer = 99;
        public byte[] conteudo;
        
        public double DensityValue (int position ,int layer, double value)
        {
            //Implementacao ainda sem a interpolacao linear
            int index = Convert.ToInt32(layer * sizeX * sizeY + value * sizeX + position);
            return conteudo[index];
        }

        public double OpacityValue (int position, int layer, double value)
        {
            if (DensityValue(position,layer,value) < 0.3)
            {
                return 0;
            }
            // Nessa parte eu nao tenho certeza se eh a densidade mesmo que ele quer
            // No trabalho ele chama de "v" mas esse "v" nao aparece em nenhum lugar
            return 0.05 * (DensityValue(position, layer, value) - 0.3);
        }
    }
}
