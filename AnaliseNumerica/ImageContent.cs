using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaliseNumerica
{
    class ImageContent
    {
        public byte[] conteudo;
        
        public double DensityValue (int position ,int layer, double value)
        {
            return 3;
        }

        public double OpacityValue (int position, int layer, double value)
        {
            return 4;
        }
    }
}
