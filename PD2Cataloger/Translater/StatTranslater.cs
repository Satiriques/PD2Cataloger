using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD2Cataloger.Translater
{
    public class StatTranslater
    {
        private Dictionary<string, string> _translationDictionary;

        public StatTranslater()
        {
        }

        public void LoadFile(string path)
        {
            _translationDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));
        }

        public string GetFormattedString(StatModel model)
        {
            var stringValue = _translationDictionary.GetValueOrDefault(model.Name)?
                                   .Replace("%value%", model.Value?.ToString())
                                   .Replace("%chance%", model.Chance?.ToString())
                                   .Replace("%level%", model.Level?.ToString())
                                   .Replace("%skill%", model.Skill) ?? model.Name;

            return stringValue;
        }
    }
}
