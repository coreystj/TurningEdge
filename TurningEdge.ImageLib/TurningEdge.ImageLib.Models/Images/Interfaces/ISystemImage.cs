using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurningEdge.ImageLib.Models.Images.DataTypes;

namespace TurningEdge.ImageLib.Models.Images.Interfaces
{
    public interface ISystemImage : IImage
    {
        System.Drawing.Image RawImage { get; }
        ImageTypes Type { get; }
    }
}
