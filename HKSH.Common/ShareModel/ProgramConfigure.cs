﻿namespace HKSH.Common.ShareModel
{
    /// <summary>
    /// 程序啟動配置項
    /// </summary>
    public class ProgramConfigure
    {
        /// <summary>
        /// 啟用Keycloak授權認證.
        /// </summary>
        public bool EnableAuthentication { get; set; } = false;

        /// <summary>
        /// 啟用數據庫上下文.
        /// </summary>
        public bool EnableDbContext { get; set; } = false;

        /// <summary>
        /// 啟用數據庫上下文自動執行Pening的Migration.
        /// </summary>
        public bool EnableMigration { get; set; } = false;

        /// <summary>
        /// 啟用靜態文件.
        /// </summary>
        public bool EnableStaticFiles { get; set; } = false;

        /// <summary>
        /// 啟用緩衝.
        /// </summary>
        public bool EnableBuffering { get; set; } = true;

        /// <summary>
        /// 啟用Kafka消息組件.
        /// </summary>
        public bool EnableKafka { get; set; } = false;

        /// <summary>
        /// 使用Elastic Apm鏈路追蹤.
        /// </summary>
        public bool EnableElasticApm { get; set; } = false;

        /// <summary>
        /// 啟用RabbitMQ消息組件.
        /// </summary>
        public bool EnableRabbitMQ { get; set; } = false;
    }
}