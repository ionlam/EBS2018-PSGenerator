using System.Collections.Generic;
using System.Linq;

namespace PSGenerator
{
   public class PublicationGenerator
   {
      public const string FILENAME = "publications.txt";

      int TotalCount { get; }
      List<PublicationAttributeGenerator> AttributeGenerators { get; }

      public PublicationGenerator(PublicationConfig config)
      {
         TotalCount = config.Count;
         AttributeGenerators = config.Fields.Select(f => new PublicationAttributeGenerator(f, config.Count)).ToList();
      }

      public IEnumerable<Publication> GenerateAll()
      {
         return Enumerable.Range(1, TotalCount).Select(i => GenerateOne());
      }

      Publication GenerateOne()
      {
         var pub = new Publication();
         foreach (var ag in AttributeGenerators)
         {
            var attr = ag.Generate();
            pub.Add(attr);
         }
         return pub;
      }
   }
}
