namespace HKSH.Common.Processor
{
    /// <summary>
    /// process context
    /// </summary>
    public class ProcessCtx
    {
        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public Dictionary<string, object> Attributes { get; set; } = new();
    }
}