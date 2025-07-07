using CommunityToolkit.Mvvm.ComponentModel;

namespace CIImage.Components;

public class ImageComponentConfig : ObservableObject
{
    string _imagePath = string.Empty;
    public string ImagePath
    {
        get => _imagePath;
        set
        {
            if (_imagePath == value) return;
            _imagePath = value;
            OnPropertyChanged();
        }
    }

    double _cornerRadius = 0;
    public double CornerRadius
    {
        get => _cornerRadius;
        set
        {
            if (_cornerRadius == value) return;
            _cornerRadius = value;
            OnPropertyChanged();
        }
    }
}
