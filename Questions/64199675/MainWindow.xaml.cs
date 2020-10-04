using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyMethod();
        }

private void MyMethod()
{
    ImageSource   StageWindowImageSource = img.Source;
    CroppedBitmap Crop                   = new CroppedBitmap((BitmapSource) StageWindowImageSource, new Int32Rect(20, 20, 150, 110));

    img2.Source = GetImage(Crop, new JpegBitmapEncoder());
}

private BitmapImage GetImage(BitmapSource source, BitmapEncoder encoder)
{
    var image = new BitmapImage();

    using (var sourceMs = new MemoryStream())
    {
        encoder.Frames.Add(BitmapFrame.Create(source));
        encoder.Save(sourceMs);

        sourceMs.Position = 0;
        using (var destMs = new MemoryStream(sourceMs.ToArray()))
        {
            image.BeginInit();
            image.StreamSource = destMs;
            image.CacheOption  = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();
        }
    }

    return image;
}
    }
}
