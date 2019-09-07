using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TCPExchanger
{
    [DataContract]
    public class Person
    {
        [DataMember(Name = "id")] public int ID { get; set; }

        [DataMember(Name = "name")] public string Name { get; set; }

        // 配列
        [DataMember(Name = "numbers")] public int[] Numbers { get; set; }

        // リスト型
        [DataMember(Name = "list")] public List<int> NumberList { get; private set; } = new List<int>();

        // ハッシュマップ型
        [DataMember(Name = "map")]
        public IDictionary<string, string> Attributes { get; private set; } = new Dictionary<string, string>();
    }

    [JsonObject]
    public class Json
    {
        [JsonProperty("id")] public int UserID { get; set; }
        [JsonProperty("name")] public string Username { get; set; }
    }
}