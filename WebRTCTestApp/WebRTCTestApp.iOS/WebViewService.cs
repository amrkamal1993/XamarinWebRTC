using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using WebRTCTestApp.iOS.Services;

[assembly: Dependency(typeof(WebViewService))]
namespace WebRTCTestApp.iOS.Services
{
    public class WebViewService : IWebViewService
    {
        public string GetContent()
        {
            return NSBundle.MainBundle.BundlePath + "/Html/call.html";
        }
    }
}