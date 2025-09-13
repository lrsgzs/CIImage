using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using ImageComponentSettings = CIImage.Models.ComponentSettings.ImageComponentSettings;

namespace CIImage.Controls.Components;

[ComponentInfo(
    "fbe3f24b-a69b-4a1e-bcaf-899ccd773101",
    "图片",
    "\uE9B2",
    "在主界面上显示图片。"
)]
public partial class ImageComponent : ComponentBase<ImageComponentSettings>
{
    public ImageComponent()
    {
        InitializeComponent();
    }
}