using System.Collections.Generic;
using Android.Views;
using Pharmacy.Domain;

namespace Pharmacy.Droid
{
    public class ContentRecyclerAdapter : RecyclerViewAdapter<AvContent, ContentViewHolder>//, FastScrollRecyclerView.ISectionedAdapter
    {
        public ContentRecyclerAdapter(IList<AvContent> dataSet) : base(dataSet)
        {
        }

        protected override ContentViewHolder CreateViewHolder(LayoutInflater inflater, ViewGroup parent)
        {
            return new ContentViewHolder(new View(parent.Context));
        }
    }
}