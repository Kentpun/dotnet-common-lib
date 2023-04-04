namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// ICurrentContext
    /// </summary>
    public interface IRepositoryCurrentContext
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        ClaimCurrentUser CurrentUser { get; }
    }
}