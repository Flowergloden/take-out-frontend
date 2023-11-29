using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Threading;
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
    public sealed partial class Status : Page
    {
        public event EventHandler StatusChanged;

        private const string StatusDir = "rider/info/1";
        public string User;
        public string Phone;

        public Status()
        {
            NavigationCacheMode = NavigationCacheMode.Required;
            GetStatus();
            StatusChanged += (_, _) => { this.InitializeComponent(); };
        }

        private async void GetStatus()
        {
            var info = await Profiles.GetClient(StatusDir);

            var jsonInfo = JsonDocument.Parse(info);
            var root = jsonInfo.RootElement;
            var dataElement = root.GetProperty("data");
            User = dataElement.GetProperty("name").GetString();
            Phone = dataElement.GetProperty("phone").GetString();
            Console.WriteLine($"inner: {User}");
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}