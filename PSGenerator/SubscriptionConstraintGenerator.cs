namespace PSGenerator
{
   public class SubscriptionConstraintGenerator
   {
      SubscriptionField Field { get; }
      RandomValueWithTarget RandomValueWithTarget { get; }
      RandomValue RandomOperator { get; }
      RandomValue RandomValue { get; }

      public SubscriptionConstraintGenerator(SubscriptionField field, int randomCount)
      {
         Field = field;
         var frequencyMinPercent = field.FieldFrequencyMinPercent ?? 100;
         var newRandomCount = new RandomValueWithTarget(frequencyMinPercent, randomCount).GetRandomNewCount();
         RandomValueWithTarget = new RandomValueWithTarget(frequencyMinPercent, newRandomCount);
         RandomOperator = field.GetRandomOperator(newRandomCount);
         RandomValue = field.GetRandomValue(newRandomCount);
      }

      public SubscriptionConstraint Generate()
      {
         if (RandomValueWithTarget.NextTargetMiss()) return null;
         return new SubscriptionConstraint
         {
            Name = Field.FieldName,
            Operator = (string)RandomOperator.NextObject(),
            Value = RandomValue.NextObject(),
         };
      }
   }
}
