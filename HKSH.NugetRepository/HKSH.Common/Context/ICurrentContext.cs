using HKSH.Common.Repository.Database;

namespace ABI.ArtWork.Common
{
    /// <summary>
    /// ICurrentContext
    /// </summary>
    public interface ICurrentContext
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