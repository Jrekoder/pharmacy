using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Pharmacy.Droid
{
    [Activity(Label = "@string/products_title",
          Icon = "@mipmap/icon",
          LaunchMode = Android.Content.PM.LaunchMode.SingleTop, ParentActivity = typeof(activity_pharmacy),
          ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class activity_products : AppCompatActivity
    {
        private ProductListAdapter mAdapter = null;
        private List<Product> products = null;

        public activity_products()
        {
            products = Product.LoadProducts();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_products);

            this.mAdapter = new ProductListAdapter(this);
            ((ListView)FindViewById(Resource.Id.ListProducts)).Adapter = this.mAdapter;
        }

        private class ProductListAdapter : BaseAdapter
        {
            private activity_products activity_products;
            private List<Product> products = null;
            public ProductListAdapter(activity_products activity_products)
            {
                this.activity_products = activity_products;
                this.products = activity_products.products;
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
                    LayoutInflater layoutInflater = (LayoutInflater)Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService);
                    convertView = layoutInflater.Inflate(Resource.Layout.item_product, parent, false);
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