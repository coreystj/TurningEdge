using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Generics.Abstracts;
using TurningEdge.Generics.Factories;
using TurningEdge.ImageLib.Models.Images.Interfaces;

namespace TurningEdge.ImageLib.Models.Images.Factories
{
    public class ImageFactory : Factory<IImage>
    {
        public IImage Create<Y>(string path)
            where Y : IImage
        {
            return base.Create<Y>(path) as IImage;
        }
    }
}
