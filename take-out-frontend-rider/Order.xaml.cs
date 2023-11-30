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
using System.Text.Json;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace take_out_frontend_rider
{
    public class OrderItem
    {
        public long Id;
        public int DeliveryStatus;
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Order : Page
    {
        public List<OrderItem> Orders;

        private event EventHandler OnGetData;

        private const string Dir = "rider/history?";
        private const string Para = "riderId=";

        public Order()
        {
            GetOrders(Dir + Para + "1");
            OnGetData += (_, _) => { this.InitializeComponent(); };
        }

        private async void GetOrders(string curl)
        {
            var message = await Profiles.GetClient(curl);

            var json = JsonDocument.Parse(message);
            var root = json.RootElement;
            var data = root.GetProperty("data");
            var len = data.GetArrayLength();

            for (int i = 0; i < len; i++)
            {
                Orders.Add(new()
                {
                    DeliveryStatus = data[i].GetProperty("status").GetInt32(),
                    Id = data[i].GetProperty("id").GetInt64(),
                });
            }

            OnGetData?.Invoke(this, EventArgs.Empty);
        }
    }
}