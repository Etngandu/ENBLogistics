namespace ENB.Test.Logistics.Entities
{
  /// <summary>
  /// Determines the type of a contact person.
  /// </summary>
  public enum OperatorType
  {
    /// <summary>
    /// Indicates an unidentified value.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates a Embedded.
    /// </summary>
    Embedded = 1,

    /// <summary>
    /// Indicates a flex member.
    /// </summary>
    Flex = 2,

    /// <summary>
    /// Indicates a Temporary.
    /// </summary>
    Temporary = 3
  }
}
