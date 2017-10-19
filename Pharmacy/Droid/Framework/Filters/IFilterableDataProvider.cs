using System.Collections.Generic;

namespace Pharmacy.Droid
{
    public interface IFilterableDataProvider<TData>
    {
        IList<TData> AllItems { get; }

        void SetFilterResults(IList<TData> items);

        void ResetResults();
    }
}