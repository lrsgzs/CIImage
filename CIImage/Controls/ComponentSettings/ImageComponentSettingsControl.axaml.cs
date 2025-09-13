using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using CIImage.Models.ComponentSettings;
using ClassIsland.Core.Abstractions.Controls;

namespace CIImage.Controls.ComponentSettings;

public partial class ImageComponentSettingsControl : ComponentBase<ImageComponentSettings>
{
    public ImageComponentSettingsControl()
    {
        InitializeComponent();
    }

    private async void On_OpenFile(object sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

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
        if (result.Count == 0) return;
        
        var file = result[0];
        Settings.ImagePath = file.Path.AbsolutePath;
    }
}