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
}