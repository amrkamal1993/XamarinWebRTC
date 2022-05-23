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

[assembly: ExportRenderer(typeof(GenericWebView), typeof(HybridWebViewRenderer))]
namespace WebRTCTestApp.iOS.CustomRender
{
    public class HybridWebViewRenderer : WkWebViewRenderer
    {

        WKUserContentController userController;
        static WKWebViewConfiguration config = new WKWebViewConfiguration()
        {
            AllowsAirPlayForMediaPlayback = false,
            MediaPlaybackRequiresUserAction = false,

            AllowsInlineMediaPlayback = true,
            Preferences = new WKPreferences()
            {
                JavaScriptCanOpenWindowsAutomatically = true,
                JavaScriptEnabled = true
            }
        };

        public HybridWebViewRenderer() : this(config)
        {
        }

        public HybridWebViewRenderer(WKWebViewConfiguration config) : base(config)
        {
            userController = config.UserContentController;

        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                GenericWebView hybridWebView = e.OldElement as GenericWebView;
                hybridWebView.Cleanup();
            }

            if (e.NewElement != null)
            {
                BackgroundColor = UIColor.Clear;
                Opaque = false;

            }
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            ((GenericWebView)Element).InvokeAction(message.Body.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((GenericWebView)Element).Cleanup();
            }
            base.Dispose(disposing);
        }

    }
}