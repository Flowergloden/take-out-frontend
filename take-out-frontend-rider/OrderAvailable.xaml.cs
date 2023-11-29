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

        private const string PutDir = "rider/take";

        public List<OrderAvailableItem> Items;

        private event EventHandler OnGetData;

        private event EventHandler OnAccpet;

        public OrderAvailable()
        {
            GetData(GetDir + ParaPage + Page.ToString()
                    + And + ParaPageSize + PageSize.ToString()
                    + And + ParaStatus + "0");
            OnGetData += (_, _) => { this.InitializeComponent(); };

            // TEST ONLY
            Items = new List<OrderAvailableItem>
            {
                new()
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
                    Address = data.GetProperty("address").GetString(),
                    AddressBookId = data.GetProperty("addressBookId").GetInt32(),
                    Id = data.GetProperty("id").GetInt32(),
                    Number = data.GetProperty("number").GetString(),
                    OrderTime = data.GetProperty("orderTime").GetString(),
                    UserId = data.GetProperty("userId").GetInt32(),
                    UserName = data.GetProperty("userName").GetString(),
                });
            }

            OnGetData?.Invoke(null, EventArgs.Empty);
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (sender as Button) ?? new Button();
            var item = button.DataContext;
            var frame = MainWindow.Instance.MainContextFrame;

            AcceptPut((item as OrderAvailableItem)?.Id ?? 0);

            OnAccpet += (_, _) =>
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
            var content = JsonContent.Create(new
            {
                id,
            });

            var message = await Profiles.PutClient(PutDir, content);
            var json = JsonDocument.Parse(message);
            var root = json.RootElement;

            // TODO: check if success

            OnAccpet?.Invoke(null, EventArgs.Empty);
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