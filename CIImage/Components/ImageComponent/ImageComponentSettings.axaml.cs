using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using ClassIsland.Core.Abstractions.Controls;

namespace CIImage.Components;

public partial class ImageComponentSettings : ComponentBase<ImageComponentConfig>
{
    public ImageComponentSettings()
    {
        InitializeComponent();
    }

    private async void On_OpenFile(object sender, RoutedEventArgs e)
    {
        // var dialog = new OpenFileDialog()
        // {
        //     FileName = Settings.ImagePath,
        //     Filter = "图片|*.jpg;*.png;*.jpeg;*.bmp;*.gif"
        // };
        //
        // if (dialog.ShowDialog() != true)
        //     return;
        // var file = dialog.FileName;
        // Settings.ImagePath = file;

        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null)
        {
            return;
        }

        var storage = topLevel.StorageProvider;

        var result = await storage.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "打开图片文件",
            FileTypeFilter =
            [
                new FilePickerFileType("图片")
                {
                    Patterns = ["*.png", "*.jpg", "*.jpeg", "*.bmp", "*.gif"],
                    MimeTypes = ["image/*"]
                }
            ],
            AllowMultiple = false,
            SuggestedFileName = Settings.ImagePath,
        });

        if (result.Count == 0)
        {
            return;
        }

        var file = result[0];
        Settings.ImagePath = file.Path.AbsolutePath;
    }
}