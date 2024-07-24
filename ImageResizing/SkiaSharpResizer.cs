using SkiaSharp;

namespace ImageResizing
{
    public class SkiaSharpResizer : ImageResizer
    {
        protected static readonly Dictionary<string, SKEncodedImageFormat> extensionToFormat = new Dictionary<string, SKEncodedImageFormat>(StringComparer.OrdinalIgnoreCase)
        {
            { ".jpg", SKEncodedImageFormat.Jpeg },
            { ".jpeg", SKEncodedImageFormat.Jpeg },
            { ".png", SKEncodedImageFormat.Png },
            { ".bmp", SKEncodedImageFormat.Bmp },
            { ".gif", SKEncodedImageFormat.Gif },
            { ".webp", SKEncodedImageFormat.Webp }
        };

        public static void ResizeImages()
        {
            var filePaths = GetImagePaths(Path.Combine(AppContext.BaseDirectory, "Sources"));

            foreach (var path in filePaths)
            {
                ResizeImage2(path);
            }
        }

        public static void ResizeImage(string sourceImagePath)
        {
            var inputExtension = Path.GetExtension(sourceImagePath);
            var fileName = Path.GetFileNameWithoutExtension(sourceImagePath);
            var outputExtension = inputExtension;

            foreach (var size in sizes)
            {
                using var inputStream = File.OpenRead(sourceImagePath);

                using var skData = SKData.Create(inputStream);
                using var codec = SKCodec.Create(skData);

                using var destinationImage = SKBitmap.Decode(inputStream);
                var scale = GetScale(destinationImage.Width, destinationImage.Height, size.width, size.height);

                using var resizedImage = destinationImage.Resize(new SKImageInfo((int)(destinationImage.Width * scale), (int)(destinationImage.Height * scale)), SKFilterQuality.High);

                if (resizedImage == null)
                {
                    Console.WriteLine($"Failed to resize image to {size.width}x{size.height}");
                    continue;
                }

                var outputDirectory = Path.Combine(AppContext.BaseDirectory, "ResizedImages", "SkiaSharp");
                var outputPath = Path.Combine(outputDirectory, $"{fileName}_{size.width}x{size.height}{outputExtension}");

                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                if (!extensionToFormat.TryGetValue(inputExtension, out var encodeFormat))
                {
                    encodeFormat = SKEncodedImageFormat.Jpeg; // Default to JPEG if unknown extension
                    outputExtension = ".jpg";
                }

                using var outputImage = SKImage.FromBitmap(resizedImage);
                using var data = outputImage.Encode(encodeFormat, 90);
                using var outputStream = File.Open(outputPath, FileMode.OpenOrCreate);
                data.SaveTo(outputStream);

                outputStream.Close();
                inputStream.Close();

                Console.WriteLine($"Resized image saved to {outputPath}");
            }
        }

        public static void ResizeImage2(string sourceImagePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(sourceImagePath);
            var inputExtension = Path.GetExtension(sourceImagePath);
            var outputExtension = inputExtension;

            using var inputStream = File.OpenRead(sourceImagePath);
            using var original = SKBitmap.Decode(inputStream);
            foreach (var size in sizes)
            {
                var scale = GetScale(original.Width, original.Height, size.width, size.height);

                using var resized = original.Resize(new SKImageInfo((int)(original.Width * scale), (int)(original.Height * scale)), SKFilterQuality.High);

                // Resize the image
                if (resized == null)
                {
                    Console.WriteLine($"Failed to resize image to {size.width}x{size.height}");
                    continue;
                }

                // Determine the encode format
                var outputDirectory = Path.Combine(AppContext.BaseDirectory, "ResizedImages", "SkiaSharp");
                var outputPath = Path.Combine(outputDirectory, $"{fileName}_{size.width}x{size.height}{outputExtension}");

                // Extract the file extension
                string fileExtension = Path.GetExtension(outputPath);

                if (!extensionToFormat.TryGetValue(fileExtension, out var format))
                {
                    format = SKEncodedImageFormat.Jpeg; // Default to JPEG if unknown extension
                }

                // Ensure the output directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                // Save the resized image
                using var image = SKImage.FromBitmap(resized);
                using var data = image.Encode(format, 100);
                using var outputStream = File.OpenWrite(outputPath);
                data.SaveTo(outputStream);
                Console.WriteLine($"Resized image saved to {outputPath}");
            }
        }
    }
}
