using System;
using System.Collections.Generic;

namespace PSGenerator
{
   public class PublicationConfig : JsonObject
   {
      public const string FILENAME = "publication.config";

      public int Count { get; set; }
      public List<PublicationField> Fields { get; set; } = new List<PublicationField>();

      public static PublicationConfig GetSample()
      {
         var config = new PublicationConfig { Count = 1000 };
         config.Fields.Add(new PublicationFieldString
         {
            FieldName = "company",
            ValueList = new List<string> { "Google", "Apple", "Microsoft", "Cisco", "Amazon" },
            TargetValue = "Google",
            TargetValueMinPercent = 50,
         });
         config.Fields.Add(new PublicationFieldDouble
         {
            FieldName = "value",
            ValueDecimals = 2,
            ValueFrom = 90 - 40,
            ValueTo = 90 + 60,
         });
         config.Fields.Add(new PublicationFieldDouble
         {
            FieldName = "variation",
            ValueDecimals = 2,
            ValueFrom = 0.73 - 0.2,
            ValueTo = 0.73 + 0.3,
         });
         config.Fields.Add(new PublicationFieldDouble
         {
            FieldName = "drop",
            ValueDecimals = 2,
            ValueFrom = 10 - 5,
            ValueTo = 10 + 5,
         });
         config.Fields.Add(new PublicationFieldDate
         {
            FieldName = "date",
            ValueList = new List<DateTime> { new DateTime(2018, 3, 21), new DateTime(2018, 3, 23), new DateTime(2018, 3, 30), new DateTime(2018, 4, 27) },
            TargetValue = new DateTime(2018, 3, 30),
            TargetValueMinPercent = 70,
         });
         return config;
      }
   }
}
