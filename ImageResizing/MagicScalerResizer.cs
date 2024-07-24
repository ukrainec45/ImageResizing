using PhotoSauce.MagicScaler;
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
                    settings.TrySetEncoderFormat(inputExtension == ".gif" ? ImageMimeTypes.Gif : ImageMimeTypes.Webp);

                var outputDirectory = Path.Combine(AppContext.BaseDirectory, "ResizedImages", "MagicScaler");
                var outputPath = Path.Combine(outputDirectory, $"{fileName}_{size.width}x{size.height}{inputExtension}");
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                using var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

                MagicImageProcessor.ProcessImage(inputStream, outputStream, settings);
                outputStream.Close();

                inputStream.Position = 0;
            }
            inputStream.Close();
        }
    }
}
