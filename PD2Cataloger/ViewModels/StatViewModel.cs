using PD2Cataloger.Core;
using PD2Cataloger.Translater;

namespace PD2Cataloger.ViewModels
{
    public class StatViewModel : BindableBase
    {

        public StatViewModel(StatModel stat, StatTranslater statTranslater)
        {
            Model = stat;
            if (stat.Range != null) Range = new RangeViewModel(stat.Range);

            FormattedString = statTranslater.GetFormattedString(stat);
        }

        public StatViewModel(string formattedString)
        {
            FormattedString = formattedString;
        }

        public StatModel Model { get; }
        public RangeViewModel Range { get; }
        public string FormattedString { get; }
    }
}
