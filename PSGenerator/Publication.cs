using System.Collections.Generic;
using System.Linq;

namespace PSGenerator
{
   public class Publication
   {
      List<PublicationAttribute> Attributes { get; } = new List<PublicationAttribute>();

      public void Add(PublicationAttribute attr)
      {
         Attributes.Add(attr);
      }

      public IEnumerable<PublicationAttribute> All()
      {
         return Attributes;
      }

      public override string ToString()
      {
         if (Attributes.Count == 0) return "";
         var strAttributes = Attributes.Select(f => f.ToString());
         return "{" + string.Join(";", strAttributes) + "}";
      }
   }
}
