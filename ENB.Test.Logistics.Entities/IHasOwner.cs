namespace ENB.Test.Logistics.Entities
{
  /// <summary>
  /// This interface is used to mark the owner of an object.
  /// </summary>
  public interface IHasOwner
  {
    /// <summary>
    /// The Operator instance this object belongs to.
    /// </summary>
      Operator Owner { get; set; }
  }
}
