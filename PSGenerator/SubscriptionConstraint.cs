namespace PSGenerator
{
   public class SubscriptionConstraint
   {
      public string Name { get; set; }
      public string Operator { get; set; }
      public object Value { get; set; }

      public override string ToString()
      {
         var strValue = Converter.ToString(Value);
         return string.Format("({0},{1},{2})", Name, Operator, strValue);
      }
   }
}
