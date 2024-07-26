using NetVips;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizing
{
    public class NetVipsResizer : ImageResizer
    {
        public static void ResizeImages()
        {
            var filePaths = GetImagePaths(Path.Combine(AppContext.BaseDirectory, "Sources"));
            DeleteAllFilesInDirectory(Path.Combine(AppContext.BaseDirectory, "ResizedImages", "NetVips"));

            foreach (var path in filePaths)
            {
                ResizeImage(path);
            }
        }

        public static void ResizeAnimatedImage(string sourceImagePath)
        {
            var inputExtension = Path.GetExtension(sourceImagePath);
            var fileName = Path.GetFileNameWithoutExtension(sourceImagePath);
            var outputExtension = inputExtension;

            using var inputStream = File.OpenRead(sourceImagePath);
            var image = Image.NewFromFile(sourceImagePath);

            foreach (var size in sizes)
            {
                var scale = GetScale(image.Width, image.Height, size.width, size.height);
                using var thumb = Image.Thumbnail($"{sourceImagePath}[n=-1]", (int)(image.Width * scale), (int?)(image.Height * scale));

                var outputDirectory = Path.Combine(AppContext.BaseDirectory, "ResizedImages", "NetVips");
                var outputPath = Path.Combine(outputDirectory, $"{fileName}_{size.width}x{size.height}{outputExtension}");

                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                using var outputStream = File.Open(outputPath, FileMode.OpenOrCreate);
                thumb.WriteToStream(outputStream, outputExtension);

                outputStream.Close();
                inputStream.Position = 0;
            }
            inputStream.Close();
        }

        public static void ResizeImage(string sourceImagePath)
        {
            var inputExtension = Path.GetExtension(sourceImagePath);
            var fileName = Path.GetFileNameWithoutExtension(sourceImagePath);
            var outputExtension = inputExtension;

            foreach (var size in sizes)
            {
                using var inputStream = File.OpenRead(sourceImagePath);

                var image = Image.NewFromFile(sourceImagePath);
                var scale = GetScale(image.Width, image.Height, size.width, size.height);
                image = image.Resize(scale);

                var outputDirectory = Path.Combine(AppContext.BaseDirectory, "ResizedImages", "NetVips");
                var outputPath = Path.Combine(outputDirectory, $"{fileName}_{size.width}x{size.height}{outputExtension}");

                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                using var outputStream = File.Open(outputPath, FileMode.OpenOrCreate);
                image.WriteToStream(outputStream, outputExtension);

                outputStream.Close();
                inputStream.Close();
            }
        }
    }
}
