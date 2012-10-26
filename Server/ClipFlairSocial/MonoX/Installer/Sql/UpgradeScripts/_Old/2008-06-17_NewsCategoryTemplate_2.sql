/*
   17. lipanj 200812:15:45
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
ALTER TABLE dbo.NewsCategoryTemplate ADD CONSTRAINT
	FK_NewsCategoryTemplate_NewsCategory1 FOREIGN KEY
	(
	Id
	) REFERENCES dbo.NewsCategory
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.NewsCategoryTemplate
	DROP COLUMN NewsCategoryId
GO
COMMIT
