Begin transaction
BEGIN TRY

	ALTER TABLE dbo.SnDiscussionBoard ADD IsPinned bit NOT NULL CONSTRAINT DF_SnDiscussionBoard_IsPinned DEFAULT 0
	ALTER TABLE dbo.SnDiscussionBoard ADD PinnedOn datetime NULL
	ALTER TABLE dbo.SnDiscussionBoard ADD PinnedByUserId uniqueidentifier NULL

	ALTER TABLE dbo.SnDiscussionTopic ADD IsPinned bit NOT NULL CONSTRAINT DF_SnDiscussionTopic_IsPinned DEFAULT 0
	ALTER TABLE dbo.SnDiscussionTopic ADD PinnedOn datetime NULL
	ALTER TABLE dbo.SnDiscussionTopic ADD PinnedByUserId uniqueidentifier NULL

	ALTER TABLE dbo.SnDiscussionMessage ADD IsPinned bit NOT NULL CONSTRAINT DF_SnDiscussionMessage_IsPinned DEFAULT 0
	ALTER TABLE dbo.SnDiscussionMessage ADD	PinnedOn datetime NULL
	ALTER TABLE dbo.SnDiscussionMessage ADD	PinnedByUserId uniqueidentifier NULL
		
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
