using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Content;
using Gcm.Client;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.WindowsAzure.MobileServices;

namespace Pharmacy.Droid
{
    [Activity(Label = "@string/app_name",
              Icon = "@mipmap/icon",
              LaunchMode = Android.Content.PM.LaunchMode.SingleTop,
              ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class activity_main : BaseActivity
    {
        TabFragmentPagerAdapter PagerAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //push notifications
            activity_main.instance = this;
            this.RegisterGCM();

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.main_toolbar);
            //Toolbar will now take on default Action Bar characteristics
            SetSupportActionBar(toolbar);

            setupViewPager();
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_settings, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_settings:

                    Toast.MakeText(this, "Settings selected", ToastLength.Short).Show();

                    //FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction ();
                    //SettingsFragment settings = new SettingsFragment ();
                    //fragmentTx.Add (Resource.Id.fragment_container, settings);
                    //fragmentTx.AddToBackStack (null);
                    //fragmentTx.Commit ();

                    break;

                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        void setupViewPager()
        {
            PagerAdapter = new TabFragmentPagerAdapter(this, SupportFragmentManager);
            PagerAdapter.AddFragment(new fragment_pharmacies(this));
            PagerAdapter.AddFragment(new fragment_assistant());

            var viewPager = FindViewById<ViewPager>(Resource.Id.main_viewPager);
            viewPager.Adapter = PagerAdapter;

            var tabLayout = FindViewById<TabLayout>(Resource.Id.main_tabLayout);
            tabLayout.TabMode = TabLayout.ModeFixed;
            tabLayout.TabGravity = TabLayout.GravityFill;
            tabLayout.SetupWithViewPager(viewPager);

            PagerAdapter.FillTabLayout(tabLayout);

            viewPager.PageSelected += (sender, e) =>
            {
                if (e.Position == 0)
                    Analytics.TrackEvent("View: Pharmacies");

                if (e.Position == 1)
                    Analytics.TrackEvent("View: Assistant");

                //Tier = (PartnerTiers)e.Position;

                ////update the query listener
                //var fragment = PagerAdapter.GetFragmentAtPosition (e.Position);
                //queryListener = (SearchView.IOnQueryTextListener)fragment;

                //searchView?.SetOnQueryTextListener (queryListener);

                //UpdateColors (Tier);
            };
        }

        #region push notifications

        public void RegisterGCM()
        {
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);
            GcmClient.Register(this, CustomBroadcastReceiver.senderIDs);
        }

        // Create a new instance field for this activity.
        static activity_main instance = new activity_main();

        // Return the current activity instance.
        public static activity_main CurrentActivity
        {
            get
            {
                return instance;
            }
        }

        static MobileServiceClient client = new MobileServiceClient(Settings.ApplicationUrl);

        // Return the Mobile Services client.
        public MobileServiceClient CurrentClient
        {
            get
            {
                return client;
            }
        }

        #endregion push notifications
    }
}