using System.Collections.Generic;
using System.Linq;

namespace PSGenerator
{
   public class Subscription
   {
      List<SubscriptionConstraint> Constraints { get; } = new List<SubscriptionConstraint>();

      public void Add(SubscriptionConstraint constraint)
      {
         Constraints.Add(constraint);
      }

      public IEnumerable<SubscriptionConstraint> All()
      {
         return Constraints;
      }

      public override string ToString()
      {
         if (Constraints.Count == 0) return "";
         var strConstraints = Constraints.Select(c => c.ToString());
         return "{" + string.Join(";", strConstraints) + "}";
      }
   }
}
