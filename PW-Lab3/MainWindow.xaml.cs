using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
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
using System.Drawing;
using System.Collections;

namespace PW_Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableImage Image = new ObservableImage();

        public static int MirroredProperty { get; set; }

        public static int RotateProperty { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = Image;

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

            Img.RenderTransformOrigin = new Point(0.5, 0.5);
            MirroredProperty *= -1;

            ScaleTransform mirrorTransform = new ScaleTransform();
            mirrorTransform.ScaleX = MirroredProperty;

            Img.RenderTransform = mirrorTransform;
        }

        public void Rotate(object sender, EventArgs args)
        {
            if (Img.Source == null) return;
            Img.RenderTransformOrigin = new Point(0.5, 0.5);

            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = RotateProperty++ * 90;

            Img.RenderTransform = rotateTransform;
        }

        public void GreenOnly(object sender, EventArgs args)
        {
            if (Img.Source == null) return;

            BitmapSource image = (BitmapSource) Img.Source;
            int stride = (int) (image.PixelWidth * image.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[(int)image.PixelHeight * stride];
            image.CopyPixels(pixels, stride, 0);

            for(int i = 0, j = 2; j < pixels.Length; i += 4, j += 4)
            {
                pixels[i] = pixels[j] = 0;
            }

            Img.Source = BitmapSource.Create(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, image.Format, image.Palette, pixels, stride);
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
                pixels[i] = (byte)(pixels[i] * -1);
            }

            Img.Source = BitmapSource.Create(image.PixelWidth, image.PixelHeight, image.DpiX, image.DpiY, image.Format, image.Palette, pixels, stride);
        }
    }
}
