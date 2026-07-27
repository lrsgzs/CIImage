using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using CIImage.Models.ComponentSettings;
using ClassIsland.Core.Abstractions.Controls;
using CommunityToolkit.Mvvm.Input;

namespace CIImage.Controls.ComponentSettings;

public partial class ImageComponentSettingsControl : ComponentBase<ImageComponentSettings>
{
    public Dictionary<string, string> PresetImageList { get; } = new()
    {
        ["AppLogo"] = "avares://ClassIsland/Assets/AppLogo.png",
        ["AppLogo_Fade"] = "avares://ClassIsland/Assets/AppLogo_Fade.png",
        ["AppLogo_Monochrome"] = "avares://ClassIsland/Assets/AppLogo_Monochrome.png",
        ["白厄_掉线"] = "avares://ClassIsland/Assets/HoYoStickers/白厄_掉线.png",
        ["白厄_没事"] = "avares://ClassIsland/Assets/HoYoStickers/白厄_没事.png",
        ["白厄_我吗"] = "avares://ClassIsland/Assets/HoYoStickers/白厄_我吗.png",
        ["白厄_再见"] = "avares://ClassIsland/Assets/HoYoStickers/白厄_再见.png",
        ["白厄_战斗"] = "avares://ClassIsland/Assets/HoYoStickers/白厄_战斗.png",
        ["光辉矢愿_遨游"] = "avares://ClassIsland/Assets/HoYoStickers/光辉矢愿_遨游.png",
        ["光辉矢愿_不可以看"] = "avares://ClassIsland/Assets/HoYoStickers/光辉矢愿_不可以看.png",
        ["光辉矢愿_回眸"] = "avares://ClassIsland/Assets/HoYoStickers/光辉矢愿_回眸.png",
        ["光辉矢愿_瞄准"] = "avares://ClassIsland/Assets/HoYoStickers/光辉矢愿_瞄准.png",
        ["光辉矢愿_想我了吗"] = "avares://ClassIsland/Assets/HoYoStickers/光辉矢愿_想我了吗.png",
        ["光辉矢愿_小喇叭"] = "avares://ClassIsland/Assets/HoYoStickers/光辉矢愿_小喇叭.png",
        ["米沙_欢迎光临"] = "avares://ClassIsland/Assets/HoYoStickers/米沙_欢迎光临.png",
        ["帕姆_不可以"] = "avares://ClassIsland/Assets/HoYoStickers/帕姆_不可以.png",
        ["帕姆_点赞"] = "avares://ClassIsland/Assets/HoYoStickers/帕姆_点赞.png",
        ["帕姆_嗨"] = "avares://ClassIsland/Assets/HoYoStickers/帕姆_嗨.png",
        ["帕姆_哭哭"] = "avares://ClassIsland/Assets/HoYoStickers/帕姆_哭哭.png",
        ["帕姆_震惊"] = "avares://ClassIsland/Assets/HoYoStickers/帕姆_震惊.png",
        ["帕姆_注意"] = "avares://ClassIsland/Assets/HoYoStickers/帕姆_注意.png",
        ["昔涟_爱"] = "avares://ClassIsland/Assets/HoYoStickers/昔涟_爱.png",
        ["昔涟_回眸"] = "avares://ClassIsland/Assets/HoYoStickers/昔涟_回眸.png",
        ["昔涟_收到"] = "avares://ClassIsland/Assets/HoYoStickers/昔涟_收到.png",
        ["昔涟_守护"] = "avares://ClassIsland/Assets/HoYoStickers/昔涟_守护.png"
    };
    
    public ImageComponentSettingsControl()
    {
        InitializeComponent();
    
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        ZoomBorder.SetMatrix(Settings.Matrix);
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

    [RelayCommand]
    private void ApplyPreset(string uri)
    {
        Settings.ImagePath = uri;
    }

    private void RevertChange_OnClick(object? sender, RoutedEventArgs e)
    {
        ZoomBorder.SetMatrix(Settings.Matrix);
    }

    private void ApplyChange_OnClick(object? sender, RoutedEventArgs e)
    {
        Settings.Matrix = ZoomBorder.Matrix;
    }
}