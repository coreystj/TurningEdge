using System;
using System.Collections.Generic;
using System.Text;

namespace TurningEdge.Modeling.Common.Graphics.Structs
{
    [Serializable]
    public struct Color
    {
        private float _r;
        private float _g;
        private float _b;
        private float _a;

        public float R
        {
            get { return _r; }
            set { _r = value; }
        }

        public float G
        {
            get { return _g; }
            set { _g = value; }
        }

        public float B
        {
            get { return _b; }
            set { _b = value; }
        }

        public float A
        {
            get { return _a; }
            set { _a = value; }
        }

        public Color(float r, float g, float b)
            : this(r, g, b, 1f) { }

        public Color(float r, float g, float b, float a)
        {
            _r = r;
            _g = g;
            _b = b;
            _a = a;
        }

        public override string ToString()
        {
            return "Color{" 
                + _r + ", " 
                + _g + ", " 
                + _b + ", " 
                + _a 
                + "}";
        }
    }
}
