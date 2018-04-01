using System;

namespace PSGenerator
{
   public static class RandomUtil
   {
      public static Random NewRandom()
      {
         var seed = Guid.NewGuid().GetHashCode();
         return new Random(seed);
      }
   }
}
