using System;
using System.Collections.Generic;

namespace PSGenerator
{
   public class SubscriptionConfig : JsonObject
   {
      public static string FILENAME = "subscription.config";

      public int Count { get; set; }
      public List<SubscriptionField> Fields { get; } = new List<SubscriptionField>();

      public static SubscriptionConfig GetSample()
      {
         var config = new SubscriptionConfig { Count = 100 };
         config.Fields.Add(new SubscriptionFieldString
         {
            FieldName = "company",
            FieldFrequencyMinPercent = 90,
            ValueList = new List<string> { "Google", "Apple", "Microsoft", "Cisco", "Amazon", "Honda" },
            TargetValue = "Google",
            TargetValueMinPercent = 50,
            Operators = new List<string> { "=", "<>" },
            TargetOperator = "=",
            TargetOperatorMinPercent = 90,
         });
         config.Fields.Add(new SubscriptionFieldDouble
         {
            FieldName = "value",
            FieldFrequencyMinPercent = 80,
            ValueDecimals = 2,
            ValueFrom = 90 - 40,
            ValueTo = 90 + 60,
            Operators = new List<string> { "=", ">", ">=", "<=", "<" },
         });
         config.Fields.Add(new SubscriptionFieldDouble
         {
            FieldName = "variation",
            FieldFrequencyMinPercent = 70,
            ValueDecimals = 2,
            ValueFrom = 0.73 - 0.2,
            ValueTo = 0.73 + 0.3,
            Operators = new List<string> { "=", ">", "<" },
         });
         config.Fields.Add(new SubscriptionFieldDouble
         {
            FieldName = "drop",
            FieldFrequencyMinPercent = 60,
            ValueDecimals = 2,
            ValueFrom = 10 - 5,
            ValueTo = 10 + 5,
            Operators = new List<string> { "=", ">", "<" },
         });
         config.Fields.Add(new SubscriptionFieldDate
         {
            FieldName = "date",
            ValueList = new List<DateTime> { new DateTime(2018, 3, 21), new DateTime(2018, 3, 23), new DateTime(2018, 3, 30), new DateTime(2018, 4, 27) },
            TargetValue = new DateTime(2018, 3, 30),
            TargetValueMinPercent = 70,
            Operators = new List<string> { "=", ">", ">=", "<=", "<" },
            TargetOperator = ">=",
            TargetOperatorMinPercent = 70,
         });
         return config;
      }
   }
}
