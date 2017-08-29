using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Pharmacy.Droid
{
    public class fragment_products : Android.Support.V4.App.Fragment
    {
        private activity_main mActivity = null;
        private ProductListAdapter mAdapter = null;
        private List<Product> products = null;

        public fragment_products(activity_main act)
        {
            this.mActivity = act;
            products = Product.LoadProducts();
        }
        public static fragment_products NewInstance(activity_main act)
        {
            fragment_products fragment = new fragment_products(act);
            return fragment;
        }
        public override void OnResume()
        {
            base.OnResume();
        }
        public override void OnPause()
        {
            base.OnPause();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_section_products, container, false);

            this.mAdapter = new ProductListAdapter(inflater, this);
            ((ListView)rootView.FindViewById(Resource.Id.ListProducts)).Adapter = this.mAdapter;

            return rootView;
        }

        private class ProductListAdapter : BaseAdapter
        {
            private LayoutInflater inflater;
            private fragment_products fragment_products;
            private List<Product> products = null;
            public ProductListAdapter(LayoutInflater inflater, fragment_products fragment_products)
            {
                this.inflater = inflater;
                this.fragment_products = fragment_products;
                this.products = fragment_products.products;
            }

            public override int Count
            {
                get
                {
                    return products.Count;
                }

            }

            public override Java.Lang.Object GetItem(int position)
            {
                return null;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    convertView = inflater.Inflate(Resource.Layout.item_product, parent, false);
                }
                convertView.Id = position;

                ((TextView)convertView.FindViewById(Resource.Id.product_name)).Text = products[position].Descripcion;
                ((TextView)convertView.FindViewById(Resource.Id.product_description)).Text = products[position].Codigo;
                ((TextView)convertView.FindViewById(Resource.Id.product_price)).Text = string.Format("${0} MX", products[position].Precio);

                return convertView;
            }
        }

    }
}