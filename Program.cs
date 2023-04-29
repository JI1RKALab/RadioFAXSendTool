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
                      // 判定
                      if (!File.Exists(o.SourceImage))
                      {
                          // 判定
                          throw new FileNotFoundException(o.SourceImage);
                      }
                      else
                      {
                          // ここで画信号を操作する
                          string FileImage = new ImageMake().MakeImage(o.SourceImage);

                          // 画信号ファイルを生成
                          string WaveFileName = Path.Combine(Path.GetDirectoryName(o.SourceImage), $"{Path.GetFileNameWithoutExtension(o.SourceImage)}.wav");

                          // イニシャライズ
                          FaxMachine faxMachine = new(16000, 1900, 400, 576);

                          // 信号生成
                          faxMachine.Fax(FileImage, WaveFileName, new BinaryCodedHeader(o.SatelliteName, o.SectorName, o.Date, o.Time, o.SectorName, o.Open));

                          // テンポラリファイル消す
                          File.Delete(FileImage);
                      }
                  });
        }

    }
}