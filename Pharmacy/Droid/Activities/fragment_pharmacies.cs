using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Pharmacy.Droid
{
    public class fragment_pharmacies : Android.Support.V4.App.Fragment
	{
        private activity_main mActivity = null;
		private MapView mMapView = null;
        private PharmacyListAdapter mAdapter = null;
        private List<Pharmacy> mPharmacies = null;

		public fragment_pharmacies(activity_main act) {
            this.mActivity = act;
            this.mPharmacies = Pharmacy.LoadPharmacies();
        }

		public static fragment_pharmacies NewInstance(activity_main act)
		{
			fragment_pharmacies fragment = new fragment_pharmacies(act);
			return fragment;
		}

		public override void OnResume()
		{
			base.OnResume();
			mMapView.OnResume();
		}

		public override void OnPause()
		{
			base.OnPause();
			mMapView.OnPause();
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			mMapView.OnDestroy();
		}

		public override void OnLowMemory()
		{
			base.OnLowMemory();
			mMapView.OnLowMemory();
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate(Resource.Layout.fragment_section_pharmacies, container, false);
			mMapView = (MapView)rootView.FindViewById(Resource.Id.mapView);
			mMapView.OnCreate(savedInstanceState);
			mMapView.OnResume();

            this.mAdapter = new PharmacyListAdapter(inflater, this);
            ((ListView)rootView.FindViewById(Resource.Id.list_pharmacies)).Adapter = this.mAdapter;
            ((ListView)rootView.FindViewById(Resource.Id.list_pharmacies)).OnItemClickListener = new SetOnClickListener(mActivity, this);

			try
			{
				MapsInitializer.Initialize(Application.Context);
			}
			catch (Exception e)
			{
				System.Diagnostics.Trace.TraceError(e.Message);
			}

			mMapView.GetMapAsync(new CustomMapReady(this));

			return rootView;
		}

		private class SetOnClickListener : Java.Lang.Object, ListView.IOnItemClickListener
		{
			private activity_main activity = null;
            private fragment_pharmacies fragment = null;

			public SetOnClickListener(activity_main act, fragment_pharmacies frag)
			{
				this.activity = act;
                this.fragment = frag;
			}

            public void OnItemClick(AdapterView parent, View view, int position, long id)
            {
				Intent intent = new Intent(activity, typeof(activity_pharmacy));
                string str = JsonConvert.SerializeObject(this.fragment.mPharmacies[position]);
                intent.PutExtra("pharmacy", str);
                Bundle bndlanimation = ActivityOptions.MakeCustomAnimation(activity, Resource.Animation.slide_in, Resource.Animation.slide_out).ToBundle();
                activity.StartActivity(intent, bndlanimation);
            }
        }

		private class CustomMapReady : Java.Lang.Object, IOnMapReadyCallback
		{
            private fragment_pharmacies fragment = null;
            public CustomMapReady(fragment_pharmacies frag) { this.fragment = frag; }
			public GoogleMap mMap { get; private set; }
			public event EventHandler MapReady;

			public void OnMapReady(GoogleMap googleMap)
			{
				mMap = googleMap;
				var handler = MapReady;
				if (handler != null)
					handler(this, EventArgs.Empty);

				googleMap.MyLocationEnabled = true;

				foreach (Pharmacy p in fragment.mPharmacies)
				{
					MarkerOptions mo = new MarkerOptions();
					mo.SetPosition(new LatLng(p.Latitud, p.Longitud));
					mo.SetTitle(p.NombreComercial);
					mo.SetSnippet(p.NombreSucursal);
					googleMap.AddMarker(mo).SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueOrange));
				}

				// For zooming automatically to the location of the marker
				CameraPosition cameraPosition = new CameraPosition.Builder().Target(new LatLng(19.358051, -99.186442)).Zoom(14).Build();
				googleMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
			}
		}

		private class PharmacyListAdapter : BaseAdapter
		{
            private LayoutInflater inflater = null;
            private fragment_pharmacies fragment = null;
            private List<Pharmacy> pharmacies = null;

			public PharmacyListAdapter(LayoutInflater inf, fragment_pharmacies frag)
			{
                this.inflater = inf;
                this.fragment = frag;
                this.pharmacies = this.fragment.mPharmacies;
			}

			public override int Count
			{
				get
				{
					return pharmacies.Count;
				}
			}

			public override Java.Lang.Object GetItem(int position)
			{
                return null; //pharmacies[position];
			}

			public override long GetItemId(int position)
			{
				return position;
			}

			public override View GetView(int position, View convertView, ViewGroup parent)
			{
				if (convertView == null)
				{
					convertView = inflater.Inflate(Resource.Layout.item_pharmacy, parent, false);
				}
				convertView.Id = position;

				((TextView)convertView.FindViewById(Resource.Id.pharmacy_title)).Text = pharmacies[position].NombreComercial;
                ((TextView)convertView.FindViewById(Resource.Id.pharmacy_subtitle)).Text = pharmacies[position].NombreSucursal;

				return convertView;
			}
		}
	}
}
