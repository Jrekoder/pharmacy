using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Microsoft.Azure.Mobile.Analytics;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Pharmacy.Droid
{
    [Activity(Label = "@string/pharmacy_title",
              Icon = "@mipmap/icon",
			  LaunchMode = Android.Content.PM.LaunchMode.SingleTop, ParentActivity = typeof(activity_main),
			  ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class activity_pharmacy : AppCompatActivity
    {
        private TextView pharmacy_title, pharmacy_subtitle, pharmacy_state, pharmacy_city, pharmacy_address = null;
        private Pharmacy pharmacy = null;
        private MapView mMapView = null;
        private Button search = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_pharmacy);

            search = FindViewById<Button> (Resource.Id.search_products);

            var toolbar = FindViewById<Toolbar> (Resource.Id.main_toolbar);
            //Toolbar will now take on default Action Bar characteristics
            SetSupportActionBar (toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled (true);

            string pharmacy_received = Intent.GetStringExtra("pharmacy");
            pharmacy = JsonConvert.DeserializeObject<Pharmacy>(pharmacy_received);
            LoadViews();
            pharmacy_title.Text = pharmacy.NombreSucursal;
            pharmacy_subtitle.Text = pharmacy.NombreComercial;
            pharmacy_state.Text = pharmacy.Estado;
            pharmacy_city.Text = pharmacy.Ciudad;
            pharmacy_address.Text = pharmacy.Direccion;

            mMapView.OnCreate(savedInstanceState);
            try
            {
                MapsInitializer.Initialize(Application.Context);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message);
            }

            mMapView.GetMapAsync(new CustomMapReady(this));


        }

        protected override void OnStart ()
        {
            base.OnStart ();
            Analytics.TrackEvent ("View: Pharmacy Detail");
        }

        private class CustomMapReady : Java.Lang.Object, IOnMapReadyCallback
        {
            private activity_pharmacy pharmacies = null;
            public CustomMapReady(activity_pharmacy pharma) { this.pharmacies = pharma; }
            public GoogleMap mMap { get; private set; }
            public event EventHandler MapReady;

            public void OnMapReady(GoogleMap googleMap)
            {
                mMap = googleMap;
                var handler = MapReady;
                if (handler != null)
                    handler(this, EventArgs.Empty);

                googleMap.MyLocationEnabled = true;

                googleMap.UiSettings.SetAllGesturesEnabled(false);

                Pharmacy pharma = pharmacies.pharmacy;
                MarkerOptions mo = new MarkerOptions();
                mo.SetPosition(new LatLng(pharma.Latitud, pharma.Longitud));
                mo.SetTitle(pharma.NombreComercial);
                mo.SetSnippet(pharma.NombreSucursal);
                googleMap.AddMarker(mo).SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueOrange));
               
                // For zooming automatically to the location of the marker
                CameraPosition cameraPosition = new CameraPosition.Builder().Target(new LatLng(pharma.Latitud, pharma.Longitud)).Zoom(14).Build();
                googleMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            mMapView.OnResume();
            search.Click += Search_Products_Click;
        }

        protected override void OnPause()
        {
            base.OnPause();
            mMapView.OnPause();
            search.Click -= Search_Products_Click;
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Finish();
            OverridePendingTransition(Resource.Animation.slide_enter, Resource.Animation.slide_exit);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if(item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();       
            }
            return base.OnOptionsItemSelected(item);
        }

        private void LoadViews()
        {
            pharmacy_title = FindViewById<TextView>(Resource.Id.pharmacy_title);
			pharmacy_subtitle = FindViewById<TextView>(Resource.Id.pharmacy_subtitle);
			pharmacy_state = FindViewById<TextView>(Resource.Id.pharmacy_state);
			pharmacy_city = FindViewById<TextView>(Resource.Id.pharmacy_city);
            pharmacy_address = FindViewById<TextView>(Resource.Id.pharmacy_address);
            mMapView = (MapView)FindViewById(Resource.Id.mapView);
        }

        private void Search_Products_Click (object sender, EventArgs e)
        {
            Intent intent = new Intent (this, typeof (activity_products));
            string str = JsonConvert.SerializeObject (null);

            if(string.IsNullOrEmpty (str) || str == "null")
            {
                throw new Exception ("Empty or Null object in pharmacy parameter!");
            }

            intent.PutExtra ("pharmacy", str);
            Bundle bndlanimation = ActivityOptions.MakeCustomAnimation (this, Resource.Animation.slide_in, Resource.Animation.slide_out).ToBundle ();

            this.StartActivity (intent, bndlanimation);

        }
    }
}
