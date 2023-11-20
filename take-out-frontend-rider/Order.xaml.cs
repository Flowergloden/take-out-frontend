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
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace take_out_frontend_rider
{
    public class OrderItem
    {
        public int Id;
        public int OrderId;
        public int DeliveryStatus;
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Order : Page
    {
        public List<OrderItem> Orders;
        public Order()
        {
            this.InitializeComponent();
            Orders = new()
            {
                new()
                {
                    Id = 1,
                    OrderId = 1,
                    DeliveryStatus = 1
                },
                new()
                {
                    Id = 2,
                    OrderId = 2,
                    DeliveryStatus = 2
                },
            };
        }
    }
}
