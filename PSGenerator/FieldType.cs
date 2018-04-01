using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PSGenerator
{
   [JsonConverter(typeof(StringEnumConverter))]
   public enum FieldType
   {
      String,
      Double,
      Int,
      Date,
   }
}
