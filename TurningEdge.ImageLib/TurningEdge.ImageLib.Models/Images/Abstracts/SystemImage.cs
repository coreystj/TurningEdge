using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TurningEdge.ImageLib.Models.Images.DataTypes;
using TurningEdge.ImageLib.Models.Images.Interfaces;

namespace TurningEdge.ImageLib.Models.Images.Abstracts
{
    public abstract class SystemImage<T> : Image, ISystemImage
        where T : SystemImage<T>
    {

        protected System.Drawing.Image _rawImage;
        protected ImageTypes _type;

        public override int Width
        {
            get
            {
                return _rawImage.Width;
            }
        }

        public override int Height
        {
            get
            {
                return _rawImage.Height;
            }
        }

        public System.Drawing.Image RawImage
        {
            get
            {
                return _rawImage;
            }
        }

        public ImageTypes Type
        {
            get
            {
                return _type;
            }
        }

        public SystemImage(string path, ImageTypes type)
            : base(path)
        {
            Load(path);
            _type = type;
        }

        public SystemImage(System.Drawing.Image rawImage, ImageTypes type) 
            : base(string.Empty)
        {
            _rawImage = rawImage;
            _type = type;
        }

        public SystemImage(T otherImage)
            : base(otherImage._path)
        {
            _rawImage = otherImage._rawImage;
            _type = otherImage._type;
        }

        public SystemImage(string path) 
            : base(path)
        {
        }

        public override IImage Load(string path)
        {
            FireOnImageLoad();
            _rawImage = System.Drawing.Image.FromFile(path);
            FireOnImageLoaded();
            return this;
        }

        public override void Save(string path)
        {
            FireOnImageSave();
            _rawImage.Save(path);
            FireOnImageSaved();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override Modeling.Common.Graphics.Structs.Color GetPixel(int x, int y)
        {
            System.Drawing.Color color;
            Bitmap bitmap = RawImage as Bitmap;
            if (x < 0 || y < 0 || x > Width - 1 || y > Height - 1)
                color = Color.FromArgb(0,0,0,1);
            else
                color = bitmap.GetPixel(x, y);
            return new Modeling.Common.Graphics.Structs.Color(color.R, color.G, color.B, color.A);
        }

        public override void SetPixel(int x, int y, Modeling.Common.Graphics.Structs.Color color)
        {
            Bitmap bitmap = RawImage as Bitmap;
            bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(
                (int)(color.A * 255),
                (int)(color.R * 255),
                (int)(color.G * 255),
                (int)(color.B * 255)));
        }
    }
}
