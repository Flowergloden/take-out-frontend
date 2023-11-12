using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace take_out_frontend_rider
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            ContextFrame.Navigate(typeof(OrderAvailable));
        }

        private void Switch_OrderAvailable(object sender, RoutedEventArgs e)
        {
            ContextFrame.Navigate(typeof(OrderAvailable), null, new EntranceNavigationTransitionInfo());
        }

        private void Switch_Order(object sender, RoutedEventArgs e)
        {
            ContextFrame.Navigate(typeof(Order), null, new EntranceNavigationTransitionInfo());
        }

        private void Switch_status(object sender, RoutedEventArgs e)
        {
            ContextFrame.Navigate(typeof(Status), null, new EntranceNavigationTransitionInfo());
        }
    }
}
