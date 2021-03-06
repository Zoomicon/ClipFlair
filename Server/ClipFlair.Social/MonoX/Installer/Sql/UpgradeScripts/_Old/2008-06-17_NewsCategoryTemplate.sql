/*
   17. lipanj 200811:58:29
   User: 
   Server: APPSERVER\SQLSERVER2005
   Database: MonoX2
   Application: 
*/

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
ALTER TABLE dbo.NewsCategoryTemplate
	DROP CONSTRAINT FK_NewsCategoryTemplate_NewsCategory
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.NewsCategoryTemplate
	DROP CONSTRAINT DF_NewsCategoryTemplate_Id
GO
CREATE TABLE dbo.Tmp_NewsCategoryTemplate
	(
	Id uniqueidentifier NOT NULL ROWGUIDCOL,
	NewsCategoryId uniqueidentifier NOT NULL,
	TemplateName nvarchar(100) NOT NULL,
	TemplateNameFullContent nvarchar(100) NOT NULL,
	DateEntered datetime NULL,
	DateModified datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_NewsCategoryTemplate ADD CONSTRAINT
	DF_NewsCategoryTemplate_Id DEFAULT (newid()) FOR Id
GO
IF EXISTS(SELECT * FROM dbo.NewsCategoryTemplate)
	 EXEC('INSERT INTO dbo.Tmp_NewsCategoryTemplate (Id, NewsCategoryId, TemplateName)
		SELECT Id, NewsCategoryId, TemplateName FROM dbo.NewsCategoryTemplate WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.NewsCategoryTemplate
GO
EXECUTE sp_rename N'dbo.Tmp_NewsCategoryTemplate', N'NewsCategoryTemplate', 'OBJECT' 
GO
ALTER TABLE dbo.NewsCategoryTemplate ADD CONSTRAINT
	PK_NewsCategoryTemplate PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.NewsCategoryTemplate ADD CONSTRAINT
	FK_NewsCategoryTemplate_NewsCategory FOREIGN KEY
	(
	NewsCategoryId
	) REFERENCES dbo.NewsCategory
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT
