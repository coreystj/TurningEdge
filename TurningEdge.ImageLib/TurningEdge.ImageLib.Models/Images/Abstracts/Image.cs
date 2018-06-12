using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TurningEdge.Generics.Abstracts;
using TurningEdge.ImageLib.Models.Images.Delegates;
using TurningEdge.ImageLib.Models.Images.Interfaces;
using TurningEdge.Modeling.Common.Graphics.Structs;

namespace TurningEdge.ImageLib.Models.Images.Abstracts
{
    public abstract class Image : BaseModel, IImage
    {
        public event OnImageLoadAction OnImageLoad = delegate { };
        public event OnImageLoadedAction OnImageLoaded = delegate { };
        public event OnImageSaveAction OnImageSave = delegate { };
        public event OnImageSavedAction OnImageSaved = delegate { };

        protected string _path;

        public string Path
        {
            get
            {
                return _path;
            }
        }

        public abstract int Width
        {
            get;
        }

        public abstract int Height
        {
            get;
        }

        public Image(string path)
            : base()
        {
            _path = path;
        }

        public abstract IImage Load(string path);
        public abstract void Save(string path);

        protected void FireOnImageLoad()
        {
            OnImageLoad(_path);
        }
        protected void FireOnImageLoaded()
        {
            OnImageLoaded(this);
        }
        protected void FireOnImageSave()
        {
            OnImageSave(_path);
        }
        protected void FireOnImageSaved()
        {
            OnImageSaved(_path);
        }

        public override string ToString()
        {
            return "width: " + Width + ", " +
                "height: " + Height + ", " +
                "path: " + _path + ", " +
                base.ToString();
        }

        public abstract void SetPixel(int v1, int v2, Modeling.Common.Graphics.Structs.Color color);

        public abstract Modeling.Common.Graphics.Structs.Color GetPixel(int v1, int v2);
    }
}
