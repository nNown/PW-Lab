using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PW_Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int MirroredProperty { get; set; }

        public static int RotateProperty { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MirroredProperty = RotateProperty = 1;
        }

        public void LoadImage(object sender, EventArgs args)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "BMP|*.bmp";
            dialog.Title = "Open an Image file";

            if (dialog.ShowDialog() ?? false)
            {
                //Image.Image = new BitmapImage(new Uri(dialog.FileName));
                Img.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        public void Mirror(object sender, EventArgs args)
        {
            if (Img.Source == null) return;

            Img.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            MirroredProperty *= -1;

            ScaleTransform mirrorTransform = new ScaleTransform();
            mirrorTransform.ScaleX = MirroredProperty;

            Img.RenderTransform = mirrorTransform;
        }

        public void Rotate(object sender, EventArgs args)
        {
            if (Img.Source == null) return;
            Img.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = RotateProperty++ * 90;

            Img.RenderTransform = rotateTransform;
        }

        public void GreenOnly(object sender, EventArgs args)
        {
            if (Img.Source == null) return;
            BitmapImage img = (BitmapImage)Img.Source;
            Bitmap bitmap = ImageToBitmap(img);
            for(int i = 0; i < bitmap.Height; i++)
            {
                for(int j = 0; j < bitmap.Width; j++)
                {
                    System.Drawing.Color pixel = bitmap.GetPixel(j, i);
                    int alpha = pixel.A;
                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;

                    if(red > 150 || blue > 150)
                    {
                        red = green = blue = 255;
                    }

                    bitmap.SetPixel(j, i, System.Drawing.Color.FromArgb(alpha, red, green, blue));
                }
            }
            Img.Source = BitmapToImage(bitmap);
        }

        public void Negative(object sender, EventArgs args)
        {
            if (Img.Source == null) return;

            BitmapSource image = (BitmapSource)Img.Source;
            int stride = (int)(image.PixelWidth * image.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[(int)image.PixelHeight * stride];
            image.CopyPixels(pixels, stride, 0);

            for (var i = 0; i < pixels.Length; i++)
            {
                pixels[i] = (byte)(255 - pixels[i]);
            }

            Img.Source = BitmapSource.Create(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, image.Format, image.Palette, pixels, stride);
        }

        private BitmapImage BitmapToImage(Bitmap img)
        {
            using (MemoryStream mr = new MemoryStream())
            {
                img.Save(mr, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = mr;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private Bitmap ImageToBitmap(BitmapImage img)
        {
            using (MemoryStream mr = new MemoryStream())
            {
                BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();
                bitmapEncoder.Frames.Add(BitmapFrame.Create(img));
                bitmapEncoder.Save(mr);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(mr);
                return new Bitmap(bitmap);
            }
        }
    }
}
