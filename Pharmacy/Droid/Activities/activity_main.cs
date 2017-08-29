using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Gcm.Client;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.WindowsAzure.MobileServices;

namespace Pharmacy.Droid
{
    [Activity(Label = "@string/app_name",
              Icon = "@mipmap/icon", Theme = "@style/Theme.AppCompat.Custom",
              LaunchMode = Android.Content.PM.LaunchMode.SingleTop,
              ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class activity_main : AppCompatActivity
    {
        private AppSectionsPagerAdapter mAppSectionsPagerAdapter = null;
        private ViewPager mViewPager = null;
        private List<Android.Support.V4.App.Fragment> mFragmentList = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			//push notifications
			activity_main.instance = this;
            this.RegisterGCM();

            // Create your application here
            SetContentView(Resource.Layout.activity_main);

            mFragmentList = new List<Android.Support.V4.App.Fragment>();
            mFragmentList.Add(new fragment_pharmacies (this));
            mFragmentList.Add(new fragment_products (this));
            mFragmentList.Add(new fragment_mycard ());
            mFragmentList.Add(new fragment_assistant ());

            mAppSectionsPagerAdapter = new AppSectionsPagerAdapter(SupportFragmentManager, mFragmentList, this);
            mViewPager = FindViewById<ViewPager>(Resource.Id.pager);
            mViewPager.Adapter = mAppSectionsPagerAdapter;
            mViewPager.AddOnPageChangeListener (new CustomAddOnPageChangeListener());
            mViewPager.SetCurrentItem(1, false);
        }

        public class CustomAddOnPageChangeListener : Java.Lang.Object, ViewPager.IOnPageChangeListener
        {
            public CustomAddOnPageChangeListener()
            {
            }

            public void OnPageScrolled (int position, float positionOffset, int positionOffsetPixels)
            {
                //throw new NotImplementedException ();
            }

            public void OnPageScrollStateChanged (int state)
            {
                //throw new NotImplementedException ();
            }

            public void OnPageSelected (int position)
            {
                System.Diagnostics.Debug.WriteLine ("we are here: " + position);
                Analytics.TrackEvent (activity_main.GetTabText (position).ToString ());
            }
        }

        public static Java.Lang.String GetTabText(int position)
        {
			Java.Lang.String result = null;
			switch (position)
			{
				case 0:
					result = new Java.Lang.String ("Products");
					break;
				case 1:
					result = new Java.Lang.String ("Pharmacies");
					break;
				case 2:
					result = new Java.Lang.String ("My Card");
					break;
				case 3:
					result = new Java.Lang.String ("Assistant");
					break;
				default:
					result = new Java.Lang.String ("");
					break;
			}
            return result;
        }

        public class AppSectionsPagerAdapter : FragmentPagerAdapter
        {
            private List<Android.Support.V4.App.Fragment> mFragmentList = null;
            private activity_main mActivity = null;

            public AppSectionsPagerAdapter(Android.Support.V4.App.FragmentManager fm, List<Android.Support.V4.App.Fragment> mFragmentList, activity_main activity)
            : base(fm)
            {
                this.mFragmentList = mFragmentList;
                this.mActivity = activity;
            }

            public override int Count { get { return mFragmentList.Count; } }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                Android.Support.V4.App.Fragment fragment = null;
                switch (position)
                {
                    case 0:
                        fragment = fragment_products.NewInstance(mActivity);
                        break;
                    case 1:
                        fragment = fragment_pharmacies.NewInstance(mActivity);
                        break;
                    case 2:
                        fragment = fragment_mycard.NewInstance();
                        break;
                    case 3:
                        fragment = fragment_assistant.NewInstance();
                        break;
                    default:
                        fragment = fragment_pharmacies.NewInstance(mActivity);
                        break;
                }

                return fragment;
            }

            public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
            {
                return activity_main.GetTabText (position);
            }
        }

		#region push notifications

        public void RegisterGCM ()
        {
			GcmClient.CheckDevice (this);
			GcmClient.CheckManifest (this);
			GcmClient.Register (this, CustomBroadcastReceiver.senderIDs);
        }

		// Create a new instance field for this activity.
		static activity_main instance = new activity_main ();

		// Return the current activity instance.
		public static activity_main CurrentActivity
		{
			get
			{
				return instance;
			}
		}

        static MobileServiceClient client = new MobileServiceClient (Constants.ApplicationURL);

		// Return the Mobile Services client.
		public MobileServiceClient CurrentClient
		{
			get
			{
				return client;
			}
		}

        #endregion

    }
}
