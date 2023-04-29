using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace net.sictransit.wefax
{
    public class ImageMake
    {
        public string MakeImage(string imageFilename) 
        {

            using (SixLabors.ImageSharp.Image ImageData = SixLabors.ImageSharp.Image.Load<Rgb24>(imageFilename))
            {
                














            }




            return "";
        }
    }
}
