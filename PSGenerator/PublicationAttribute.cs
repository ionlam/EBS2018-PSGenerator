namespace PSGenerator
{
   public class PublicationAttribute
   {
      public string Name { get; set; }
      public object Value { get; set; }

      public override string ToString()
      {
         var strValue = Converter.ToString(Value);
         return string.Format("({0},{1})", Name, strValue);
      }
   }
}
