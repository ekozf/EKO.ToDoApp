namespace EKO.ToDoApp.Shared.Enums;

/// <summary>
/// Urgency flags
/// </summary>
public enum UrgencyEnum
{
    /// <summary>
    /// No urgency, not important
    /// </summary>
    None,

    /// <summary>
    /// Low urgency, should be done, isn't important
    /// </summary>
    Low,

    /// <summary>
    /// Medium urgency, required and important but not urgent
    /// </summary>
    Medium,

    /// <summary>
    /// High urgency, must be done as soon as possible!
    /// </summary>
    High
}
