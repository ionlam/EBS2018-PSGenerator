using System.Collections.Generic;
using System.Linq;

namespace PSGenerator
{
   public class SubscriptionGenerator
   {
      public const string FILENAME = "subscriptions.txt";

      int TotalCount { get; }
      List<SubscriptionConstraintGenerator> ConstraintGenerators { get; }

      public SubscriptionGenerator(SubscriptionConfig config)
      {
         TotalCount = config.Count;
         ConstraintGenerators = config.Fields.Select(f => new SubscriptionConstraintGenerator(f, config.Count)).ToList();
      }

      public IEnumerable<Subscription> GenerateAll()
      {
         return Enumerable.Range(1, TotalCount).Select(i => GenerateOne());
      }

      Subscription GenerateOne()
      {
         var sub = new Subscription();
         foreach (var cg in ConstraintGenerators)
         {
            var constr = cg.Generate();
            if (constr != null) sub.Add(constr);
         }
         return sub;
      }
   }
}
