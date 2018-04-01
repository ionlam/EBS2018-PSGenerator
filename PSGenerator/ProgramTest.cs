using JsonSubTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSGenerator
{
   public class ProgramTest
   {
      public static void TestRandomFromStringListWithTarget()
      {
         var valueList = new List<string> { "GOOGL", "AMZN", "MSFT", "CSCO" };
         var targetValue = "GOOGL";
         var targetValueMinPercent = 45.5;
         var randCount = 10;
         var rand = new RandomValueFromListWithTarget<string>(valueList, targetValue, targetValueMinPercent, randCount);
         var result = Enumerable.Range(1, randCount).Select(i => rand.Next()).ToList();
         var text = string.Join(",", result);
         Console.WriteLine(text);
         var count = result.Where(elem => elem == targetValue).Count();
         Console.WriteLine(count);
         Console.WriteLine();
      }

      public static void TestRandomFromIntRangeWithTarget()
      {
         var valueFrom = 1;
         var valueTo = 100;
         var targetValue = 11;
         var targetValueMinPercent = 20.0;
         var randCount = 100;
         var rand = new RandomValueFromIntRangeWithTarget(valueFrom, valueTo, targetValue, targetValueMinPercent, randCount);
         var result = Enumerable.Range(1, randCount).Select(i => rand.Next()).ToList();
         var text = string.Join(",", result.Select(e => e.ToString()));
         Console.WriteLine(text);
         var count = result.Where(elem => elem == targetValue).Count();
         Console.WriteLine(count);
         Console.WriteLine();
      }

      public static void TestRandomFromDateRangeWithTarget()
      {
         var valueFrom = new DateTime(2018, 3, 21);
         var valueTo = new DateTime(2018, 4, 27);
         var targetValue = new DateTime(2018, 3, 30);
         var targetValueMinPercent = 20.0;
         var randCount = 10;
         var rand = new RandomValueFromDateRangeWithTarget(valueFrom, valueTo, targetValue, targetValueMinPercent, randCount);
         var result = Enumerable.Range(1, randCount).Select(i => rand.Next()).ToList();
         var text = string.Join(",", result.Select(e => e.ToString("yyyy-MM-dd")));
         Console.WriteLine(text);
         var count = result.Where(elem => elem == targetValue).Count();
         Console.WriteLine(count);
         Console.WriteLine();
      }

      class Group : JsonObject
      {
         public string GroupName { get; set; }
         public List<Person> Members { get; set; } = new List<Person>();
      }
      [JsonConverter(typeof(StringEnumConverter))]
      enum PersonType
      {
         Employee,
         Artist
      }
      [JsonConverter(typeof(JsonSubtypes), "Type")]
      [JsonSubtypes.KnownSubType(typeof(Employee), PersonType.Employee)]
      [JsonSubtypes.KnownSubType(typeof(Artist), PersonType.Artist)]
      abstract class Person : JsonObject
      {
         public abstract PersonType Type { get; }
         public string Name { get; set; }
         public int Age { get; set; }
      }
      class Employee : Person
      {
         public override PersonType Type { get; } = PersonType.Employee;
         public string Job { get; set; }
         public string Department { get; set; }
      }
      class Artist : Person
      {
         public override PersonType Type { get; } = PersonType.Employee;
         public string Skill { get; set; }
      }


      public static void TestJsonSerialization()
      {
         var group = new Group { GroupName = "Fantastic4" };
         group.Members.Add(new Employee
         {
            Name = "Jon",
            Age = 33,
            Job = "electrical engineer",
            Department = "Wind Turbines"
         });
         group.Members.Add(new Artist
         {
            Name = "Michelle",
            Age = 25,
            Skill = "novice painter"
         });
         var json = group.ToJson();
         Console.WriteLine(json);
         Console.WriteLine();

         var group2 = new Group();
         group2.FromJson(json);
         var json2 = group2.ToJson();
         Console.WriteLine(json2);
         Console.WriteLine();
      }
   }
}
