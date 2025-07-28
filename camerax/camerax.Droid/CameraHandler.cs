using Microsoft.Maui.Handlers;

namespace camerax.Droid
{
    public class CameraHandler : ViewHandler<Camera, CameraView>
    {
        public CameraHandler() : base(ViewMapper)
        {

        }

        protected override CameraView CreatePlatformView()
        {
            return new CameraView(Context);
        }
    }
}
