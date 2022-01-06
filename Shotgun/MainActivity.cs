using Android.App;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Runtime.Remoting.Contexts;

namespace Shotgun
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button bLoad;
        Button bRevolver;
        Button bShot;
        
        ImageView imgBlood;
        TextView lblBullets;
        Revolver revolver = new Revolver();
        SoundPool sounds;
        int soundShot;
        int soundFalse;
        int soundRoll;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            bLoad = FindViewById<Button>(Resource.Id.bLoad);
            bRevolver = FindViewById<Button>(Resource.Id.bRevolver);
            bShot = FindViewById<Button>(Resource.Id.bShot);
            imgBlood = FindViewById<ImageView>(Resource.Id.imgBlood);
            lblBullets = FindViewById<TextView>(Resource.Id.lblBullets);

            CreateSoundPool();
            LoadSounds();

            bLoad.Click += delegate
            {
                imgBlood.Visibility = ViewStates.Visible;
                revolver.TakeBullet();
                lblBullets.Text = revolver.Bullets.ToString();
            };

            bShot.Click += delegate
            {
                if (revolver.Shot())
                {
                    //звук выстрела
                    sounds.Play(soundShot, 1f, 1f, 1, 0, 1);
                    imgBlood.Visibility = ViewStates.Gone;
                }
                else
                {
                    imgBlood.Visibility = ViewStates.Visible;
                    //звук холостого 
                    sounds.Play(soundFalse, 1f, 1f, 1, 0, 1);
                }
                lblBullets.Text = revolver.Bullets.ToString();
            };
            
        }
        
        void CreateSoundPool()
        {
           
            var attributes = new AudioAttributes.Builder()
                .SetUsage(AudioUsageKind.Game)
                .SetContentType(AudioContentType.Sonification)
                .Build();
            sounds = new SoundPool.Builder().SetAudioAttributes(attributes).Build();
        }
        void LoadSounds()
        {
            var mContext = Application.Context;
            soundShot = sounds.Load((string)mContext, Resource.Raw.revolver_shot);
            soundFalse = sounds.Load((string)mContext,Resource.Raw.gun_false);
        }

        
    }
    public class Revolver
    {
        byte[] baraban = new byte[8];               //пустой барабан без патронов
        int position = 0;                           // позиция курка барабана
        int _bullets = 0;
        public int Bullets { get { return _bullets; } }
        public void TakeBullet()
        {
            if (_bullets == 8)
            {
                // полная разрядка барабана револьвера
                for (var i = 0; i < baraban.Length; i++)
                {
                    baraban[i] = 0;

                }
                _bullets = 0;
            }
            int bullet = 1;
            while (bullet > 0 && _bullets < 8)
            {
                for (var i = 0; i < baraban.Length; i++)
                {
                    if (baraban[i] == 0 && Math.Random() > 0.5)
                    {
                        baraban[i] = 1;
                        _bullets++;
                        bullet--;
                    }
                }
            }

        }
        public bool Shot()
        {
            if (baraban[position] == 1)
            {
                _bullets--;
                baraban[position] = 0;
                return true;
            }
            else { return false; }
            position = (position == 7) ? 0 : position + 1;
        }


    }

}