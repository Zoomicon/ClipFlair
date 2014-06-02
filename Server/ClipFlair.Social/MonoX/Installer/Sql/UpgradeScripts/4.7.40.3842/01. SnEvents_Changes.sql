INSERT INTO [dbo].[SnEventType]
           ([Id]
           ,[Name])
     VALUES
           (NewId(),
           'Member posted a wall note')
GO

BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnEvent ADD
	SnNoteId uniqueidentifier NULL
GO
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnNote FOREIGN KEY
	(
	SnNoteId
	) REFERENCES dbo.SnNote
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT



