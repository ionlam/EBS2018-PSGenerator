using System;
using System.Collections.Generic;

namespace PSGenerator
{
   public class RandomValueFromList<T> : RandomValueBase<T>
   {
      protected Random Random { get; } = RandomUtil.NewRandom();
      List<T> ValueList { get; }

      public RandomValueFromList(List<T> valueList)
      {
         ValueList = valueList;
      }

      public override T Next()
      {
         return ValueList[Random.Next(ValueList.Count)];
      }
   }
}
