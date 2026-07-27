using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Threading;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Icons;
using ImageComponentSettings = CIImage.Models.ComponentSettings.ImageComponentSettings;

namespace CIImage.Controls.Components;

[ComponentInfo(
    "fbe3f24b-a69b-4a1e-bcaf-899ccd773101",
    "图片",
    FluentIcons.ImageRegular,
    "在主界面上显示图片。"
)]
public partial class ImageComponent : ComponentBase<ImageComponentSettings>
{
    public ImageComponent()
    {
        InitializeComponent();
        
        AttachedToVisualTree += OnAttachedToVisualTree;
    }

    private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        Settings.PropertyChanged += (s, e) =>
        {
            ZoomBorder.SetMatrix(Settings.Matrix);
        };

        Dispatcher.UIThread.InvokeAsync(async () =>
        {
            await Task.Delay(300);
            ZoomBorder.SetMatrix(Settings.Matrix);
        }, DispatcherPriority.ApplicationIdle);
    }
}