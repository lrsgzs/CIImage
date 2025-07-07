using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Attributes;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CIImage.Components;

[ComponentInfo(
    "fbe3f24b-a69b-4a1e-bcaf-899ccd773101",
    "图片",
    PackIconKind.Image,
    "在主界面上显示图片。"
)]
public partial class ImageComponent
{
    private string lastImagePath = string.Empty;
    private ILessonsService LessonsService { get; }

    public ImageComponent(ILessonsService lessonsService)
    {
        LessonsService = lessonsService;
        InitializeComponent();
    }

    void LoadPicture(object? sender, PropertyChangedEventArgs e)
    {
        LoadPicture();
    }

    void LoadPicture()
    {
        if (Settings.ImagePath != lastImagePath)
        {
            lastImagePath = Settings.ImagePath;
            BitmapImage bitmap = new BitmapImage();
            try
            {
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Settings.ImagePath);
                bitmap.EndInit();
                ImageViewer.Source = bitmap;
                LessonsService.PostMainTimerTicked += setGeometry; // 要不然 RenderSize.Width 为 0 就尴尬了

                ErrMsg.Content = "";
                ErrMsg.Visibility = Visibility.Collapsed;
                ImageViewer.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                ErrMsg.Content = "图片不存在";
                ErrMsg.Visibility = Visibility.Visible;
                ImageViewer.Visibility = Visibility.Collapsed;
            }

        }
    }

    private void setGeometry(object? sender, EventArgs e)
    {
        setGeometry();
    }

    void setGeometry()
    {
        Size size = ImageViewer.RenderSize;
        if (size.Width == 0) return;

        ImageViewerGeometry.Rect = new Rect(0, 0, size.Width, size.Height);
        LessonsService.PostMainTimerTicked -= setGeometry;
    }

    private void ComponentBase_Loaded(object sender, RoutedEventArgs e)
    {
        this.BeginInvoke(LoadPicture);
        Settings.PropertyChanged += LoadPicture;
    }

    private void ComponentBase_Unloaded(object sender, RoutedEventArgs e)
    {
        Settings.PropertyChanged -= LoadPicture;
    }
}
