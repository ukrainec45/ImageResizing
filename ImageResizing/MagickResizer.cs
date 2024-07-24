using ImageMagick;

namespace ImageResizing
{
    internal class MagickResizer : ImageResizer
    {
        protected static readonly Dictionary<string, MagickFormat> extensionToFormat = new Dictionary<string, MagickFormat>(StringComparer.OrdinalIgnoreCase)
        {
            { ".jpg", MagickFormat.Jpeg },
            { ".jpeg", MagickFormat.Jpeg },
            { ".png", MagickFormat.Png },
            { ".bmp", MagickFormat.Bmp },
            { ".gif", MagickFormat.Gif },
            { ".webp", MagickFormat.WebP },
            { ".avif", MagickFormat.Avif },
            { ".avifs", MagickFormat.Avif }
        };

        public static void ResizeImages(string sourcesDirectory)
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

            using var inputStream = File.OpenRead(sourceImagePath);

            if (!extensionToFormat.TryGetValue(inputExtension, out var encodeFormat))
            {
                encodeFormat = MagickFormat.Jpeg; // Default to JPEG if unknown extension
                inputExtension = ".jpg";
            }
            var magicImageCollection = new MagickImageCollection(inputStream, encodeFormat);

            int width = magicImageCollection.First().Width;
            int height = magicImageCollection.First().Height;

            foreach (var size in sizes)
            {
                var scale = GetScale(width, height, size.width, size.height);

                var newWidth = (int)(width * scale);
                var newHeight = (int)(height * scale);

                var clonedImageCollection = magicImageCollection.Clone();

                var outputDirectory = Path.Combine(AppContext.BaseDirectory, "ResizedImages", "Magick");

                var outputPath = Path.Combine(outputDirectory, $"{fileName}_{size.width}x{size.height}{inputExtension}");
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                using var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

                if (!IsAnimatedImage(magicImageCollection))
                {
                    ResizeCore(clonedImageCollection.First(), newWidth, newHeight);

                    clonedImageCollection.Write(outputStream);
                    inputStream.Position = 0;
                    continue;
                }

                clonedImageCollection.Coalesce();
                foreach (var image in clonedImageCollection)
                {
                    ResizeCore(image, newWidth, newHeight);
                }

                clonedImageCollection.Write(outputStream);
                outputStream.Close();

                inputStream.Position = 0;
            }
            inputStream.Close();
        }

        private static void ResizeCore(IMagickImage<byte> image, int newWidth, int newHeight)
        {
            var size = new MagickGeometry(newWidth, newHeight)
            {
                IgnoreAspectRatio = true
            };
            image.Resize(size);
        }

        private static bool IsAnimatedImage(MagickImageCollection imageCollection) => imageCollection.Count > 1;
    }
}
