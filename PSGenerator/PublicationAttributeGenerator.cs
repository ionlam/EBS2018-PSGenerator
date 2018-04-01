namespace PSGenerator
{
   public class PublicationAttributeGenerator
   {
      PublicationField Field { get; }
      RandomValue RandomValue { get; }

      public PublicationAttributeGenerator(PublicationField field, int randomCount)
      {
         Field = field;
         RandomValue = field.GetRandomValue(randomCount);
      }
      public PublicationAttribute Generate()
      {
         return new PublicationAttribute { Name = Field.FieldName, Value = RandomValue.NextObject() };
      }
   }
}
