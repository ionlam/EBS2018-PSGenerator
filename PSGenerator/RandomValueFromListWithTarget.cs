using System.Collections.Generic;

namespace PSGenerator
{
   public class RandomValueFromListWithTarget<T> : RandomValueFromList<T>
   {
      T TargetValue { get; }
      RandomValueWithTarget RandomValueWithTarget { get; }

      public RandomValueFromListWithTarget(List<T> valueList, T targetValue, double targetValueMinPercent, int randomCount)
         : base(valueList)
      {
         TargetValue = targetValue;
         RandomValueWithTarget = new RandomValueWithTarget(targetValueMinPercent, randomCount);
      }

      public override T Next()
      {
         if (RandomValueWithTarget.NextTargetHit())
         {
            return TargetValue;
         }
         return base.Next();
      }
   }
}
