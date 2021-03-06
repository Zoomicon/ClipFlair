/*
   7. siječanj 200912:11:52
   User: 
   Server: appserver\sqlserver2005
   Database: DirhWebInternet
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
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Navigation
	DROP CONSTRAINT FK_Navigation_Page
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Navigation
	DROP CONSTRAINT DF_Navigation_IsContainerPage
GO
ALTER TABLE dbo.Navigation
	DROP CONSTRAINT DF_Navigation_PageOrder
GO
CREATE TABLE dbo.Tmp_Navigation
	(
	Id uniqueidentifier NOT NULL,
	PageId uniqueidentifier NULL,
	ApplicationId uniqueidentifier NULL,
	ExternalUrl nvarchar(500) NULL,
	IsContainerPage bit NOT NULL,
	ParentId uniqueidentifier NULL,
	PageOrder int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Navigation ADD CONSTRAINT
	DF_Navigation_IsContainerPage DEFAULT ((0)) FOR IsContainerPage
GO
ALTER TABLE dbo.Tmp_Navigation ADD CONSTRAINT
	DF_Navigation_PageOrder DEFAULT ((0)) FOR PageOrder
GO
IF EXISTS(SELECT * FROM dbo.Navigation)
	 EXEC('INSERT INTO dbo.Tmp_Navigation (Id, PageId, ExternalUrl, IsContainerPage, ParentId, PageOrder)
		SELECT Id, PageId, ExternalUrl, IsContainerPage, ParentId, PageOrder FROM dbo.Navigation WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.Navigation
	DROP CONSTRAINT FK_Navigation_Navigation
GO
ALTER TABLE dbo.NavigationLocalization
	DROP CONSTRAINT FK_NavigationLocalization_Navigation
GO
ALTER TABLE dbo.NavigationRole
	DROP CONSTRAINT FK_NavigationRole_Navigation
GO
DROP TABLE dbo.Navigation
GO
EXECUTE sp_rename N'dbo.Tmp_Navigation', N'Navigation', 'OBJECT' 
GO
ALTER TABLE dbo.Navigation ADD CONSTRAINT
	PK_Navigation PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Navigation ADD CONSTRAINT
	FK_Navigation_Navigation FOREIGN KEY
	(
	ParentId
	) REFERENCES dbo.Navigation
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Navigation ADD CONSTRAINT
	FK_Navigation_Page FOREIGN KEY
	(
	PageId
	) REFERENCES dbo.Page
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.Navigation ADD CONSTRAINT
	FK_Navigation_aspnet_Applications FOREIGN KEY
	(
	ApplicationId
	) REFERENCES dbo.aspnet_Applications
	(
	ApplicationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.NavigationRole ADD CONSTRAINT
	FK_NavigationRole_Navigation FOREIGN KEY
	(
	NavigationId
	) REFERENCES dbo.Navigation
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.NavigationLocalization ADD CONSTRAINT
	FK_NavigationLocalization_Navigation FOREIGN KEY
	(
	NavigationId
	) REFERENCES dbo.Navigation
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT
