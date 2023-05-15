namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// Program Configure
    /// </summary>
    public class ProgramConfigure
    {
        /// <summary>
        /// Gets or sets a value indicating whether [enable authentication].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable authentication]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableAuthentication { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [enable database context].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable database context]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableDbContext { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [enable migration].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable migration]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableMigration { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [enable static files].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable static files]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableStaticFiles { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [enable cors].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable cors]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableCors { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [enable buffering].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable buffering]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableBuffering { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether [enable kafka].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable kafka]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableKafka { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [enable mongo database].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable mongo database]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableMongoDB { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [enable elastic apm].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable elastic apm]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableElasticApm { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [enable rabbit mq].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable rabbit mq]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableRabbitMQ { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [enable XXL job].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable XXL job]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableXxlJob { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether [enable redis].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable redis]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableRedis { get; set; } = false;
    }
}