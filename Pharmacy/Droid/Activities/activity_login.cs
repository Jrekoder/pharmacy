using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.WindowsAzure.MobileServices;

namespace Pharmacy.Droid
{
    [Activity(Label = "@string/app_name",
              MainLauncher = true, Icon = "@mipmap/icon",
              LaunchMode = Android.Content.PM.LaunchMode.SingleTop, NoHistory = true,
              ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class activity_login : Activity
    {
        private LinearLayout splash_container = null;
        private LinearLayout login_container = null;
        private ImageView app_logo = null;
        private ImageView logo_reference = null;
        private Button login = null;

        // Client reference.
        private MobileServiceClient client;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);

            // Init Mobile Center Analytics and Crashes
            MobileCenter.Start(Constants.MobileCenterAppID, typeof(Analytics), typeof(Crashes));

            // Create the client instance, using the mobile app backend URL.
            client = new MobileServiceClient(Constants.ApplicationURL);

            // Create your application here
            SetContentView(Resource.Layout.activity_login);

            LoadViews();
            InitTransition();
        }

        protected override void OnResume()
        {
            base.OnResume();
            login.Click += Login_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            login.Click -= Login_Click;
        }

        private void Login_Click(object sender, EventArgs e)
        {
            StartMainActivity();
        }

        private void InitTransition()
        {
            Task.Delay(1000).ContinueWith((b) =>
            {
                RunOnUiThread(() =>
                {
                    ExecuteTransition();
                });
            });
        }

        private void LoadViews()
        {
            splash_container = FindViewById<LinearLayout>(Resource.Id.splash_container);
            login_container = FindViewById<LinearLayout>(Resource.Id.login_container);
            app_logo = FindViewById<ImageView>(Resource.Id.app_logo);
            logo_reference = FindViewById<ImageView>(Resource.Id.logo_reference);
            login = FindViewById<Button>(Resource.Id.login);
        }

        private void ExecuteTransition()
        {
            float toYLogo = logo_reference.GetY();
            float fromYLogo = app_logo.GetY();
            toYLogo = fromYLogo - toYLogo;
            TranslateAnimation tanim = new TranslateAnimation(0, 0, 0, -(toYLogo));
            tanim.FillAfter = true;
            tanim.Duration = 1000;
            Animation translateIn = AnimationUtils.LoadAnimation(this, Resource.Animation.translate_in);
            Animation translateOut = AnimationUtils.LoadAnimation(this, Resource.Animation.translate_out);
            app_logo.StartAnimation(tanim);
            login_container.StartAnimation(translateIn);
            splash_container.StartAnimation(translateOut);
            splash_container.Visibility = Android.Views.ViewStates.Gone;
            logo_reference.Visibility = Android.Views.ViewStates.Gone;
            login_container.Visibility = Android.Views.ViewStates.Visible;
        }

        private void StartMainActivity()
        {
            Intent intent = new Intent(this, typeof(activity_main));
            intent.SetFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        // Define a authenticated user.
        private MobileServiceUser user;
        private async Task<bool> Authenticate()
        {
            var success = false;
            try
            {
                user = await client.LoginAsync(this, MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory, "schema");
                CreateAndShowDialog(string.Format("you are now logged in - {0}", user.UserId), "Logged in!");

                success = true;
            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex, "Authentication failed");
            }
            return success;
        }

        private void CreateAndShowDialog(Exception exception, String title)
        {
            CreateAndShowDialog(exception.Message, title);
        }

        private void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}