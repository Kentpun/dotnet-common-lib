namespace HKSH.BaseService.CustomException
{
    /// <summary>
    /// SubscribeActionRepeateException
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public class SubscribeActionRepeateException : OperationCanceledException
    {
        /// <summary>
        /// Gets or sets the cap subsribe request identifier.
        /// </summary>
        /// <value>
        /// The cap subsribe request identifier.
        /// </value>
        public string CapSubsribeRequestId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeActionRepeateException"/> class.
        /// </summary>
        /// <param name="capSubsribeRequestId">The cap subsribe request identifier.</param>
        public SubscribeActionRepeateException(string capSubsribeRequestId) : base($"Subscription message repeated consumption:{capSubsribeRequestId}")
        {
            CapSubsribeRequestId = capSubsribeRequestId;
        }
    }
}