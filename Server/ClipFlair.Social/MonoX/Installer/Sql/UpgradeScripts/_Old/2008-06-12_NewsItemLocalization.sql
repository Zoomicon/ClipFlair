/*
   12. lipanj 200811:24:42
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
ALTER TABLE dbo.NewsItemLocalization
	DROP CONSTRAINT FK_NewsItemLocalization_Language
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.NewsItemLocalization
	DROP CONSTRAINT FK_NewsItemsLocalization_NewsItems
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.NewsItemLocalization
	DROP CONSTRAINT DF_NewsItemsLocalization_Id
GO
ALTER TABLE dbo.NewsItemLocalization
	DROP CONSTRAINT DF_NewsItemsLocalization_Published
GO
CREATE TABLE dbo.Tmp_NewsItemLocalization
	(
	Id uniqueidentifier NOT NULL ROWGUIDCOL,
	NewsId uniqueidentifier NOT NULL,
	LanguageId uniqueidentifier NOT NULL,
	Title nvarchar(300) NULL,
	ShortContent nvarchar(MAX) NULL,
	[Content] ntext NULL,
	Published bit NOT NULL,
	PublishStart datetime NULL,
	PublishEnd datetime NULL,
	MetaDescription nvarchar(255) NULL,
	MetaKeywords nvarchar(255) NULL,
	DateEntered datetime NULL,
	DateModified datetime NULL,
	VisibleDate datetime NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_NewsItemLocalization ADD CONSTRAINT
	DF_NewsItemsLocalization_Id DEFAULT (newid()) FOR Id
GO
ALTER TABLE dbo.Tmp_NewsItemLocalization ADD CONSTRAINT
	DF_NewsItemsLocalization_Published DEFAULT ((1)) FOR Published
GO
IF EXISTS(SELECT * FROM dbo.NewsItemLocalization)
	 EXEC('INSERT INTO dbo.Tmp_NewsItemLocalization (Id, NewsId, LanguageId, Title, ShortContent, [Content], Published, PublishStart, PublishEnd, MetaDescription, MetaKeywords, DateEntered, DateModified, VisibleDate)
		SELECT Id, NewsId, LanguageId, Title, ShortContent, [Content], Published, PublishStart, PublishEnd, MetaDescription, MetaKeywords, DateEntered, DateModified, VisibleDate FROM dbo.NewsItemLocalization WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.NewsItemLocalization
GO
EXECUTE sp_rename N'dbo.Tmp_NewsItemLocalization', N'NewsItemLocalization', 'OBJECT' 
GO
ALTER TABLE dbo.NewsItemLocalization ADD CONSTRAINT
	PK_NewsItemsLocalization PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.NewsItemLocalization ADD CONSTRAINT
	FK_NewsItemsLocalization_NewsItems FOREIGN KEY
	(
	NewsId
	) REFERENCES dbo.NewsItem
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.NewsItemLocalization ADD CONSTRAINT
	FK_NewsItemLocalization_Language FOREIGN KEY
	(
	LanguageId
	) REFERENCES dbo.Language
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT
