using BenchmarkDotNet.Attributes;

namespace ImageResizing
{
    public class Benchmark
    {
        [Benchmark]
        public void MagicScalerResizeJpg()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.jpg");
            MagicScalerResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagicScalerResizePng()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.png");
            MagicScalerResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagicScalerResizeBmp()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.bmp");
            MagicScalerResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagicScalerResizeWebP()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.webp");
            MagicScalerResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagicScalerResizeGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.gif");
            MagicScalerResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagicScalerResizeAnimatedGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Animated", "animated-gif.gif");
            MagicScalerResizer.ResizeImage(sourceImagePath, true);
        }

        [Benchmark]
        public void MagicScalerResizeAnimatedWebP()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Animated", "animated-webp.webp");
            MagicScalerResizer.ResizeImage(sourceImagePath, true);
        }

        [Benchmark]
        public void MagickResizeJpg()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.jpg");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizePng()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.png");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeBmp()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.bmp");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.gif");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeWebP()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.webp");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeAnimatedGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Animated", "animated-gif.gif");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void MagickResizeAnimatedWebP()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Animated", "animated-webp.webp");
            MagickResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeJpg()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.jpg");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizePng()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.png");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeBmp()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.bmp");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.gif");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeWebP()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "headphones.webp");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }

        [Benchmark]
        public void ImageSharpResizeAnimatedGif()
        {
            var sourceImagePath = Path.Combine(AppContext.BaseDirectory, "Sources", "Animated", "animated-gif.gif");
            ImageSharpResizer.ResizeImage(sourceImagePath);
        }
    }
}
