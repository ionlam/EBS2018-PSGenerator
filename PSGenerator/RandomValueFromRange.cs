using System;

namespace PSGenerator
{
   public class RandomValueFromDoubleRange : RandomValueBase<double>
   {
      protected Random Random { get; } = RandomUtil.NewRandom();
      double Offset { get; }
      double Range { get; }
      int? ValueDecimals { get; }

      public RandomValueFromDoubleRange(double valueFrom, double valueTo, int? valueDecimals)
      {
         Offset = valueFrom;
         Range = valueTo - valueFrom;
         ValueDecimals = valueDecimals;
      }

      public override double Next()
      {
         var value = Offset + Range * Random.NextDouble();
         if (ValueDecimals == null) return value;
         return Math.Round(value, ValueDecimals.GetValueOrDefault());
      }
   }

   public class RandomValueFromIntRange : RandomValueBase<int>
   {
      RandomValueFromDoubleRange RandomValue { get; }

      public RandomValueFromIntRange(int valueFrom, int valueTo)
      {
         RandomValue = new RandomValueFromDoubleRange(valueFrom, valueTo, 0);
      }

      public override int Next()
      {
         return (int)Math.Round(RandomValue.Next());
      }
   }

   public class RandomValueFromDateRange : RandomValueBase<DateTime>
   {
      RandomValueFromDoubleRange RandomValue { get; }

      public RandomValueFromDateRange(DateTime valueFrom, DateTime valueTo)
      {
         RandomValue = new RandomValueFromDoubleRange(valueFrom.Ticks, valueTo.Ticks, 0);
      }

      public override DateTime Next()
      {
         var ticks = (long)Math.Round(RandomValue.Next());
         return new DateTime(ticks).Date;
      }
   }
}
