using PhotoSauce.MagicScaler;
using PhotoSauce.NativeCodecs.Giflib;
using PhotoSauce.NativeCodecs.Libheif;
using PhotoSauce.NativeCodecs.Libjpeg;
using PhotoSauce.NativeCodecs.Libpng;
using PhotoSauce.NativeCodecs.Libwebp;

namespace ImageResizing
{
    internal class MagicScalerResizer : ImageResizer
    {
        public static void ResizeImages(string sourcesDirectory, bool isAnimated = false)
        {
            var filePaths = GetImagePaths(sourcesDirectory);
            //DeleteAllFilesInDirectory(Path.Combine(AppContext.BaseDirectory, "ResizedImages", "Magick"));

            foreach (var path in filePaths)
            {
                ResizeImage(path, isAnimated);
            }
        }

        public static void ResizeImage(string sourceImagePath, bool isAnimated = false)
        {
            var inputExtension = Path.GetExtension(sourceImagePath);
            var fileName = Path.GetFileNameWithoutExtension(sourceImagePath);

            CodecManager.Configure(codecs =>
            {
                codecs.UseLibwebp();
                codecs.UseLibjpeg();
                codecs.UseLibpng();
                codecs.UseGiflib();
                codecs.UseLibheif();
            });

            using var inputStream = new FileStream(sourceImagePath, FileMode.Open, FileAccess.Read);
            ImageFileInfo imageInfo = ImageFileInfo.Load(inputStream);
            inputStream.Position = 0;

            foreach (var size in sizes)
            {
                var width = imageInfo.Frames[0].Width;
                var height = imageInfo.Frames[0].Height;
                var scale = GetScale(width, imageInfo.Frames[0].Height, size.width, size.height);

                var newWidth = (int)(width * scale);
                var newHeight = (int)(height * scale);

                var settings = new ProcessImageSettings
                {
                    Width = newWidth,
                    Height = newHeight,
                    ResizeMode = CropScaleMode.Max
                };

                if (isAnimated)
                    settings.TrySetEncoderFormat(inputExtension switch
                    {
                        ".gif" => ImageMimeTypes.Gif,
                        ".webp" => ImageMimeTypes.Webp,
                        ".avifs" => ImageMimeTypes.Avif,
                        ".avif" => ImageMimeTypes.Avif,
                        _ => ImageMimeTypes.Gif
                    });

                var outputDirectory = Path.Combine(AppContext.BaseDirectory, "ResizedImages", "MagicScaler");
                var outputPath = Path.Combine(outputDirectory, $"{fileName}_{size.width}x{size.height}{inputExtension}");
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                using var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

                MagicImageProcessor.ProcessImage(inputStream, outputStream, settings);
                outputStream.Close();

                inputStream.Position = 0;
            }

            //Console.WriteLine($"---SUCCESSFULLY RESIZED IMAGE FORMAT: {inputExtension}\n Animated: {isAnimated}---");

            inputStream.Close();
        }
    }
}
