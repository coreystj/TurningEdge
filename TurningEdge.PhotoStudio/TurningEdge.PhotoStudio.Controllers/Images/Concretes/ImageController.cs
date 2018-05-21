using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurningEdge.Generics.Interfaces;
using TurningEdge.ImageLib.Models.Images.Abstracts;
using TurningEdge.ImageLib.Models.Images.Interfaces;

namespace TurningEdge.PhotoStudio.Controllers.Images.Concretes
{
    //         [Controller]
    //        /  /      \  \
    //       /Notify  Action\
    //      /  /          \  \
    //  Update/            \Update
    //    /  /              \  \
    //  [Model]             [View]

    public class ImageController : IController<Image>
    {
        private IView<Image> _view;
        private Image _model;
        public ImageController(IView<Image> view, Image image)
        {
            _view = view;
            _model = image;

            Bind(image);
        }

        ~ImageController()
        {
            Unbind();
        }

        public void Bind(Image model)
        {
            _model = model;

            _model.OnImageLoad += Model_OnImageLoad;
            _model.OnImageLoaded += Model_OnImageLoaded;
            _model.OnImageSave += Model_OnImageSave;
            _model.OnImageSaved += Model_OnImageSaved;
        }

        public Image Unbind()
        {
            _model.OnImageLoad -= Model_OnImageLoad;
            _model.OnImageLoaded -= Model_OnImageLoaded;
            _model.OnImageSave -= Model_OnImageSave;
            _model.OnImageSaved -= Model_OnImageSaved;

            return _model;
        }

        private void Model_OnImageSaved(string path)
        {
        }

        private void Model_OnImageSave(string path)
        {
        }

        private void Model_OnImageLoaded(Image image)
        {
            _view.Update(image);
        }

        private void Model_OnImageLoad(string path)
        {
        }
    }
}
