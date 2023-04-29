using CommandLine;
using Serilog;
using System.IO;

namespace net.sictransit.wefax
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Parser.Default.ParseArguments<Options>(args)
                  .WithParsed(o =>
                  {
                      if (!File.Exists(o.SourceImage))
                      {
                          throw new FileNotFoundException(o.SourceImage);
                      }

                      // Todo:ここで画信号を操作する
                      string FileImage = new ImageMake().MakeImage(o.SourceImage);

                      string wavFilename = Path.Combine(Path.GetDirectoryName(o.SourceImage), $"{Path.GetFileNameWithoutExtension(o.SourceImage)}.wav");

                      FaxMachine faxMachine = new(16000, 1900, 400, 576);

                      faxMachine.Fax(FileImage, wavFilename, new BinaryCodedHeader(o.SatelliteName, o.SectorName, o.Date, o.Time, o.SectorName, o.Open));

                      //ファイル消す
                      File.Delete(FileImage);
                  });
        }

    }
}