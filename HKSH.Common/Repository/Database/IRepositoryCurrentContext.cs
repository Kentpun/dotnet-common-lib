using HKSH.Common.ShareModel.User;

namespace HKSH.Common.Repository.Database
{
    /// <summary>
    /// IRepositoryCurrentContext
    /// </summary>
    public interface IRepositoryCurrentContext
    {
        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        UserInfoResponse CurrentUser { get; }
    }
}