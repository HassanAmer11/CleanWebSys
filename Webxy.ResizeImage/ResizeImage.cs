using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace Webxy.ResizeImage;

public static class ResizeImage
{

    public static System.Drawing.Image resizeImages( System.Drawing.Image imgToResize, Size size)
    {
        //Get the image current width
        int sourceWidth = imgToResize.Width;
        //Get the image current height
        int sourceHeight = imgToResize.Height;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;
        //Calulate  width with new desired size
        nPercentW = ((float)size.Width / (float)sourceWidth);
        //Calculate height with new desired size
        nPercentH = ((float)size.Height / (float)sourceHeight);


        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;
        //New Width
        int destWidth = (int)(sourceWidth * nPercent);
        //New Height
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        // Draw image with new width and height
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();

        return (System.Drawing.Image)b;
    }
    /// <summary>
    /// Function for resize image
    /// </summary>
    /// <param name="img">Image</param>
    /// <returns></returns>
    //private System.Drawing.Image resizeImage(System.Drawing.Image img)
    //{
    //    Bitmap b = new Bitmap(img);
    //    System.Drawing.Image i = resizeImage(b, new Size(150, 150));
    //    return i;
    //}

    public static void ReduceImageSize(double scaleFactor, Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = (int)(image.Width * scaleFactor);
            var newHeight = (int)(image.Height * scaleFactor);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }

    public static Bitmap ResizeImages(Image image, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var resultImage = new Bitmap(width, height);
        resultImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
        using (var graphics = Graphics.FromImage(resultImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using (var wrapMode = new ImageAttributes())
            {
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }
        return resultImage;
    }

    public static System.Drawing.Image resizeImage(string imagepath, System.Drawing.Image imgToResize, Size size)
    {
        //Get the image current width  
        int sourceWidth = imgToResize.Width;
        //Get the image current height  
        int sourceHeight = imgToResize.Height;
        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;
        //Calulate  width with new desired size  
        nPercentW = ((float)size.Width / (float)sourceWidth);
        //Calculate height with new desired size  
        nPercentH = ((float)size.Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;
        //New Width  
        int destWidth = (int)(sourceWidth * nPercent);
        //New Height  
        int destHeight = (int)(sourceHeight * nPercent);
        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        // Draw image with new width and height  
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();
       
        return (System.Drawing.Image)b;
    }

    /// <summary>
    ///     Resize a given image
    /// </summary>
    /// <param name="filePath">input file with path</param>
    /// <param name="width">width of resized image</param>
    /// <param name="height">height of resized image</param>
    public static void Resize(string filePath, int width, int height)
    {
        string filename = @filePath; //source image location

        var file = filePath;
        Console.WriteLine($"Loading {file}");
        using (var imageStream = new FileStream(file, FileMode.Open, FileAccess.Read))
        using (var image = new Bitmap(imageStream))
        {
            var resizedImage = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(resizedImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.DrawImage(image, 0, 0, width, height);
                var newFilePath = $"{Path.GetDirectoryName(file)}\\{Path.GetFileNameWithoutExtension(file)}_{width}x{height}.png";
                resizedImage.Save(
                    newFilePath,
                    ImageFormat.Png);
                Console.WriteLine($"Saving {newFilePath}");
            }
        }
    }
    /// <summary>
    ///  Resize a given image by maintaining the aspect ratio.
    /// </summary>
    /// <param name="filePath">input file with path</param>
    /// <param name="width">width of resized image</param>
    /// <param name="height">height of resized image</param>
    public static void ResizeImageWithAspectRatio(string newpath, string extension, string filePath, int width, int height)
    {
        Console.WriteLine($"Loading {filePath}");
        using (var imageStream = new FileStream(newpath, FileMode.Open, FileAccess.Read))
        using (var image = new Bitmap(imageStream))
        {
            var thumbnail = new Bitmap(width, height);
            using (var graphic = Graphics.FromImage(thumbnail))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                var ratioX = width / (double)image.Width;
                var ratioY = height / (double)image.Height;

                var ratio = ratioX < ratioY ? ratioX : ratioY;

                var newHeight = Convert.ToInt32(image.Height * ratio);
                var newWidth = Convert.ToInt32(image.Width * ratio);

                var posX = Convert.ToInt32((width - image.Width * ratio) / 2);
                var posY = Convert.ToInt32((height - image.Height * ratio) / 2);

                graphic.Clear(Color.White);
                graphic.DrawImage(image, posX, posY, newWidth, newHeight);
                var newFilePath =
                    $"{Path.GetDirectoryName(newpath)}\\{Path.GetFileNameWithoutExtension(filePath)}{extension}";
                thumbnail.Save(newFilePath,
                    ImageFormat.Png);
                Console.WriteLine($"Saving {newFilePath}");
            }
        }
    }

    //public static void ResizeImageWithAspectRatio(string newpath, string extension, string filePath, int width, int height)
    //{
    //    Console.WriteLine($"Loading {filePath}");
    //    using (var imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
    //    using (var image = new Bitmap(imageStream))
    //    {
    //        var thumbnail = new Bitmap(width, height);
    //        using (var graphic = Graphics.FromImage(thumbnail))
    //        {
    //            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
    //            graphic.CompositingQuality = CompositingQuality.HighQuality;
    //            graphic.SmoothingMode = SmoothingMode.HighQuality;
    //            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

    //            var ratioX = width / (double)image.Width;
    //            var ratioY = height / (double)image.Height;

    //            var ratio = Math.Min(ratioX, ratioY); // Keep aspect ratio

    //            var newWidth = Convert.ToInt32(image.Width * ratio);
    //            var newHeight = Convert.ToInt32(image.Height * ratio);

    //            var posX = Convert.ToInt32((width - newWidth) / 2);
    //            var posY = Convert.ToInt32((height - newHeight) / 2);

    //            graphic.Clear(Color.White); // Optional background
    //            graphic.DrawImage(image, posX, posY, newWidth, newHeight);

    //            var newFilePath = $"{Path.GetDirectoryName(newpath)}\\{Path.GetFileNameWithoutExtension(filePath)}{extension}";
    //            thumbnail.Save(newFilePath, ImageFormat.Png);

    //            Console.WriteLine($"Saving {newFilePath}");
    //        }
    //    }
    //}

}
