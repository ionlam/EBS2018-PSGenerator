namespace PSGenerator
{
   public abstract class RandomValueBase<T> : RandomValue
   {
      public abstract T Next();

      public override object NextObject()
      {
         return Next();
      }
   }
}
