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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderAvailable : Page
    {
        public List<OrderAvailableItem> Items;
        public OrderAvailable()
        {
            this.InitializeComponent();

            Items = new List<OrderAvailableItem>
            {
                new()
                {
                    Id = "1", Number = "1", UserId = "1", UserName = "1", AddressBookId = "1", Address = "1",
                    OrderTime = "1"
                }
            };
        }

        private void ContentGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class OrderAvailableItem
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string AddressBookId { get; set; }
        public string Address { get; set; }
        public string OrderTime { get; set; }
    }
}