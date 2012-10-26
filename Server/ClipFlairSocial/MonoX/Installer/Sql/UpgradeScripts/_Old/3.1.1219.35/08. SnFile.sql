/*
   24. ožujak 201017:50:29
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
ALTER TABLE dbo.SnFile
	DROP CONSTRAINT FK_SnFile_BlogPost
GO
COMMIT
select Has_Perms_By_Name(N'dbo.BlogPost', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.BlogPost', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.BlogPost', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnFile
	DROP CONSTRAINT FK_SnFile_SnDiscussionMessage
GO
COMMIT
select Has_Perms_By_Name(N'dbo.SnDiscussionMessage', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.SnDiscussionMessage', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.SnDiscussionMessage', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnFile
	DROP CONSTRAINT FK_SnFile_SnNote
GO
COMMIT
select Has_Perms_By_Name(N'dbo.SnNote', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.SnNote', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.SnNote', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnFile
	DROP CONSTRAINT FK_SnFile_SnMessage
GO
COMMIT
select Has_Perms_By_Name(N'dbo.SnMessage', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.SnMessage', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.SnMessage', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnFile ADD
	Rating float(53) NOT NULL CONSTRAINT DF_SnFile_Rating DEFAULT 0
GO
ALTER TABLE dbo.SnFile
	DROP COLUMN MessageId, NoteId, BlogPostId, SnDiscussionMessageId, CustomId1, CustomId2, CustomId3
GO
COMMIT
select Has_Perms_By_Name(N'dbo.SnFile', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.SnFile', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.SnFile', 'Object', 'CONTROL') as Contr_Per  