using PD2Cataloger.Core;

namespace PD2Cataloger.ViewModels
{
    public class RangeViewModel : BindableBase
    {
        private Range _range;

        public int Max { get; }
        public int Min { get; }

        public RangeViewModel(Range range)
        {
            _range = range;
            Max = range.Max;
            Min = range.Min;
        }
    }
}
