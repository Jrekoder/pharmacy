using Android.OS;
using Android.Views;
using Android.Webkit;

namespace Pharmacy.Droid
{
    public class fragment_assistant : Android.Support.V4.App.Fragment, ITabFragment
    {
        #region ITabFragment Members

        public string Title => "Assistant";
        public int Icon => Resource.Drawable.ic_tabbar_resources;

        #endregion ITabFragment Members

        public fragment_assistant() { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_assistant, container, false);

            WebView webView = (WebView)rootView.FindViewById(Resource.Id.webView);
            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.DomStorageEnabled = true;
            webView.Settings.UseWideViewPort = true;
            webView.Settings.MixedContentMode = MixedContentHandling.AlwaysAllow;
            webView.LoadUrl(Settings.WebChatUrl);

            return rootView;
        }
    }
}