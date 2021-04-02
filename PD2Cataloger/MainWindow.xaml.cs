using MahApps.Metro.Controls;
using Newtonsoft.Json;
using PD2Cataloger.Factories;
using PD2Cataloger.Translater;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace PD2Cataloger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ClipboardMonitorWindow, INotifyPropertyChanged
    {
        private bool _isListening;
        private string _selectedAccount;
        private Config _config;
        private ItemViewModel _selectedItem;
        private readonly ViewModelFactory _viewModelFactory;
        public MainWindow()
        {
            _config = Config.Create();
            var statTranslater = new StatTranslater();
            statTranslater.LoadFile("Resources/Translation.json");
            _viewModelFactory = new ViewModelFactory(statTranslater);


            DataContext = this;

            ListenCommand = new DelegateCommand(ListenExecute, ListenCanExecute);
            ComboBoxEnterCommand = new DelegateCommand<ComboBox>(ComboBoxEnterExecute);
            RemoveCommand = new DelegateCommand(RemoveExecute, RemoveCanExecute);
            ClipboardUpdateCommand = new DelegateCommand(ClipboardUpdateExecute);

            SelectedAccount = _config.Accounts.FirstOrDefault();

            foreach (var account in _config.Accounts)
            {
                Accounts.Add(account);
            }

            UpdateItems();

            InitializeComponent();
        }

        private void ClipboardUpdateExecute()
        {
            if (IsListening)
            {
                var text = Clipboard.GetText();

                ItemModel item = null;

                try
                {
                    item = JsonConvert.DeserializeObject<ItemModel>(text);
                }
                catch
                {

                }

                if (item != null)
                {
                    _config.AddItem(SelectedAccount, item);
                    ShownItems.Add(_viewModelFactory.CreateItem(item));
                }
            }
        }

        public bool IsListening
        {
            get => _isListening; set
            {
                if (_isListening != value)
                {
                    _isListening = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                if (_selectedAccount != value)
                {
                    _selectedAccount = value;
                    RaisePropertyChanged();
                    RemoveCommand.RaiseCanExecuteChanged();
                    ListenCommand.RaiseCanExecuteChanged();
                    UpdateItems();
                }
            }
        }

        private void UpdateItems()
        {
            ShownItems.Clear();
            foreach (var item in _config.ItemsByAccount.GetValueOrEmpty<string, ItemModel, HashSet<ItemModel>>(SelectedAccount))
            {
                ShownItems.Add(_viewModelFactory.CreateItem(item));
            }
        }

        public ItemViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                if(_selectedItem != value)
                {
                    _selectedItem = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<string> Accounts { get; } = new();
        public ObservableCollection<ItemViewModel> ShownItems { get; } = new();


        public DelegateCommand AddButton { get; }
        public DelegateCommand ListenCommand { get; }
        public DelegateCommand<ComboBox> ComboBoxEnterCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public DelegateCommand<ComboBox> ComboBoxDeleteCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ListenExecute()
        {
            IsListening = !IsListening;
        }
        private bool ListenCanExecute()
            => !string.IsNullOrWhiteSpace(SelectedAccount);

        private void ComboBoxEnterExecute(ComboBox obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Text)) return;

            var newAccount = obj.Text;
            if (!Accounts.Contains(obj.Text))
            {
                _config.AddAccount(newAccount);
                Accounts.Add(newAccount);
                SelectedAccount = newAccount;
            }
        }

        private bool RemoveCanExecute() => !string.IsNullOrWhiteSpace(SelectedAccount);

        private void RemoveExecute()
        {
            Accounts.Remove(SelectedAccount);
            SelectedAccount = Accounts.FirstOrDefault();
        }

        private void ListBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Delete)
            {
                _config.RemoveItem(SelectedAccount, SelectedItem.Model);
                ShownItems.Remove(SelectedItem);
                SelectedItem = ShownItems.FirstOrDefault();
            }
        }
    }
}
