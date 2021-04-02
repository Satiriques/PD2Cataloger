using PD2Cataloger.Core;
using PD2Cataloger.Factories;
using PD2Cataloger.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PD2Cataloger
{
    public class ItemViewModel : BindableBase
    {
        HashSet<string> _ignoredStats = new HashSet<string>() 
        {
            "maxdurability",
            "item_throw_maxdamage",
            "coldlength"
        };

        public ItemViewModel(ViewModelFactory viewModelFactory, ItemModel model)
        {
            Model = model;

            ILevel = model.Ilevel;
            Name = model.Name;
            Quality = model.Quality;
            Runeword = model.Runeword;
            Set = model.Set;
            Type = model.Type;
            IsRuneword = model.IsRuneword;
            Defense = model.Defense;
            IsEthereal = model.IsEthereal;
            NumberOfSockets = model.NumberOfSockets;

            for (var i = 0; i < model.Stats.Length; i++)
            {
                StatModel stat = model.Stats[i];

                if(_ignoredStats.Contains(stat.Name))
                {
                    continue;
                }

                var viewModel = viewModelFactory.CreateStat(stat);

                var index = viewModel.FormattedString.IndexOf('_');
                if (index != -1 && viewModel.FormattedString != stat.Name)
                {
                    var nextStat = model.Stats[i + 1];
                    var nextStatViewModel = viewModelFactory.CreateStat(nextStat);

                    var stringValue = viewModel.FormattedString.Substring(0, index);

                    var index2 = nextStatViewModel.FormattedString.IndexOf('_');
                    stringValue += nextStatViewModel.FormattedString.Substring(index2 + 2, nextStatViewModel.FormattedString.Length - index2 - 2);
                    ++i;
                    Stats.Add(viewModelFactory.CreateMockStat(stringValue));
                }
                else
                {
                    Stats.Add(viewModelFactory.CreateStat(stat));
                }
            }
        }

        public int ILevel { get; }
        public string Name { get; }
        public Quality Quality { get; }
        public string Runeword { get; }
        public string Set { get; }
        public ObservableCollection<StatViewModel> Stats { get; } = new();
        public string Type { get; }
        public bool IsRuneword { get; }
        public int Defense { get; }
        public bool IsEthereal { get; }
        public int NumberOfSockets { get; }
        public string FormattedString { get; }
        public ItemModel Model { get; }
    }
}
