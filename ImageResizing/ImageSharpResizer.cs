using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using IImageEncoder = SixLabors.ImageSharp.Formats.IImageEncoder;

namespace ImageResizing
{
    public class ImageSharpResizer : ImageResizer
    {
        public static void ResizeImages(string sourcesDirectory, bool isAnimated = false)
        {
            var filePaths = GetImagePaths(sourcesDirectory);
            //DeleteAllFilesInDirectory(Path.Combine(AppContext.BaseDirectory, "ResizedImages", "Magick"));

            foreach (var path in filePaths)
            {
                ResizeImage(path);
            }
        }

        public static void ResizeImage(string sourceImagePath)
        {
            var inputExtension = Path.GetExtension(sourceImagePath);

            var fileName = Path.GetFileNameWithoutExtension(sourceImagePath);

            using var inputStream = new FileStream(sourceImagePath, FileMode.Open, FileAccess.Read);
            var image = Image.Load(inputStream, out var format);
            var imageFormat = format;
            var encoder = GetEncoder(imageFormat, image.Metadata);

            foreach (var size in sizes)
            {
                var scale = GetScale(image.Width, image.Height, size.width, size.height);

                var outputDirectory = Path.Combine(AppContext.BaseDirectory, "ResizedImages", "ImageSharp");
                var outputPath = Path.Combine(outputDirectory, $"{fileName}_{size.width}x{size.height}{inputExtension}");
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                using var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
                if (scale == 1)
                {
                    image.Save(outputStream, encoder);
                    return;
                }

                var newWidth = (int)(image.Width * scale);
                var newHeight = (int)(image.Height * scale);

                var resized = image.Clone(x => x.Resize(newWidth, newHeight, KnownResamplers.Bicubic));
                resized.Save(outputStream, encoder);

                outputStream.Close();
                inputStream.Position = 0;
            }

            inputStream.Close();
        }

        private static IImageEncoder GetEncoder(IImageFormat format, ImageMetadata metadata)
        {
            if (format == JpegFormat.Instance)
                return new JpegEncoder() { Quality = 95 };

            if (format == PngFormat.Instance)
                return new PngEncoder() { CompressionLevel = PngCompressionLevel.Level1 };

            if (format == BmpFormat.Instance)
                return new BmpEncoder() { BitsPerPixel = metadata.GetBmpMetadata().BitsPerPixel };

            return SixLabors.ImageSharp.Configuration.Default.ImageFormatsManager.FindEncoder(format);
        }
    }
}
