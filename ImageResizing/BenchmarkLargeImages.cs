using BenchmarkDotNet.Attributes;

namespace ImageResizing
{
    [MemoryDiagnoser]
    public class BenchmarkLargeImages
    {
        [Benchmark]
        public void MagicScalerResizeLargeJpg()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.jpg");
            MagicScalerResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagicScalerResizeLargeGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.gif");
            MagicScalerResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagicScalerResizeLargePng()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.png");
            MagicScalerResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagicScalerResizeLargeWebp()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.webp");
            MagicScalerResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagicScalerResizeLargeAnimatedGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "Animated", "large-animated-gif.gif");
            MagicScalerResizer.ResizeImage(sourceImagePath, true);
        }

        [Benchmark]
        public void MagicScalerResizeLargeAnimatedWebp()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "Animated", "large-animated-webp.webp");
            MagicScalerResizer.ResizeImage(sourceImagePath, true);
        }

        [Benchmark]
        public void MagickResizeLargeJpg()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.jpg");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeLargeGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.gif");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeLargePng()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.png");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeLargeWebp()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.webp");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeLargeAnimatedGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "Animated", "large-animated-gif.gif");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeLargeAnimatedWebp()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "Animated", "large-animated-webp.webp");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeLargeJpg()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.jpg");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeLargeGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.gif");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeLargePng()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.png");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeLargeWebp()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "large.webp");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeLargeAnimatedGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "Animated", "large-animated-gif.gif");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeLargeAnimatedWebp()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Large", "Animated", "large-animated-webp.webp");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }
    }
}
