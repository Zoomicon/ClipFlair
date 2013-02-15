USE [MonoX2]
GO

ALTER DATABASE [MonoX2] SET RECURSIVE_TRIGGERS ON
GO

BEGIN TRANSACTION
DECLARE @TriggerName VarChar(50)
SET @TriggerName = 'DeleteNavigationItem'

IF OBJECT_ID (@TriggerName, 'TR') IS NOT NULL  
	EXEC ('DROP TRIGGER ' + @TriggerName)

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[DeleteNavigationItem] ON [dbo].[Navigation] FOR DELETE
AS

IF @@rowcount = 0 RETURN

DELETE FROM T
FROM Navigation AS T JOIN deleted AS D
  ON T.ParentId = D.Id
COMMIT



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
ALTER TABLE dbo.Navigation
	DROP CONSTRAINT FK_Navigation_Navigation
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
ALTER TABLE dbo.Navigation
	NOCHECK CONSTRAINT FK_Navigation_Navigation
GO
COMMIT
