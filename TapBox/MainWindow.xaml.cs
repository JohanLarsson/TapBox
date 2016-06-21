using System.Windows.Controls.Primitives;

namespace TapBox
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnTouchKeyboardClick(object sender, RoutedEventArgs e)
        {
            if (((ToggleButton)sender).IsChecked == true)
            {
                TouchKeyboard.Show();
            }
            else
            {
                TouchKeyboard.Hide();
            }
        }

        private void OnOnScreenKeyboardClick(object sender, RoutedEventArgs e)
        {
            if (((ToggleButton)sender).IsChecked == true)
            {
                OnScreenKeyboard.Show();
            }
            else
            {
                OnScreenKeyboard.Hide();
            }
        }
    }
}
