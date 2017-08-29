using Android.OS;
using Android.Views;
using Android.Webkit;

namespace Pharmacy.Droid
{
    public class fragment_assistant : Android.Support.V4.App.Fragment
	{
		public fragment_assistant () { }

		public static fragment_assistant NewInstance()
		{
			fragment_assistant fragment = new fragment_assistant();
			return fragment;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View rootView = inflater.Inflate (Resource.Layout.fragment_section_myassistant, container, false);

			WebView webView = (WebView) rootView.FindViewById (Resource.Id.webView);
			webView.Settings.JavaScriptEnabled = true;
			webView.LoadUrl (Constants.WebChatURL);

			return rootView;
		}
	}
}
