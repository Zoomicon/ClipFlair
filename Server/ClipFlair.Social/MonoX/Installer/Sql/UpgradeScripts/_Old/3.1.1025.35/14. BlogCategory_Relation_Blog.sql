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
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BlogCategory
	DROP CONSTRAINT FK_BlogCategory_Language
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BlogCategory
	DROP CONSTRAINT FK_BlogCategory_aspnet_Applications
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_BlogCategory
	(
	Id uniqueidentifier NOT NULL,
	ApplicationId uniqueidentifier NOT NULL,
	LanguageId uniqueidentifier NOT NULL,
	BlogId uniqueidentifier NULL,
	Name nvarchar(255) NOT NULL,
	Slug nvarchar(255) NOT NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.BlogCategory)
	 EXEC('INSERT INTO dbo.Tmp_BlogCategory (Id, ApplicationId, LanguageId, Name, Slug)
		SELECT Id, ApplicationId, LanguageId, Name, Slug FROM dbo.BlogCategory WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.BlogPostCategory
	DROP CONSTRAINT FK_BlogPostCategory_BlogCategory
GO
DROP TABLE dbo.BlogCategory
GO
EXECUTE sp_rename N'dbo.Tmp_BlogCategory', N'BlogCategory', 'OBJECT' 
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
ALTER TABLE dbo.BlogCategory ADD CONSTRAINT
	FK_BlogCategory_Blog FOREIGN KEY
	(
	BlogId
	) REFERENCES dbo.Blog
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BlogPostCategory ADD CONSTRAINT
	FK_BlogPostCategory_BlogCategory FOREIGN KEY
	(
	BlogCategoryId
	) REFERENCES dbo.BlogCategory
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT

update BlogCategory 
SET BlogId = (

	select Top 1 b.Id from blog b 
	where b.ApplicationId = ApplicationId 
	and b.LanguageId = LanguageId
	)

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
ALTER TABLE dbo.BlogCategory
	DROP CONSTRAINT FK_BlogCategory_Blog
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BlogCategory
	DROP CONSTRAINT FK_BlogCategory_Language
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BlogCategory
	DROP CONSTRAINT FK_BlogCategory_aspnet_Applications
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_BlogCategory
	(
	Id uniqueidentifier NOT NULL,
	BlogId uniqueidentifier NOT NULL,
	Name nvarchar(255) NOT NULL,
	Slug nvarchar(255) NOT NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.BlogCategory)
	 EXEC('INSERT INTO dbo.Tmp_BlogCategory (Id, BlogId, Name, Slug)
		SELECT Id, BlogId, Name, Slug FROM dbo.BlogCategory WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.BlogPostCategory
	DROP CONSTRAINT FK_BlogPostCategory_BlogCategory
GO
DROP TABLE dbo.BlogCategory
GO
EXECUTE sp_rename N'dbo.Tmp_BlogCategory', N'BlogCategory', 'OBJECT' 
GO
ALTER TABLE dbo.BlogCategory ADD CONSTRAINT
	PK_BlogCategory PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.BlogCategory ADD CONSTRAINT
	FK_BlogCategory_Blog FOREIGN KEY
	(
	BlogId
	) REFERENCES dbo.Blog
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.BlogPostCategory ADD CONSTRAINT
	FK_BlogPostCategory_BlogCategory FOREIGN KEY
	(
	BlogCategoryId
	) REFERENCES dbo.BlogCategory
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT