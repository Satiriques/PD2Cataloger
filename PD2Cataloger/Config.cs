using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PD2Cataloger
{
    public class Config
    {
        private static readonly string _path = "config.json";

        static public Config Create()
        {
            if (!File.Exists(_path))
            {
                var config = new Config();
                config.Save();
                return config;
            }

            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(_path));
        }

        public HashSet<string> Accounts { get; } = new();
        public Dictionary<string, HashSet<ItemModel>> ItemsByAccount { get; } = new();

        private void Save() => File.WriteAllText(_path, JsonConvert.SerializeObject(this, Formatting.Indented));

        private async Task SaveAsync() => await File.WriteAllTextAsync(_path, JsonConvert.SerializeObject(this, Formatting.Indented));

        public void RemoveAccount(string accountName)
        {
            Accounts.Remove(accountName);
            ItemsByAccount.Remove(accountName);
            Save();
        }

        public void RemoveItem(string accountName, ItemModel item)
        {
            var key = ItemsByAccount.GetValueOrDefault(accountName);

            if(key != null)
            {
                key.Remove(item);
                Save();
            }
        }

        public void AddAccount(string accountName)
        {
            if (!Accounts.Contains(accountName))
            {
                Accounts.Add(accountName);
                ItemsByAccount.Add(accountName, new HashSet<ItemModel>());
                Save();
            }
        }

        public void AddItem(string accountName, ItemModel item)
        {
            var key = ItemsByAccount.GetValueOrDefault(accountName);

            if(key == null)
            {
                ItemsByAccount.Add(accountName, new HashSet<ItemModel>());
                key = ItemsByAccount[accountName];
            }

            key.Add(item);
            Save();
        }
    }
}
