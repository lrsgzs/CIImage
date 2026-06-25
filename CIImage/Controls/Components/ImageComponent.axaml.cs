using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;
using ImageComponentSettings = CIImage.Models.ComponentSettings.ImageComponentSettings;
using SkiaSharp;

namespace CIImage.Controls.Components;

[ComponentInfo(
    "fbe3f24b-a69b-4a1e-bcaf-899ccd773101",
    "图片",
    "",
    "在主界面上显示图片。"
)]
public partial class ImageComponent : ComponentBase<ImageComponentSettings>
{
    private DispatcherTimer? _gifTimer;
    private List<Bitmap>? _gifFrames;
    private int[]? _gifFrameDelaysMs;
    private int _currentFrameIndex;
    private string _lastImagePath = string.Empty;

    public ImageComponent()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        Settings.PropertyChanged += OnSettingsChanged;
        LoadImage(Settings.ImagePath);
    }

    private void OnUnloaded(object? sender, RoutedEventArgs e)
    {
        Settings.PropertyChanged -= OnSettingsChanged;
        StopGifAnimation();
    }

    private void OnSettingsChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ImageComponentSettings.ImagePath):
                LoadImage(Settings.ImagePath);
                break;
            case nameof(ImageComponentSettings.IsAnimating):
                ApplyAnimationState();
                break;
        }
    }

    private void LoadImage(string path)
    {
        if (path == _lastImagePath) return;
        _lastImagePath = path;

        StopGifAnimation();

        if (string.IsNullOrEmpty(path))
        {
            ImageViewer.Source = null;
            return;
        }

        if (IsGifFile(path))
        {
            LoadAnimatedGif(path);
        }
        else
        {
            try
            {
                ImageViewer.Source = new Bitmap(path);
            }
            catch
            {
                ImageViewer.Source = null;
            }
        }
    }

    private static bool IsGifFile(string path)
    {
        return System.IO.Path.GetExtension(path).Equals(".gif", StringComparison.OrdinalIgnoreCase);
    }

    private void LoadAnimatedGif(string path)
    {
        try
        {
            int frameCount;
            SKCodecFrameInfo[] frameInfos;
            SKImageInfo info;

            using (var stream = new SKFileStream(path))
            using (var codec = SKCodec.Create(stream))
            {
                frameCount = codec.FrameCount;
                if (frameCount <= 1) return; // Not animated, fall through to static load

                frameInfos = codec.FrameInfo;
                info = codec.Info;
            }

            var frames = new List<Bitmap>(frameCount);
            var delaysMs = new int[frameCount];

            // Decode all frames sequentially with a single codec to handle
            // frame compositing (disposal methods, transparency) correctly
            using (var stream = new SKFileStream(path))
            using (var codec = SKCodec.Create(stream))
            {
                for (int i = 0; i < frameCount; i++)
                {
                    var opts = new SKCodecOptions(i);
                    using var skBitmap = new SKBitmap(info.Width, info.Height);
                    var result = codec.GetPixels(info, skBitmap.GetPixels(), opts);

                    if (result != SKCodecResult.Success) continue;

                    // Convert SKBitmap to Avalonia Bitmap via PNG encoding
                    using var skImage = SKImage.FromBitmap(skBitmap);
                    using var data = skImage.Encode(SKEncodedImageFormat.Png, 100);
                    var bytes = data.ToArray();
                    using var ms = new System.IO.MemoryStream(bytes);
                    frames.Add(new Bitmap(ms));

                    // Ensure minimum frame delay of 20ms (50fps max)
                    delaysMs[i] = Math.Max(frameInfos[i].Duration, 20);
                }
            }

            if (frames.Count == 0) return;

            _gifFrames = frames;
            _gifFrameDelaysMs = delaysMs;
            _currentFrameIndex = 0;

            ImageViewer.Source = _gifFrames[0];

            if (Settings.IsAnimating)
            {
                StartGifTimer();
            }
        }
        catch
        {
            // If GIF decoding fails, try loading as a static image
            try { ImageViewer.Source = new Bitmap(path); } catch { ImageViewer.Source = null; }
        }
    }

    private void OnGifTimerTick(object? sender, EventArgs e)
    {
        if (_gifFrames == null || _gifFrameDelaysMs == null || _gifFrames.Count == 0) return;

        _currentFrameIndex = (_currentFrameIndex + 1) % _gifFrames.Count;

        ImageViewer.Source = _gifFrames[_currentFrameIndex];

        if (_gifTimer != null)
        {
            _gifTimer.Interval = TimeSpan.FromMilliseconds(_gifFrameDelaysMs[_currentFrameIndex]);
        }
    }

    private void StartGifTimer()
    {
        if (_gifTimer != null || _gifFrameDelaysMs == null) return;

        _gifTimer = new DispatcherTimer(
            TimeSpan.FromMilliseconds(_gifFrameDelaysMs[_currentFrameIndex]),
            DispatcherPriority.Normal,
            OnGifTimerTick);
        _gifTimer.Start();
    }

    private void StopGifTimer()
    {
        if (_gifTimer == null) return;

        _gifTimer.Stop();
        _gifTimer.Tick -= OnGifTimerTick;
        _gifTimer = null;
    }

    private void ApplyAnimationState()
    {
        if (_gifFrames == null) return; // No GIF loaded

        if (Settings.IsAnimating)
        {
            if (_gifTimer == null)
                StartGifTimer();
        }
        else
        {
            StopGifTimer();
        }
    }

    private void StopGifAnimation()
    {
        StopGifTimer();

        if (_gifFrames != null)
        {
            foreach (var frame in _gifFrames)
                frame.Dispose();
            _gifFrames = null;
        }

        _gifFrameDelaysMs = null;
        _currentFrameIndex = 0;
    }
}
