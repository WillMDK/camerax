using System;
using System.Globalization;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Camera.Core;
using AndroidX.Camera.View;
using AndroidX.Lifecycle;
using Object = Java.Lang.Object;

namespace camerax.Droid
{
    public class CameraView : FrameLayout
    {
        private Zoom _zoom;

        public CameraView(Context context) : base(context)
        {
            StartCamera();
        }

        private void StartCamera()
        {
            Inflate(Context, Resource.Layout.Camera, this);
            var surfaceContainer = FindViewById<RelativeLayout>(Resource.Id.surface_container);

            var preview = new PreviewView(Context);
            preview.LayoutParameters = new RelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            preview.SetScaleType(PreviewView.ScaleType.FitCenter);

            surfaceContainer.AddView(preview, 0);

            var cameraController = new LifecycleCameraController(Context);
            _zoom = new Zoom();

            if (Context is AndroidX.Lifecycle.ILifecycleOwner lifecycleOwner)
            {
                cameraController.ZoomState.Observe(lifecycleOwner, _zoom);
                cameraController.BindToLifecycle(lifecycleOwner);
            }
            else if (Microsoft.Maui.ApplicationModel.Platform.CurrentActivity is AndroidX.Lifecycle.ILifecycleOwner maLifecycleOwner)
            {
                cameraController.ZoomState.Observe(maLifecycleOwner, _zoom);
                cameraController.BindToLifecycle(maLifecycleOwner);
            }

            cameraController.CameraSelector = CameraSelector.DefaultBackCamera;

            preview.Controller = cameraController;
        }

        internal class Zoom() : Java.Lang.Object, IObserver
        {
            public void OnChanged(Object value)
            {
                try
                {
                    var zoom = ((IZoomState)value).ZoomRatio;
                    Log.Info("ZOOM", $"zoom value is {zoom}");
                }
                catch (Exception e)
                {
                    Log.Error("ZOOM", $"Could not get zoom value: value type is {value?.GetType()}, value is zoomstate {value is IZoomState}");
                    Log.Error("ZOOM", $"Error from trying to cast value: {e}");
                    throw;
                }
            }
        }
    }
}
