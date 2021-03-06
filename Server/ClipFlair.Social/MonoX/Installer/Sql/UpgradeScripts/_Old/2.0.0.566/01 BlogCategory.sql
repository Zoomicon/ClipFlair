/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.BlogCategory
	(
	Id uniqueidentifier NOT NULL,
	ApplicationId uniqueidentifier NOT NULL,
	LanguageId uniqueidentifier NOT NULL,
	Name nvarchar(255) NOT NULL,
	Slug nvarchar(255) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.BlogCategory ADD CONSTRAINT
	PK_BlogCategory PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.BlogCategory ADD CONSTRAINT
	IX_BlogCategory UNIQUE NONCLUSTERED 
	(
	ApplicationId,
	LanguageId,
	Name
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.BlogCategory ADD CONSTRAINT
	FK_BlogCategory_aspnet_Applications FOREIGN KEY
	(
	ApplicationId
	) REFERENCES dbo.aspnet_Applications
	(
	ApplicationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION
	
GO

GO
ALTER TABLE dbo.BlogCategory ADD CONSTRAINT
	FK_BlogCategory_Language FOREIGN KEY
	(
	LanguageId
	) REFERENCES dbo.Language
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

COMMIT

