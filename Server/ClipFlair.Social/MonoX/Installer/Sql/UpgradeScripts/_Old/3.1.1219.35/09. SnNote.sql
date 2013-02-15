 /*
   24. ožujak 201017:52:24
   User: 
   Server: APPSERVER\SQLSERVER2005
   Database: MonoX2Test
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
ALTER TABLE dbo.SnNote ADD
	Title nvarchar(500) NULL,
	Rating float(53) NOT NULL CONSTRAINT DF_SnNote_Rating DEFAULT 0
GO
COMMIT
select Has_Perms_By_Name(N'dbo.SnNote', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.SnNote', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.SnNote', 'Object', 'CONTROL') as Contr_Per 