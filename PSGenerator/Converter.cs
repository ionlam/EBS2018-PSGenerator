using System;
using System.Globalization;

namespace PSGenerator
{
   public static class Converter
   {
      public static string ToString(object value)
      {
         if (value is string) return $"\"{value}\"";
         if (value is DateTime) return ((DateTime)value).ToString("d.MM.yyyy", CultureInfo.InvariantCulture);
         return Convert.ToString(value, CultureInfo.InvariantCulture);
      }
   }
}
