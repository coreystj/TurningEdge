using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TurningEdge.ImageLib.Models.Images.Abstracts;
using TurningEdge.ImageLib.Models.Images.Interfaces;
using TurningEdge.Modeling.Common.Graphics.Structs;

namespace TurningEdge.ImageLib.Models.Images.Concretes
{
    public class PNGImage : SystemImage<PNGImage>
    {

        public PNGImage(string path)
            : base(path, DataTypes.ImageTypes.PNG)
        {

        }

        public PNGImage(System.Drawing.Image rawImage)
            : base(rawImage, DataTypes.ImageTypes.PNG)
        {

        }

        public PNGImage(PNGImage otherImage)
            : base(otherImage)
        {

        }


    }
}
