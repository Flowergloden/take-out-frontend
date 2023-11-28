using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace take_out_frontend_rider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderAvailable : Page
    {
        public List<OrderAvailableItem> Items;

        public OrderAvailable()
        {
            this.InitializeComponent();

            // TEST ONLY
            Items = new List<OrderAvailableItem>
            {
                new ()
                {
                    Id = 1,
                    Number = "1",
                    UserId = 1,
                    UserName = "张三",
                    AddressBookId = 1,
                    Address = "广东省广州市天河区华南理工大学",
                    OrderTime = "2021-06-01 12:00:00"
                }
            };
        }

        private async void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            // TEST ONLY
            await Profiles.GetClient("rider/info/1");

            var button = sender as Button;
            var item = button?.DataContext;
            var frame = MainWindow.Instance.MainContextFrame;
            Profiles.HasOrder = true;
            Profiles.OrderNow = item;
            frame.Navigate(typeof(OrderAvailableStatus), item, new DrillInNavigationTransitionInfo());
            for (int i = 0; i < frame.BackStack.Count; i++)
            {
                if (frame.BackStack[i].SourcePageType == typeof(OrderAvailable))
                {
                    frame.BackStack.RemoveAt(i);
                    --i;
                }
            }
        }
    }

    public class OrderAvailableItem
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int AddressBookId { get; set; }
        public string Address { get; set; }
        public string OrderTime { get; set; }
    }
}