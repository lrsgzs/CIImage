using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Attributes;
using System.ComponentModel;
using System.Windows;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using Avalonia.Visuals;
using ClassIsland.Core.Abstractions.Controls;

namespace CIImage.Components;

[ComponentInfo(
    "fbe3f24b-a69b-4a1e-bcaf-899ccd773101",
    "图片",
    "\uE9B2",
    "在主界面上显示图片。"
)]
public partial class ImageComponent : ComponentBase<ImageComponentConfig>
{
    public ImageComponent()
    {
        InitializeComponent();
    }

    private void LoadPicture(object? sender, PropertyChangedEventArgs e)
    {
        LoadPicture();
    }

    private void LoadPicture()
    {
        ImageViewer.Source = Settings.ImagePath;
    }

    private void Component_Loaded(object? sender, RoutedEventArgs e)
    {
        Dispatcher.UIThread.Invoke(LoadPicture);
        Settings.PropertyChanged += LoadPicture;
    }

    private void Component_Unloaded(object sender, RoutedEventArgs e)
    {
        Settings.PropertyChanged -= LoadPicture;
    }
}