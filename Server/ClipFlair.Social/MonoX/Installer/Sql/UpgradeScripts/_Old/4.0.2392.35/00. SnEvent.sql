Begin transaction
BEGIN TRY

	/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_SnFile

ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_BlogPost

ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_SnDiscussionTopic

ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_SnDiscussionBoard

ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_SnEventType

ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_aspnet_Users_UserId

ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_aspnet_Users_FriendId

ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_SnGroup

ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_SnAlbum

CREATE TABLE dbo.Tmp_SnEvent
	(
	Id uniqueidentifier NOT NULL,
	UserId uniqueidentifier NOT NULL,
	EventTypeId uniqueidentifier NOT NULL,
	DateCreated datetime NOT NULL,
	FriendId uniqueidentifier NULL,
	BlogPostId uniqueidentifier NULL,
	SnGroupId uniqueidentifier NULL,
	SnAlbumId uniqueidentifier NULL,
	SnFileId uniqueidentifier NULL,
	SnDiscussionBoardId uniqueidentifier NULL,
	SnDiscussionTopicId uniqueidentifier NULL,
	OaConsumerId uniqueidentifier NULL,
	CustomId1 uniqueidentifier NULL,
	CustomId2 uniqueidentifier NULL,
	CustomId3 uniqueidentifier NULL,
	EventContent nvarchar(500) NULL,
	PlainEventTitle nvarchar(150) NULL,
	PlainEventUrl nvarchar(500) NULL
	)  ON [PRIMARY]

IF EXISTS(SELECT * FROM dbo.SnEvent)
	 EXEC('INSERT INTO dbo.Tmp_SnEvent (Id, UserId, EventTypeId, DateCreated, FriendId, BlogPostId, SnGroupId, SnAlbumId, SnFileId, SnDiscussionBoardId, SnDiscussionTopicId, CustomId1, CustomId2, CustomId3, EventContent, PlainEventTitle, PlainEventUrl)
		SELECT Id, UserId, EventTypeId, DateCreated, FriendId, BlogPostId, SnGroupId, SnAlbumId, SnFileId, SnDiscussionBoardId, SnDiscussionTopicId, CustomId1, CustomId2, CustomId3, EventContent, PlainEventTitle, PlainEventUrl FROM dbo.SnEvent WITH (HOLDLOCK TABLOCKX)')

DROP TABLE dbo.SnEvent

EXECUTE sp_rename N'dbo.Tmp_SnEvent', N'SnEvent', 'OBJECT' 

ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	PK_SnEvent PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX IX_SnEvent_UserId ON dbo.SnEvent
	(
	UserId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX IX_SnEvent_EventTypeId ON dbo.SnEvent
	(
	EventTypeId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnAlbum FOREIGN KEY
	(
	SnAlbumId
	) REFERENCES dbo.SnAlbum
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnGroup FOREIGN KEY
	(
	SnGroupId
	) REFERENCES dbo.SnGroup
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_aspnet_Users_UserId FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnEventType FOREIGN KEY
	(
	EventTypeId
	) REFERENCES dbo.SnEventType
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_aspnet_Users_FriendId FOREIGN KEY
	(
	FriendId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnDiscussionBoard FOREIGN KEY
	(
	SnDiscussionBoardId
	) REFERENCES dbo.SnDiscussionBoard
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnDiscussionTopic FOREIGN KEY
	(
	SnDiscussionTopicId
	) REFERENCES dbo.SnDiscussionTopic
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_BlogPost FOREIGN KEY
	(
	BlogPostId
	) REFERENCES dbo.BlogPost
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnFile FOREIGN KEY
	(
	SnFileId
	) REFERENCES dbo.SnFile
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_oaConsumer FOREIGN KEY
	(
	OaConsumerId
	) REFERENCES dbo.oaConsumer
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
COMMIT TRANSACTION                              
END TRY

BEGIN CATCH
       SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;     
       ROLLBACK TRANSACTION
END CATCH   	