-- ------------------------------------------------------------ --
--                                                              --
-- This Sql Script is meant to be executed directly on the      --
-- Database, it's supposed to replace a stored procedure,       --
-- so it can be updated without Migrations.                     --                                                          --
-- ------------------------------------------------------------ --

BEGIN TRAN

IF NOT EXISTS (select top 1 * from sysObjects where Id=OBJECT_ID(N'log_AuditHistories') and xtype='U')
BEGIN
CREATE TABLE [dbo].log_AuditHistories(
	[Id] [long] NOT NULL,
	[RowId] [varchar](50) NOT NULL,
	[EntityName] [varchar](50) NOT NULL,
	[TableName] [varchar](50) NOT NULL,
	[Changes] [nvarchar](max) NOT NULL,
	[Kind] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[CreatedBy] [varchar(100)],
 CONSTRAINT [PK_log_AuditHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[log_AuditHistories] ADD  DEFAULT (newid()) FOR [Id]
END

COMMIT TRAN