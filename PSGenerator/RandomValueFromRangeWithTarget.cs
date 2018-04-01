using System;

namespace PSGenerator
{
   public class RandomValueFromDoubleRangeWithTarget : RandomValueFromDoubleRange
   {
      double TargetValue { get; }
      RandomValueWithTarget RandomValueWithTarget { get; }

      public RandomValueFromDoubleRangeWithTarget(double valueFrom, double valueTo, int? valueDecimals, double targetValue, double targetValueMinPercent, int randomCount)
         : base(valueFrom, valueTo, valueDecimals)
      {
         TargetValue = targetValue;
         RandomValueWithTarget = new RandomValueWithTarget(targetValueMinPercent, randomCount);
      }

      public override double Next()
      {
         if (RandomValueWithTarget.NextTargetHit())
         {
            return TargetValue;
         }
         return base.Next();
      }
   }

   public class RandomValueFromIntRangeWithTarget : RandomValueBase<int>
   {
      RandomValueFromDoubleRangeWithTarget RandomValue { get; }

      public RandomValueFromIntRangeWithTarget(int valueFrom, int valueTo, int targetValue, double targetValueMinPercent, int randomCount)
      {
         RandomValue = new RandomValueFromDoubleRangeWithTarget(valueFrom, valueTo, 0, targetValue, targetValueMinPercent, randomCount);
      }

      public override int Next()
      {
         return (int)Math.Round(RandomValue.Next());
      }
   }

   public class RandomValueFromDateRangeWithTarget : RandomValueBase<DateTime>
   {
      RandomValueFromDoubleRangeWithTarget RandomValue { get; }

      public RandomValueFromDateRangeWithTarget(DateTime valueFrom, DateTime valueTo, DateTime targetValue, double targetValueMinPercent, int randomCount)
      {
         RandomValue = new RandomValueFromDoubleRangeWithTarget(valueFrom.Ticks, valueTo.Ticks, 0, targetValue.Ticks, targetValueMinPercent, randomCount);
      }

      public override DateTime Next()
      {
         var ticks = (long)Math.Round(RandomValue.Next());
         return new DateTime(ticks).Date;
      }
   }
}
