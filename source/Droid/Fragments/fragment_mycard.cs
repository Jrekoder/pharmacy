using Android.OS;
using Android.Views;

namespace Pharmacy.Droid
{
    public class fragment_mycard : Android.Support.V4.App.Fragment
    {
        public fragment_mycard() { }

        public static fragment_mycard NewInstance()
        {
            fragment_mycard fragment = new fragment_mycard();
            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_section_mycard, container, false);
            return rootView;
        }
    }
}