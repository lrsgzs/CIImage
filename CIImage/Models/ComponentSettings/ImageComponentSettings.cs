using System.Text.Json.Serialization;
using Avalonia;
using CIImage.Shared.Converters;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CIImage.Models.ComponentSettings;

public partial class ImageComponentSettings : ObservableObject
{
    [ObservableProperty] private string _imagePath = string.Empty;
    [ObservableProperty] private double _cornerRadius = 0;
    [ObservableProperty] private bool _showTravelFriendJoin = false;

    [JsonConverter(typeof(MatrixJsonConverter))]
    public Matrix Matrix
    {
        get;
        set
        {
            if (field == value) return;

            OnPropertyChanging();
            field = value;
            OnPropertyChanged();
        }
    } = Matrix.Identity;

    public double ZoomingCornerRadius => CornerRadius / 40 * 48;

    partial void OnCornerRadiusChanged(double value)
    {
        OnPropertyChanged(nameof(ZoomingCornerRadius));
    }
}
