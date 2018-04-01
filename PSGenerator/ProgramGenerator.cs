using System;
using System.IO;
using System.Linq;

namespace PSGenerator
{
   public class ProgramGenerator
   {
      public void Run()
      {
         GeneratePublications("publications.txt");
      }

      void GeneratePublications(string outputFilename)
      {
         var config = new PublicationConfig();
         config.FromFile(PublicationConfig.FILENAME);
         var gen = new PublicationGenerator(config);
         var list = gen.GenerateAll();
         var lines = list.Select(p => p.ToString());
         File.WriteAllLines(outputFilename, lines);
         Console.WriteLine("Generated {0}", Path.GetFullPath(outputFilename));
      }
   }
}
