using Microsoft.Win32;

namespace CIImage.Components;

public partial class ImageComponentSettings {
    public ImageComponentSettings()
    {
        InitializeComponent();
    }

    private void On_OpenFile(object sender, System.Windows.RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog()
        {
            FileName = Settings.ImagePath,
            Filter = "图片|*.jpg;*.png;*.jpeg;*.bmp;*.gif"
        };

        if (dialog.ShowDialog() != true)
            return;
        var file = dialog.FileName;
        Settings.ImagePath = file;
        
    }
}
