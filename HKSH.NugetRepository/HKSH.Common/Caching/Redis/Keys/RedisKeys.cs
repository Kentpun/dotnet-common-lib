namespace HKSH.Common.Redis
{
    /// <summary>
    /// Redis keys
    /// </summary>
    public enum RedisKeys
    {
        /// <summary>
        /// The users
        /// </summary>
        Users = 0,

        /// <summary>
        /// The domain user
        /// </summary>
        DomainUsers = 2,

        /// <summary>
        /// RefreshTokens
        /// </summary>
        RefreshTokens = 3,

        /// <summary>
        /// RefreshTokensHash
        /// </summary>
        RefreshTokensHash = 4,

        /// <summary>
        /// The data dictionary
        /// </summary>
        DataDict = 5,

        /// <summary>
        /// The PKG matl type
        /// </summary>
        PkgMatlType = 6,

        /// <summary>
        /// The project user infomation
        /// </summary>
        InternalUser = 7,

        /// <summary>
        /// Brand
        /// </summary>
        Brand = 8,

        /// <summary>
        /// The MDM
        /// </summary>
        Mdm = 9,

        /// <summary>
        /// The Dag
        /// </summary>
        Dag = 10
    }
}