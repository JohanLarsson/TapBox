namespace TapBox
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class Touch
    {
        static Touch()
        {
            EventManager.RegisterClassHandler(
                typeof(TextBox),
                UIElement.TouchEnterEvent,
                new RoutedEventHandler(OnTouchEnter));

            EventManager.RegisterClassHandler(
                typeof(TextBox),
                UIElement.LostFocusEvent,
                new RoutedEventHandler(OnLostFocus));
            var mainWindow = Application.Current?.MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Closed += OnClosed;
            }
        }

        public static readonly DependencyProperty ShowTouchKeyboardOnTouchDownProperty = DependencyProperty.RegisterAttached(
            "ShowTouchKeyboardOnTouchDown",
            typeof(bool),
            typeof(Touch),
            new FrameworkPropertyMetadata(
                default(bool),
                FrameworkPropertyMetadataOptions.Inherits));

        public static void SetShowTouchKeyboardOnTouchDown(UIElement element, bool value)
        {
            element.SetValue(ShowTouchKeyboardOnTouchDownProperty, value);
        }

        public static bool GetShowTouchKeyboardOnTouchDown(UIElement element)
        {
            return (bool)element.GetValue(ShowTouchKeyboardOnTouchDownProperty);
        }

        private static void OnTouchEnter(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (GetShowTouchKeyboardOnTouchDown(textBox))
            {
                TouchKeyboard.Show();
            }
        }

        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = Keyboard.FocusedElement as TextBox;
            if (textBox == null || !GetShowTouchKeyboardOnTouchDown(textBox))
            {
                TouchKeyboard.Hide();
            }
        }

        private static void OnClosed(object sender, EventArgs eventArgs)
        {
            ((Window)sender).Closed -= OnClosed;
            TouchKeyboard.Hide();
        }
    }
}