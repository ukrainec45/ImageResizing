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

        public static void ResizeAnimatedGIFImage(string sourceImagePath)
        {
            var inputExtension = Path.GetExtension(sourceImagePath);
            var fileName = Path.GetFileNameWithoutExtension(sourceImagePath);
            var outputExtension = inputExtension;

            foreach (var size in sizes)
            {
                using var inputStream = File.OpenRead(sourceImagePath);

                var image = Image.Gifload(sourceImagePath, -1);
                var scale = GetScale(image.Width, image.Height, size.width, size.height);
                int nFrames = (int)image.Get("n-pages");
                Console.WriteLine($"Number of frames: {nFrames}");

                // List to hold resized frames
                var resizedFrames = new List<Image>();

                for (int i = 0; i < nFrames; i++)
                {
                    // Load the specific frame
                    var frame = Image.Gifload(sourceImagePath, page: i);
                    // Resize the frame
                    var resizedFrame = frame.Resize(scale);
                    resizedFrames.Add(resizedFrame);
                }

                var resizedImage = Image.Sum(resizedFrames.ToArray());

                var outputDirectory = Path.Combine(AppContext.BaseDirectory, "ResizedImages", "NetVips", "Animated");
                var outputPath = Path.Combine(outputDirectory, $"{fileName}_{size.width}x{size.height}{outputExtension}");

                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                using var outputStream = File.Open(outputPath, FileMode.OpenOrCreate);
                resizedImage.WriteToStream(outputStream, outputExtension);

                outputStream.Close();
                inputStream.Close();
            }
        }

        private static void ResizeImage(string sourceImagePath)
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
