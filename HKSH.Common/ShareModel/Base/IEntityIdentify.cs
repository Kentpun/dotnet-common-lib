namespace HKSH.Common.ShareModel.Base
{
    /// <summary>
    /// IEntityIdentify<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityIdentify<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        T Id { get; set; }
    }
}