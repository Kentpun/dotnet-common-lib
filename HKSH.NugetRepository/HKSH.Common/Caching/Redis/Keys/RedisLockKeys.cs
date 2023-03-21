namespace HKSH.Utils.Redis
{
    /// <summary>
    /// RedisLockKeys
    /// </summary>
    public enum RedisLockKeys
    {
        /// <summary>
        /// The point import
        /// </summary>
        LockPointImport = 0,

        /// <summary>
        /// The expired store points notice
        /// </summary>
        LockExpiredStorePointsNotice = 1,

        /// <summary>
        /// The expired store points clear
        /// </summary>
        LockExpiredStorePointsClear = 2,

        /// <summary>
        /// The upload file slicing
        /// </summary>
        LockUploadFileSlicing = 3,

        /// <summary>
        /// The upload file slicing set chunk
        /// </summary>
        LockUploadFileSlicingSetChunk = 4,

        /// <summary>
        /// The lock cap action
        /// </summary>
        LockCapAction = 5,

        /// <summary>
        /// The lock stock in scan code
        /// </summary>
        LockStockInScanCode = 6
    }
}