using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
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
        private const string GetDir = "rider/getOrder?";
        private const string ParaPage = "page=";
        private const string ParaPageSize = "pageSize=";
        private const string ParaStatus = "status=";
        private const string And = "&";
        private const int PageSize = 10;
        private int Page = 1;

        private const string PutDir = "rider/take/";

        public List<OrderAvailableItem> Items;

        private event EventHandler OnGetData;

        private event EventHandler OnAccept;

        public OrderAvailable()
        {
            GetData(GetDir + ParaPage + Page.ToString()
                    + And + ParaPageSize + PageSize.ToString()
                    + And + ParaStatus + "0");
            OnGetData += (_, _) => { this.InitializeComponent(); };
        }

        private async void GetData(string curl)
        {
            var message = await Profiles.GetClient(curl);
            var json = JsonDocument.Parse(message);
            var root = json.RootElement;
            var data = root.GetProperty("data").GetProperty("records");
            var len = data.GetArrayLength();

            for (int i = 0; i < len; i++)
            {
                Items.Add(new()
                {
                    Address = data[i].GetProperty("address").GetString(),
                    AddressBookId = data[i].GetProperty("addressBookId").GetInt32(),
                    Id = data[i].GetProperty("id").GetInt32(),
                    Number = data[i].GetProperty("number").GetString(),
                    OrderTime = data[i].GetProperty("orderTime").GetString(),
                    UserId = data[i].GetProperty("userId").GetInt32(),
                    UserName = data[i].GetProperty("consignee").GetString(),
                });
            }

            OnGetData?.Invoke(null, EventArgs.Empty);
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (sender as Button) ?? new Button();
            var item = button.DataContext;
            var frame = MainWindow.Instance.MainContextFrame;

            AcceptPut((item as OrderAvailableItem)?.Id ?? 114514);

            OnAccept += (_, _) =>
            {
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
            };
        }

        private async void AcceptPut(int id)
        {
            if (id == 114514) return;
            var message = await Profiles.PutClient(PutDir + id.ToString());

            var json = JsonDocument.Parse(message);
            var root = json.RootElement;
            var code = root.GetProperty("code").GetInt32();
            if (code == 1)
            {
                OnAccept?.Invoke(null, EventArgs.Empty);
            }
            else
            {
                Console.WriteLine(root.GetProperty("message").GetString());
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