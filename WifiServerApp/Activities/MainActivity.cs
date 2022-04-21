using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using WifiServerApp.Activities;

namespace WifiServerApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btnCreate, btnUpdate, btnDelete;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnCreate = FindViewById<Button>(Resource.Id.btnCreate);
            btnUpdate = FindViewById<Button>(Resource.Id.btnUpdate);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            btnCreate.Click += delegate { StartActivity(typeof(CreateLocation)); };
            btnUpdate.Click += delegate { StartActivity(typeof(UpdateLocation)); };
            btnDelete.Click += delegate { StartActivity(typeof(DeleteLocation)); };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}