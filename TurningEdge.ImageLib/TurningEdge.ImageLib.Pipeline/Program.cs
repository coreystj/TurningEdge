using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TurningEdge.ImageLib.Models.Images.Concretes;
using TurningEdge.ImageLib.Models.Images.Factories;
using TurningEdge.ImageLib.Models.Images.Interfaces;
using TurningEdge.Modeling.Common.Graphics.Structs;

namespace TurningEdge.ImageLib.Pipeline
{
    class Program
    {
        private static bool[] _fill;
        private static Color _blank = new Color(0, 0, 0, 0);
        private static Color _black = new Color(0, 0, 0, 1);

        static void Main(string[] args)
        {
            try
            {
                string path = args[0];
                string output = args[1];

                var imageFactory = new ImageFactory();

                Debugging.Debugger.Print("Reading: "+ path);
                IImage image = imageFactory.Create<PNGImage>(path);
                _fill = new bool[image.Height * image.Width];

                Debugging.Debugger.Print("Calculating outline...");
                OutlineImage(image);

                Debugging.Debugger.Print("Applying outline...");
                ColorOutline(image);

                Debugging.Debugger.Print("Saving image to: " + output);
                image.Save(output);
            }
            catch(Exception e)
            {
                Debugging.Debugger.PrintError(e);
            }
        }
        private static void ColorOutline(IImage image)
        {
            int i = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if(_fill[i])
                        image.SetPixel(x, y, _black);
                    i++;
                }
            }
        }

        private static void OutlineImage(IImage image)
        {
            int i = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    _fill[i] = CheckPixel(image, x, y);
                    i++;
                }
            }
        }

        private static bool CheckPixel(IImage image, int x, int y)
        {
            Color left = image.GetPixel(x - 1, y);
            Color right = image.GetPixel(x + 1, y);
            Color top = image.GetPixel(x, y + 1);
            Color bottom = image.GetPixel(x, y - 1);
            Color bottomLeft = image.GetPixel(x - 1, y - 1);
            Color topLeft = image.GetPixel(x - 1, y + 1);
            Color bottomRight = image.GetPixel(x + 1, y - 1);
            Color topRight = image.GetPixel(x + 1, y + 1);
            Color current = image.GetPixel(x, y);

            if (current.A == 0
                && (right.A != 0 
                || bottomRight.A != 0
                || left.A != 0
                || top.A != 0
                || bottomLeft.A != 0
                || topLeft.A != 0
                || topRight.A != 0))
                return true;
            return false;
        }
    }
}
