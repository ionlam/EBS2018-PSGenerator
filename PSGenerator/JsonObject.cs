using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSGenerator
{
   public class JsonObject
   {
      class BaseFirstContractResolver : DefaultContractResolver
      {
         public static BaseFirstContractResolver Instance { get; } = new BaseFirstContractResolver();

         protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
         {
            var properties = base.CreateProperties(type, memberSerialization);
            if (properties == null) return null;
            return properties.OrderBy(p => p.PropertyName).ToList();
         }
      }

      static JsonSerializerSettings JsonSettings { get; } = new JsonSerializerSettings
      {
         ContractResolver = BaseFirstContractResolver.Instance,
         NullValueHandling = NullValueHandling.Ignore,
         DateFormatString = "yyyy-MM-dd",
      };

      public void FromFile(string filename)
      {
         FromJson(File.ReadAllText(filename));
      }
      public void FromJson(string json)
      {
         if (string.IsNullOrWhiteSpace(json)) return;
         JsonConvert.PopulateObject(json, this, JsonSettings);
      }

      public void ToFile(string filename, bool indent = true)
      {
         File.WriteAllText(filename, ToJson(indent));
      }
      public string ToJson(bool indent = true)
      {
         var formatting = indent ? Formatting.Indented : Formatting.None;
         return JsonConvert.SerializeObject(this, formatting, JsonSettings);
      }

      public override string ToString()
      {
         return ToJson(true);
      }
   }
}
