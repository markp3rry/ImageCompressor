using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageCompressor
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo info = new DirectoryInfo(@"c:\jpeg_out");
            foreach (FileInfo fi in info.GetFiles())
            {
                fi.Delete();
            }
            string[] files = Directory.GetFiles(@"c:\jpeg_in");
            foreach (string f in files)
            {
                System.Drawing.Image img;
                img = Image.FromFile(f);
                SaveJpeg(f.Replace("jpeg_in", "jpeg_out"), img, 10);
            }
        }

        public static void SaveJpeg(string path, Image img, int quality)
        {
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codecs.Length; i++)
            {
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            }
            return null;
        }
    }
}
