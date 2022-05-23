using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using WebKit;
using WebRTCTestApp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using WebRTCTestApp.iOS.CustomRender;

[assembly: ExportRenderer(typeof(GenericWebView), typeof(MyWebViewRenderer))]
namespace WebRTCTestApp.iOS.CustomRender
{
    public class MyWebViewRenderer : ViewRenderer<GenericWebView, WKWebView>
    {
        WKWebView wkWebView;
        protected override void OnElementChanged(ElementChangedEventArgs<GenericWebView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                WKWebViewConfiguration configuration = new WKWebViewConfiguration();
                configuration.AllowsInlineMediaPlayback = true;
                wkWebView = new WKWebView(new CGRect(0, 0, 300, 400), configuration);

                //string filename = Path.Combine(NSBundle.MainBundle.BundlePath, $"/Html/call.html");
                //new NSUrl(filename, false);
                wkWebView.LoadRequest(new NSUrlRequest());
                SetNativeControl(wkWebView);
            }
        }
    }
}