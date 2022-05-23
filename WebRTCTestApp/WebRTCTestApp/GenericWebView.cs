using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WebRTCTestApp
{
    public class GenericWebView : WebView
    {
        Action<string> action;


        public void RegisterAction(Action<string> callback)
        {
            action = callback;
        }

        public void Cleanup()
        {
            action = null;
        }

        public void InvokeAction(string data)
        {
            if (action == null || data == null)
            {
                return;
            }
            action.Invoke(data);
        }
    }
}
