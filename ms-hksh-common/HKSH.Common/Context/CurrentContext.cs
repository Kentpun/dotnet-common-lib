using HKSH.Common.Repository.Database;

namespace HKSH.Common
{
    /// <summary>
    /// CurrentContext
    /// </summary>
    /// <seealso cref="ICurrentContext" />
    /// <seealso cref="IRepositoryCurrentContext" />
    public class CurrentContext : ICurrentContext, IRepositoryCurrentContext
    {
        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        private ClaimCurrentUser? _currentUser;

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public ClaimCurrentUser CurrentUser
        {
            get
            {
                _currentUser ??= new ClaimCurrentUser { Account = "168563", Name = "Leo Feihong", Phone = "15111907921", Id = 1 };
                return _currentUser;
            }
            set
            {
                _currentUser = value;
            }
        }
    }
}