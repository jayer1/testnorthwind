using Android.App;
using Android.Content.PM;
using Android.OS;
using Lottie.Forms.Droid;
using Plugin.MediaManager.Forms.Android;

namespace FinalProject.Droid
{
    [Activity(Label = "FinalProject", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            VideoViewRenderer.Init();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            AnimationViewRenderer.Init();
            
            LoadApplication(new App());
            
        }

    }
}