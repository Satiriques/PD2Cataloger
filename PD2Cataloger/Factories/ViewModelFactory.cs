using PD2Cataloger.Translater;
using PD2Cataloger.ViewModels;

namespace PD2Cataloger.Factories
{
    public class ViewModelFactory
    {
        private readonly StatTranslater _statTranslater;

        public ViewModelFactory(StatTranslater statTranslater)
        {
            _statTranslater = statTranslater;
        }

        public StatViewModel CreateStat(StatModel statModel)
        {
            return new StatViewModel(statModel, _statTranslater);
        }

        public StatViewModel CreateMockStat(string content)
        {
            return new StatViewModel(content);
        }

        public ItemViewModel CreateItem(ItemModel itemModel)
        {
            return new ItemViewModel(this, itemModel);
        }
    }
}
