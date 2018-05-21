using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurningEdge.Generics.Interfaces;
using TurningEdge.ImageLib.Models.Images.Abstracts;
using TurningEdge.ImageLib.Models.Images.Factories;
using TurningEdge.ImageLib.Models.Images.Interfaces;

namespace TurningEdge.ImageLib.Models.Images.Repositories
{
    public class ImageRepository : IRepository<IImage>
    {
        private ImageFactory _imageFactory;

        public ImageRepository()
        {
            _imageFactory = new ImageFactory();
        }

        public IImage Create<T>(string path)
            where T : SystemImage<T>
        {
            var image = _imageFactory.Create<T>(path);
            return image;
        }

        public IImage Create(IImage model)
        {
            throw new NotImplementedException();
        }

        public IImage Delete(IImage model)
        {
            throw new NotImplementedException();
        }

        public IImage Read(IImage model)
        {
            throw new NotImplementedException();
        }

        public IImage Update(IImage model)
        {
            throw new NotImplementedException();
        }
    }
}
