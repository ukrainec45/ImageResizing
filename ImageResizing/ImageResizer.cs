using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizing
{
    public class ImageResizer
    {
        protected static readonly (int width, int height)[] sizes = new (int, int)[]
        {
            (64, 64),
            (214, 214),
            (328, 320),
            (642, 320),
            (1820, 1820),
        };

        protected static string[] GetImagePaths(string directoryPath)
        {
            string[] filePaths = Array.Empty<string>();
            try
            {
                filePaths = Directory.GetFiles(directoryPath);

                // Print the file paths
                foreach (string filePath in filePaths)
                {
                    Console.WriteLine(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return filePaths;
        }

        protected static void DeleteAllFilesInDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                var files = Directory.GetFiles(directoryPath);

                foreach (var file in files)
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine($"Deleted file: {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting file: {file}. Exception: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Directory does not exist: {directoryPath}");
            }
        }

        protected static double GetScale(int sourceImageWidth, int sourceImageHeight, int maxWidth, int maxHeight = -1)
        {
            if (maxHeight == -1)
                return (double)maxWidth / sourceImageWidth;

            return Math.Min((double)maxWidth / sourceImageWidth, (double)maxHeight / sourceImageHeight);
        }
    }
}
