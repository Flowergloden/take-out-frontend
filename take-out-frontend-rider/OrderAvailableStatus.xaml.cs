using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class OrderAvailableStatus : Page
    {
        private const string Dir = "rider/arrive/";

        private OrderAvailableItem _item;

        private event EventHandler OnConfirm;

        public OrderAvailableStatus()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var item = e.Parameter as OrderAvailableItem;
            item ??= new OrderAvailableItem();

            _item = item;
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            ConfirmPut(_item.Id);

            OnConfirm += (_, _) =>
            {
                Profiles.HasOrder = false;
                Profiles.OrderNow = null;
                var frame = MainWindow.Instance.MainContextFrame;
                frame.Navigate(typeof(OrderAvailable), null, new DrillInNavigationTransitionInfo());
                // frame.BackStack.RemoveAt(frame.BackStack.Count - 1);
                for (int i = 0; i < frame.BackStack.Count; i++)
                {
                    if (frame.BackStack[i].SourcePageType == typeof(OrderAvailableStatus))
                    {
                        frame.BackStack.RemoveAt(i);
                        --i;
                    }
                }
            };
        }

        private async void ConfirmPut(int id)
        {
            var message = await Profiles.PutClient(Dir + id.ToString());
            var json = JsonDocument.Parse(message);
            var root = json.RootElement;

            // TODO: check if success

            OnConfirm?.Invoke(this, EventArgs.Empty);
        }
    }
}