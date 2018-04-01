using System;
using System.IO;
using System.Linq;

namespace PSGenerator
{
   public class Program
   {
      ProgramConfig ProgramConfig { get; }

      public Program(ProgramConfig programConfig)
      {
         ProgramConfig = programConfig;
      }

      static void Main(string[] args)
      {
         var programConfig = new ProgramConfig(args);
         var program = new Program(programConfig);
         program.Run();
      }

      public void Run()
      {
         GeneratePublicationConfig();
         GenerateSubscriptionsConfig();

         GeneratePublications();
         GenerateSubscriptions();

         //Test();
      }

      void WriteJsonObjectToFile(JsonObject jso, string filename, bool force)
      {
         var fullFilename = Path.GetFullPath(filename);
         var fileExists = File.Exists(fullFilename);
         if (fileExists)
         {
            if (force)
            {
               jso.ToFile(filename);
               Console.WriteLine("Regenerated {0}", Path.GetFullPath(filename));
            }
            else Console.WriteLine("File already exists: {0}", Path.GetFullPath(filename));
         }
         else
         {
            jso.ToFile(filename);
            Console.WriteLine("Generated {0}", Path.GetFullPath(filename));
         }
      }

      void GeneratePublicationConfig()
      {
         var config = PublicationConfig.GetSample();
         WriteJsonObjectToFile(config, PublicationConfig.FILENAME, ProgramConfig.Force);
      }

      void GenerateSubscriptionsConfig()
      {
         var config = SubscriptionConfig.GetSample();
         WriteJsonObjectToFile(config, SubscriptionConfig.FILENAME, ProgramConfig.Force);
      }

      void GeneratePublications()
      {
         var config = new PublicationConfig();
         config.FromFile(PublicationConfig.FILENAME);
         var gen = new PublicationGenerator(config);
         var list = gen.GenerateAll();
         var lines = list.Select(p => p.ToString());
         var fullFilename = Path.GetFullPath(PublicationGenerator.FILENAME);
         File.WriteAllLines(fullFilename, lines);
         Console.WriteLine("Generated {0}", fullFilename);
      }

      void GenerateSubscriptions()
      {
         var config = new SubscriptionConfig();
         config.FromFile(SubscriptionConfig.FILENAME);
         var gen = new SubscriptionGenerator(config);
         var list = gen.GenerateAll();
         var lines = list.Select(s => s.ToString());
         var fullFilename = Path.GetFullPath(SubscriptionGenerator.FILENAME);
         File.WriteAllLines(fullFilename, lines);
         Console.WriteLine("Generated {0}", fullFilename);
      }

      void Test()
      {
         ProgramTest.TestRandomFromStringListWithTarget();
         ProgramTest.TestRandomFromIntRangeWithTarget();
         ProgramTest.TestRandomFromDateRangeWithTarget();
         //ProgramTest.TestJsonSerialization();
      }
   }
}
