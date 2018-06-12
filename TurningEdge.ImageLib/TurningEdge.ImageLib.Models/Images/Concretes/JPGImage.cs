using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.ImageLib.Models.Images.Abstracts;
using TurningEdge.ImageLib.Models.Images.Interfaces;

namespace TurningEdge.ImageLib.Models.Images.Concretes
{
    public class JPGImage : SystemImage<JPGImage>
    {
        public JPGImage(string path)
            : base(path, DataTypes.ImageTypes.JPG)
        {

        }

        public JPGImage(System.Drawing.Image rawImage)
            : base(rawImage, DataTypes.ImageTypes.JPG)
        {

        }

        public JPGImage(JPGImage otherImage)
            : base(otherImage)
        {

        }
    }
}
