using CommunityToolkit.Mvvm.ComponentModel;

namespace CIImage.Configs;

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
}
