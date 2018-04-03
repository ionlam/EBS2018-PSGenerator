using System;

namespace PSGenerator
{
   public class RandomValueWithTarget
   {
      Random Random { get; } = RandomUtil.NewRandom();
      double TargetValueMinPercent { get; }
      int RandomTotalCount { get; }
      int RandomTargetCount { get; }
      int TotalCount { get; set; }
      int TargetCount { get; set; }

      public RandomValueWithTarget(double targetValueMinPercent, int randomTotalCount)
      {
         TargetValueMinPercent = targetValueMinPercent;
         RandomTotalCount = randomTotalCount;
         var minTargetCount = (int)Math.Ceiling(RandomTotalCount * TargetValueMinPercent / 100);
         RandomTargetCount = Random.Next(minTargetCount, RandomTotalCount + 1);
      }

      public bool NextTargetHit()
      {
         if (TotalCount == 0)
         {
            TotalCount = RandomTotalCount;
            TargetCount = RandomTargetCount;
         }
         if (Random.Next(TotalCount) < TargetCount)
         {
            TotalCount--;
            TargetCount--;
            return true;
         }
         TotalCount--;
         return false;
      }

      public bool NextTargetMiss()
      {
         return !NextTargetHit();
      }

      public int GetRandomTargetCount()
      {
         return RandomTargetCount;
      }
   }
}
