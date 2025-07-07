﻿using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Attributes;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CIImage.Components;

[ComponentInfo(
    "fbe3f24b-a69b-4a1e-bcaf-899ccd773101",
    "图片",
    PackIconKind.Image,
    "在主界面上显示图片。"
)]
public partial class ImageComponent
{
    private string lastImagePath = string.Empty;
    private ILessonsService LessonsService { get; }

    public ImageComponent(ILessonsService lessonsService)
    {
        LessonsService = lessonsService;
        InitializeComponent();
    }

    void LoadPicture(object? sender, PropertyChangedEventArgs e)
    {
        LoadPicture();
    }
    void LoadPicture()
    {
        if (Settings.ImagePath != lastImagePath)
        {
            lastImagePath = Settings.ImagePath;
            BitmapImage bitmap = new BitmapImage();
            try
            {
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Settings.ImagePath);
                bitmap.EndInit();
                ImageViewer.Source = bitmap;

                ErrMsg.Content = "";
                ErrMsg.Visibility = Visibility.Collapsed;
                ImageViewer.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                ErrMsg.Content = "图片不存在";
                ErrMsg.Visibility = Visibility.Visible;
                ImageViewer.Visibility = Visibility.Collapsed;
            }

        }
    }

    private void ComponentBase_Loaded(object sender, RoutedEventArgs e)
    {
        this.BeginInvoke(LoadPicture);
        Settings.PropertyChanged += LoadPicture;
    }

    private void ComponentBase_Unloaded(object sender, RoutedEventArgs e)
    {
        Settings.PropertyChanged -= LoadPicture;
    }
}
