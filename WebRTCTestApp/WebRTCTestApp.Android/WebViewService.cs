using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebRTCTestApp;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebRTCTestApp.Droid.Service.WebViewService))]
namespace WebRTCTestApp.Droid.Service
{
    public class WebViewService : IWebViewService
    {
        public string GetContent()
        {
            return "file:///android_asset/Html/call.html";
        }
    }
}