using System;

namespace PSGenerator
{
   public class RandomValueWithTarget
   {
      Random Random { get; } = RandomUtil.NewRandom();
      double TargetValueMinPercent { get; }
      int RandomCount { get; }
      int TotalCount { get; set; }
      int TargetCount { get; set; }

      public RandomValueWithTarget(double targetValueMinPercent, int randomCount)
      {
         TargetValueMinPercent = targetValueMinPercent;
         RandomCount = randomCount;
      }

      public bool NextTargetHit()
      {
         CheckCounters();
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

      public int GetTargetCount()
      {
         return (int)Math.Ceiling(RandomCount * TargetValueMinPercent / 100);
      }

      public int GetRandomNewCount()
      {
         return Random.Next(GetTargetCount(), RandomCount + 1);
      }

      void CheckCounters()
      {
         if (TotalCount > 0) return;
         TotalCount = RandomCount;
         TargetCount = GetTargetCount();
      }
   }
}
