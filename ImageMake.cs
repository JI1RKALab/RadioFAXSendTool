using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SixLabors.ImageSharp.Processing;

namespace net.sictransit.wefax
{
    public class ImageMake
    {
        public string MakeImage(string imageFilename)
        {
            // Path作成
            string FileDirPath = System.IO.Path.GetDirectoryName(imageFilename);

            // ファイル名
            string TempFilePath = System.IO.Path.Combine(FileDirPath, "TempImage.png");

            using (Image ImageData = Image.Load<Rgb24>(imageFilename))
            {
                using (Image BrackData = new Image<Rgba32>((int)Math.Round(ImageData.Width * 1.045), ImageData.Height))
                {
                    BrackData.Mutate(x => x.BackgroundColor(SixLabors.ImageSharp.Color.Black));
                    BrackData.Mutate(x => x.DrawImage(ImageData, new SixLabors.ImageSharp.Point((int)Math.Round(ImageData.Width * 1.045) - ImageData.Width, 0), opacity: 1f));
                    BrackData.SaveAsPng(TempFilePath);

                }
            }

            return TempFilePath;
        }
    }
}