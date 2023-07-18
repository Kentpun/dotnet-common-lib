using HKSH.Common.Extensions;
using NPOI.SS.Formula.Functions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HKSH.Common.AuditLogs.Models
{
    /// <summary>
    /// Audit History
    /// </summary>
    public class AuditHistory
    {
        /// <summary>
        /// The rows
        /// </summary>
        private readonly List<RowAuditLogDocument> _rows = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHistory"/> class.
        /// </summary>
        public AuditHistory()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditHistory"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        public AuditHistory(List<RowAuditLogDocument> rows)
        {
            _rows = rows;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        [Column("table_name")]
        [MaxLength(200)]
        public string? TableName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the row identifier.
        /// </summary>
        /// <value>
        /// The row identifier.
        /// </value>
        [Column("row_id")]
        public long? RowId { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [Column("action")]
        [MaxLength(100)]
        public string? Action { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        [Column("row")]
        public string? Row { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        [Column("version")]
        public string? Version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        [Column("created_by")]
        [MaxLength(100)]
        public string? CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The change log connection string
        /// </summary>
        public const string CHANGE_LOG_CONNECTION_STRING = "ChangeLog";

        /// <summary>
        /// Gets the create audit log table SQL.
        /// </summary>
        /// <value>
        /// The create audit log table SQL.
        /// </value>
        public static string CreateAuditLogTableSql
        {
            get
            {
                StringBuilder tableSqlBuilder = new();

                tableSqlBuilder.AppendLine(@$"BEGIN TRAN IF NOT EXISTS (");
                tableSqlBuilder.AppendLine(@$"  SELECT");
                tableSqlBuilder.AppendLine(@$"    TOP 1 *");
                tableSqlBuilder.AppendLine(@$"  FROM");
                tableSqlBuilder.AppendLine(@$"    sysObjects");
                tableSqlBuilder.AppendLine(@$"  WHERE");
                tableSqlBuilder.AppendLine(@$"    Id = OBJECT_ID('com_audit_{DateTime.Now:yyyyMM}')");
                tableSqlBuilder.AppendLine(@$"    and xtype = 'U'");
                tableSqlBuilder.AppendLine(@$") BEGIN CREATE TABLE com_audit_{DateTime.Now:yyyyMM} (");
                tableSqlBuilder.AppendLine(@$"  [id] [bigint] IDENTITY(1, 1) NOT NULL,");
                tableSqlBuilder.AppendLine(@$"  [table_name] [nvarchar] (200) NULL,");
                tableSqlBuilder.AppendLine(@$"  [row_id] [bigint] NULL,");
                tableSqlBuilder.AppendLine(@$"  [action] [nvarchar] (100) NULL,");
                tableSqlBuilder.AppendLine(@$"  [row] [nvarchar] (max)NULL,");
                tableSqlBuilder.AppendLine(@$"  [version] [nvarchar] (max)NULL,");
                tableSqlBuilder.AppendLine(@$"  [created_by] [nvarchar] (100) NULL,");
                tableSqlBuilder.AppendLine(@$"  [created_at] [datetime2](7) NULL CONSTRAINT [PK_com_audit_{DateTime.Now:yyyyMM}] PRIMARY KEY CLUSTERED ([id] ASC) WITH (");
                tableSqlBuilder.AppendLine(@$"    STATISTICS_NORECOMPUTE = OFF,");
                tableSqlBuilder.AppendLine(@$"    IGNORE_DUP_KEY = OFF,");
                tableSqlBuilder.AppendLine(@$"    OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF");
                tableSqlBuilder.AppendLine(@$"  ) ON [PRIMARY]");
                tableSqlBuilder.AppendLine(@$") ON [PRIMARY]");
                tableSqlBuilder.AppendLine(@$"END COMMIT TRAN");

                string createTableSql = tableSqlBuilder.ToString();

                return createTableSql;
            }
        }

        /// <summary>
        /// Gets the insert audit log SQL.
        /// </summary>
        /// <value>
        /// The insert audit log SQL.
        /// </value>
        public string InsertAuditLogSql
        {
            get
            {
                if (_rows != null && _rows.Any())
                {
                    StringBuilder sqlDataBuilder = new();
                    sqlDataBuilder.AppendLine(@$"INSERT INTO com_audit_{DateTime.Now:yyyyMM} ([table_name],[row_id],[action],[row],[version],[created_by],[created_at])");
                    sqlDataBuilder.AppendLine(@$"VALUES");
                    for (int i = 0; i < _rows.Count; i++)
                    {
                        RowAuditLogDocument log = _rows[i];

                        if (i == _rows.Count - 1)
                        {
                            sqlDataBuilder.AppendLine(@$"(N'{log.TableName}',{long.Parse(log.RowId ?? "0")},N'{log.Action}',N'{log.Row.Replace("'","\"\"")}','{DateTime.Now.GetStringTypeTimeStamp()}',N'{log.UpdateBy}',GETDATE());");
                        }
                        else
                        {
                            sqlDataBuilder.AppendLine(@$"(N'{log.TableName}',{long.Parse(log.RowId ?? "0")},N'{log.Action}',N'{log.Row.Replace("'", "\"\"")}','{DateTime.Now.GetStringTypeTimeStamp()}',N'{log.UpdateBy}',GETDATE()),");
                        }
                    }

                    string insertDataSql = sqlDataBuilder.ToString();

                    return insertDataSql;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}