/*
   23. travanj 200716:30:39
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
ALTER TABLE dbo.NewsItems
	DROP CONSTRAINT DF_NewsItems_Published
GO
ALTER TABLE dbo.NewsItems
	DROP COLUMN Title, ShortContent, [Content], Published, PublishStart, PublishEnd, MetaDescription, MetaKeywords
GO
COMMIT
