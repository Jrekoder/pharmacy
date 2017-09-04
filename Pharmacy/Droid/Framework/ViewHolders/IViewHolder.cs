using Android.Views;
using System;

namespace Pharmacy.Droid
{
    public interface IViewHolder<TData>
    {
        void FindViews (View rootView);

        void SetData (TData data);

        void SetClickHandler (Action<View, int> handler);
    }
}