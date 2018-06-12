using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.ImageLib.Models.Images.Delegates;
using TurningEdge.Modeling.Common.Graphics.Structs;

namespace TurningEdge.ImageLib.Models.Images.Interfaces
{
    public interface IImage
    {
        event OnImageLoadAction OnImageLoad;
        event OnImageLoadedAction OnImageLoaded;

        event OnImageSaveAction OnImageSave;
        event OnImageSavedAction OnImageSaved;

        int Width { get; }
        int Height { get; }
        string Path { get; }
        IImage Load(string path);
        void Save(string path);
        void SetPixel(int v1, int v2, Color color);
        Color GetPixel(int v1, int v2);
    }
}
