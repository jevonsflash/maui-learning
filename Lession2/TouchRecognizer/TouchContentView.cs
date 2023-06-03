﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lession2.TouchRecognizer
{
    public class TouchContentView : ContentView
    {
        private TouchRecognizer touchRecognizer;

        public event EventHandler<TouchActionEventArgs> OnTouchActionInvoked;



        public TouchContentView()
        {
            this.HandlerChanged+=TouchContentView_HandlerChanged;
            this.HandlerChanging+=TouchContentView_HandlerChanging;
        }


        private void TouchContentView_HandlerChanged(object sender, EventArgs e)
        {

            var handler = this.Handler;
            if (handler != null)
            {
#if WINDOWS
                touchRecognizer = new TouchRecognizer(handler.PlatformView as Microsoft.UI.Xaml.FrameworkElement);
                touchRecognizer.OnTouchActionInvoked += TouchRecognizer_OnTouchActionInvoked;
#endif
#if ANDROID
                touchRecognizer = new TouchRecognizer(handler.PlatformView as Android.Views.View);
                touchRecognizer.OnTouchActionInvoked += TouchRecognizer_OnTouchActionInvoked;

#endif

#if IOS|| MACCATALYST
                    touchRecognizer = new TouchRecognizer(handler.PlatformView as UIKit.UIView);
                    touchRecognizer.OnTouchActionInvoked += TouchRecognizer_OnTouchActionInvoked;

                    (handler.PlatformView as UIKit.UIView).UserInteractionEnabled = true;
                    (handler.PlatformView as UIKit.UIView).AddGestureRecognizer(touchRecognizer);
#endif
            }

        }

        private void TouchContentView_HandlerChanging(object sender, HandlerChangingEventArgs e)
        {


            if (e.OldHandler != null)
            {
                var handler = e.OldHandler;

#if WINDOWS
                touchRecognizer.OnTouchActionInvoked -= TouchRecognizer_OnTouchActionInvoked;
#endif
#if ANDROID
                touchRecognizer.OnTouchActionInvoked -= TouchRecognizer_OnTouchActionInvoked;

#endif

#if IOS|| MACCATALYST
                touchRecognizer.OnTouchActionInvoked -= TouchRecognizer_OnTouchActionInvoked;

                (handler.PlatformView as UIKit.UIView).UserInteractionEnabled = false;
                (handler.PlatformView as UIKit.UIView).RemoveGestureRecognizer(touchRecognizer);
#endif


            }
        }

        private void TouchRecognizer_OnTouchActionInvoked(object sender, TouchActionEventArgs e)
        {
            OnTouchActionInvoked?.Invoke(this, e);
            Debug.WriteLine(e.Type + " is Invoked, position:" + e.Location);
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {

        }
    }
}
