﻿using EverythingToolbar.Helpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EverythingToolbar
{
    public partial class SearchButton : Button
    {
        public SearchButton()
        {
            InitializeComponent();

            ResourceManager.Instance.ResourceChanged += UpdateTheme;
            SearchWindow.Instance.Activated += OnSearchWindowActivated;
            SearchWindow.Instance.Deactivated += OnSearchWindowDeactivated;
        }

        private void OnSearchWindowDeactivated(object sender, System.EventArgs e)
        {
            Border border = Template.FindName("OuterBorder", this) as Border;
            border.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void OnSearchWindowActivated(object sender, System.EventArgs e)
        {
            Border border = Template.FindName("OuterBorder", this) as Border;
            border.Background = new SolidColorBrush(Color.FromArgb(64, 255, 255, 255));
        }

        private void UpdateTheme(Theme newTheme)
        {
            Border border = Template.FindName("OuterBorder", this) as Border;
            if (newTheme == Theme.Light)
            {
                Foreground = new SolidColorBrush(Colors.Black);
                border.Opacity = 0.55;
            }
            else
            {
                Foreground = new SolidColorBrush(Colors.White);
                border.Opacity = 0.2;
            }
        }

        private void UpdateTheme(object sender, ResourcesChangedEventArgs e)
        {
            if (IsLoaded)
                UpdateTheme(e.NewTheme);
            else
                Loaded += (s, e_) => { UpdateTheme(e.NewTheme); };
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (SearchWindow.Instance.IsOpen)
                SearchWindow.Instance.Hide();
            else
                SearchWindow.Instance.Show();
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TaskbarStateManager.Instance.IsIcon = (bool)e.NewValue;
        }
    }
}
