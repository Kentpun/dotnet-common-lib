using System.ComponentModel;

namespace HKSH.Common.Enums
{
    /// <summary>
    /// TimeUnit
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        /// The second
        /// </summary>
        [Description("Second")] Second = 1001,

        /// <summary>
        /// The minute
        /// </summary>
        [Description("Minute")] Minute = 1002,

        /// <summary>
        /// The hour
        /// </summary>
        [Description("Hour")] Hour = 1003,

        /// <summary>
        /// The day
        /// </summary>
        [Description("Day")] Day = 1004,

        /// <summary>
        /// The month
        /// </summary>
        [Description("Month")] Month = 1005
    }
}