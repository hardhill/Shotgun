using Android.App;
using Android.OS;
using Android.Support.V7.App;

using Android.Widget;
using Java.Interop;

namespace Shotgun
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button bLoad;
        Button bRevolver;
        Button bShot;
        ImageView imgRevolver;
        ImageView imgBlood;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            bLoad = FindViewById<Button>(Resource.Id.bLoad);
            bRevolver = FindViewById<Button>(Resource.Id.bRevolver);
            bShot = FindViewById<Button>(Resource.Id.bShot);
            imgBlood = FindViewById<ImageView>(Resource.Id.imgBlood);
        }
    }
    public class Revolver
    {
        byte[] baraban = new byte[8];               //пустой барабан без патронов
        int position = 0;                           // позиция курка барабана

    }

}