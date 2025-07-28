using Microsoft.Maui.Hosting;

namespace camerax.Droid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseSharedMauiApp()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler(typeof(Camera), typeof(CameraHandler));
                });

            return builder.Build();
        }
    }
}
