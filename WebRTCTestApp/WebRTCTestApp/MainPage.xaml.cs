using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Zeyarat.Mobile.Services.Meeting;

namespace WebRTCTestApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;


            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);


            var urlSource = new UrlWebViewSource();
            string baseUrl = DependencyService.Get<IWebViewService>().GetContent();
            urlSource.Url = baseUrl;

            CallWebView.Source = urlSource;
        }


        private async Task CheckPermissionsAndStart()
        {
            var cameraPermissionState = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (cameraPermissionState != PermissionStatus.Granted)
                await Permissions.RequestAsync<Permissions.Camera>();

            var microphonePermissionState = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (microphonePermissionState != PermissionStatus.Granted)
                await Permissions.RequestAsync<Permissions.Microphone>();
        }


        protected override async void OnAppearing()
        {
            await CheckPermissionsAndStart();

            await ConnectToServer();

        }


        private async Task ConnectToServer()
        {
            ChatService chatService = new ChatService();
            await chatService.Connect("Amr");
            await Connect();
        }

        private async Task Connect()
        {
            try
            {
                //await CallWebView.EvaluateJavaScriptAsync($"start();");
                await CallWebView.EvaluateJavaScriptAsync($"init('54321');");
                //callPageViewModel.CurrentUser.ID 12345
                //await CallWebView.EvaluateJavaScriptAsync($"init('54321');");
                //await Task.Delay(1000);
                await CallWebView.EvaluateJavaScriptAsync($"startCall('12345');");
                //callPageViewModel.Friend.ID 54321
            }
            catch (Exception exp)
            {

            }
        }

        private void ToggleMicClick(object sender, EventArgs e)
        {

        }

        private void EndCallClick(object sender, EventArgs e)
        {

        }

        private void ToggelCameraClick(object sender, EventArgs e)
        {

        }
    }
}
