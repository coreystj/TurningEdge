using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurningEdge.ImageLib.Models.Images.Abstracts;
using TurningEdge.ImageLib.Models.Images.Interfaces;

namespace TurningEdge.ImageLib.Models.Images.Concretes
{
    public class BMPImage : SystemImage<BMPImage>
    {
        public BMPImage(string path)
            : base(path, DataTypes.ImageTypes.BMP)
        {

        }

        public BMPImage(System.Drawing.Image rawImage)
            : base(rawImage, DataTypes.ImageTypes.BMP)
        {

        }

        public BMPImage(BMPImage otherImage)
            : base(otherImage)
        {

        }
    }
}
