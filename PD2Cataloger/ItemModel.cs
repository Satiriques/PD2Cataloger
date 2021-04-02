using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace PD2Cataloger
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Quality
    {
        [EnumMember(Value = nameof(Normal))]
        Normal,
        [EnumMember(Value = nameof(Magic))]
        Magic,
        [EnumMember(Value = nameof(Rare))]
        Rare,
        [EnumMember(Value = nameof(Set))]
        Set,
        [EnumMember(Value = nameof(Unique))]
        Unique,
    }
    public class ItemModel
    {
        [JsonProperty("iLevel")]
        public int Ilevel { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("defense")]
        public int Defense { get; set; }

        [JsonProperty("quality")]
        public Quality Quality { get; set; }

        [JsonProperty("stats", NullValueHandling = NullValueHandling.Ignore)]
        public StatModel[] Stats { get; set; } = new StatModel[] { };

        [JsonProperty("socketed")]
        public Socket[] Sockets { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("set")]
        public string Set { get; set; }

        [JsonProperty("runeword")]
        public string Runeword { get; set; }

        [JsonProperty("isRuneword")]
        public bool IsRuneword { get; set; }

        [JsonProperty("isEthereal")]
        public bool IsEthereal { get; set; }
    }

    public class StatModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("skill")]
        public string Skill { get; set; }

        [JsonProperty("value")]
        public int? Value { get; set; }

        [JsonProperty("max")]
        public int? Max { get; set; }

        [JsonProperty("min")]
        public int? Min { get; set; }

        [JsonProperty("chance%")]
        public int? Chance { get; set; }

        [JsonProperty("level")]
        public int? Level { get; set; }
    }

    public class Range
    {
        [JsonProperty("max")]
        public int Max { get; set; }
        [JsonProperty("min")]
        public int Min { get; set; }
    }

    public class Socket
    {
        [JsonProperty("iLevel")]
        public int ILevel { get; set; }

        [JsonProperty("isRune")]
        public bool IsRune { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

}
