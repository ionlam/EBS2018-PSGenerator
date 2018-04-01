using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PSGenerator
{
   [JsonConverter(typeof(JsonSubtypes), "FieldType")]
   [JsonSubtypes.KnownSubType(typeof(PublicationFieldString), FieldType.String)]
   [JsonSubtypes.KnownSubType(typeof(PublicationFieldDouble), FieldType.Double)]
   [JsonSubtypes.KnownSubType(typeof(PublicationFieldInt), FieldType.Int)]
   [JsonSubtypes.KnownSubType(typeof(PublicationFieldDate), FieldType.Date)]
   public abstract class PublicationField
   {
      public string FieldName { get; set; }
      public abstract FieldType FieldType { get; }
      public abstract RandomValue GetRandomValue(int randomCount);
   }

   public abstract class PublicationFieldBase<TL, TV> : PublicationField
   {
      public List<TL> ValueList { get; set; }
      public TV ValueFrom { get; set; }
      public TV ValueTo { get; set; }
      public TV TargetValue { get; set; }
      public double? TargetValueMinPercent { get; set; }
      public bool HasTargetValue() { return TargetValue != null && TargetValueMinPercent != null; }
      public bool HasRangeValue() { return ValueFrom != null && ValueTo != null; }
   }

   public class PublicationFieldString : PublicationFieldBase<string, string>
   {
      public override FieldType FieldType { get; } = FieldType.String;
      public override RandomValue GetRandomValue(int randomCount)
      {
         if (HasRangeValue()) throw new NotImplementedException();
         if (HasTargetValue()) return new RandomValueFromListWithTarget<string>(ValueList, TargetValue, TargetValueMinPercent.GetValueOrDefault(), randomCount);
         return new RandomValueFromList<string>(ValueList);
      }
   }

   public class PublicationFieldDouble : PublicationFieldBase<double, double?>
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

   public class PublicationFieldInt : PublicationFieldBase<int, int?>
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

   public class PublicationFieldDate : PublicationFieldBase<DateTime, DateTime?>
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
