using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurningEdge.Helpers;
using TurningEdge.ImageLib.Models.Images.Concretes;
using TurningEdge.ImageLib.Models.Images.Repositories;
using TurningEdge.PhotoStudio.Pipeline.Core.DataTypes;

namespace TurningEdge.PhotoStudio.Pipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var commandType = (CommandTypes)args[0][1];
                string path = args[1];

                switch (commandType)
                {
                    case CommandTypes.Load:
                        LoadImageInformation(path);
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Invalid command entered: Please type -h for more details.");
            }
        }

        private static void LoadImageInformation(string path)
        {
            var repository = new ImageRepository();
            var image = repository.Create<PNGImage>(path) as PNGImage;
            Console.WriteLine(image);
        }
    }
}
