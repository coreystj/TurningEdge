using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurningEdge.ImageLib.Models.Images.Abstracts;

namespace TurningEdge.ImageLib.Models.Images.Delegates
{
    public delegate void OnImageLoadAction(string path);
    public delegate void OnImageLoadedAction(Image image);

    public delegate void OnImageSaveAction(string path);
    public delegate void OnImageSavedAction(string path);
}
