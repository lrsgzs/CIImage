using CommunityToolkit.Mvvm.ComponentModel;

namespace CIImage.Models.ComponentSettings;

public partial class ImageComponentSettings : ObservableObject
{
    [ObservableProperty] private string _imagePath = string.Empty;
    [ObservableProperty] private double _cornerRadius = 0;
    [ObservableProperty] private bool _isAnimating = true;
}
