using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PSGenerator
{
   [JsonConverter(typeof(JsonSubtypes), "FieldType")]
   [JsonSubtypes.KnownSubType(typeof(SubscriptionFieldString), FieldType.String)]
   [JsonSubtypes.KnownSubType(typeof(SubscriptionFieldDouble), FieldType.Double)]
   [JsonSubtypes.KnownSubType(typeof(SubscriptionFieldInt), FieldType.Int)]
   [JsonSubtypes.KnownSubType(typeof(SubscriptionFieldDate), FieldType.Date)]
   public abstract class SubscriptionField
   {
      public string FieldName { get; set; }
      public abstract FieldType FieldType { get; }
      public double? FieldFrequencyMinPercent { get; set; }

      public abstract RandomValue GetRandomValue(int randomCount);
      public abstract RandomValue GetRandomOperator(int randomCount);
   }

   public abstract class SubscriptionFieldBase<TL, TV> : SubscriptionField
   {
      public List<TL> ValueList { get; set; }
      public TV ValueFrom { get; set; }
      public TV ValueTo { get; set; }
      public TV TargetValue { get; set; }
      public double? TargetValueMinPercent { get; set; }

      public List<string> Operators { get; set; }
      public string TargetOperator { get; set; }
      public int? TargetOperatorMinPercent { get; set; }

      public bool HasTargetOperator() { return TargetOperator != null && TargetOperatorMinPercent != null; }
      public bool HasTargetValue() { return TargetValue != null && TargetValueMinPercent != null; }
      public bool HasRangeValue() { return ValueFrom != null && ValueTo != null; }

      public override RandomValue GetRandomOperator(int randomCount)
      {
         if (HasTargetOperator()) return new RandomValueFromListWithTarget<string>(Operators, TargetOperator, TargetOperatorMinPercent.GetValueOrDefault(), randomCount);
         return new RandomValueFromList<string>(Operators);
      }
   }

   public class SubscriptionFieldString : SubscriptionFieldBase<string, string>
   {
      public override FieldType FieldType { get; } = FieldType.String;
      public override RandomValue GetRandomValue(int randomCount)
      {
         if (HasRangeValue()) throw new NotImplementedException();
         if (HasTargetValue()) return new RandomValueFromListWithTarget<string>(ValueList, TargetValue, TargetValueMinPercent.GetValueOrDefault(), randomCount);
         return new RandomValueFromList<string>(ValueList);
      }
   }

   public class SubscriptionFieldDouble : SubscriptionFieldBase<double, double?>
   {
      public override FieldType FieldType { get; } = FieldType.Double;
      public int? ValueDecimals { get; set; }
      public override RandomValue GetRandomValue(int randomCount)
      {
         if (HasTargetValue())
         {
            if (HasRangeValue()) return new RandomValueFromDoubleRangeWithTarget(ValueFrom.GetValueOrDefault(), ValueTo.GetValueOrDefault(), ValueDecimals, TargetValue.GetValueOrDefault(), TargetValueMinPercent.GetValueOrDefault(), randomCount);
            return new RandomValueFromListWithTarget<double>(ValueList, TargetValue.GetValueOrDefault(), TargetValueMinPercent.GetValueOrDefault(), randomCount);
         }
         if (HasRangeValue()) return new RandomValueFromDoubleRange(ValueFrom.GetValueOrDefault(), ValueTo.GetValueOrDefault(), ValueDecimals);
         return new RandomValueFromList<double>(ValueList);
      }
   }

   public class SubscriptionFieldInt : SubscriptionFieldBase<int, int?>
   {
      public override FieldType FieldType { get; } = FieldType.Int;
      public override RandomValue GetRandomValue(int randomCount)
      {
         if (HasTargetValue())
         {
            if (HasRangeValue()) return new RandomValueFromIntRangeWithTarget(ValueFrom.GetValueOrDefault(), ValueTo.GetValueOrDefault(), TargetValue.GetValueOrDefault(), TargetValueMinPercent.GetValueOrDefault(), randomCount);
            return new RandomValueFromListWithTarget<int>(ValueList, TargetValue.GetValueOrDefault(), TargetValueMinPercent.GetValueOrDefault(), randomCount);
         }
         if (HasRangeValue()) return new RandomValueFromIntRange(ValueFrom.GetValueOrDefault(), ValueTo.GetValueOrDefault());
         return new RandomValueFromList<int>(ValueList);
      }
   }

   public class SubscriptionFieldDate : SubscriptionFieldBase<DateTime, DateTime?>
   {
      public override FieldType FieldType { get; } = FieldType.Date;
      public override RandomValue GetRandomValue(int randomCount)
      {
         if (HasTargetValue())
         {
            if (HasRangeValue()) return new RandomValueFromDateRangeWithTarget(ValueFrom.GetValueOrDefault(), ValueTo.GetValueOrDefault(), TargetValue.GetValueOrDefault(), TargetValueMinPercent.GetValueOrDefault(), randomCount);
            return new RandomValueFromListWithTarget<DateTime>(ValueList, TargetValue.GetValueOrDefault(), TargetValueMinPercent.GetValueOrDefault(), randomCount);
         }
         if (HasRangeValue()) return new RandomValueFromDateRange(ValueFrom.GetValueOrDefault(), ValueTo.GetValueOrDefault());
         return new RandomValueFromList<DateTime>(ValueList);
      }
   }
}
