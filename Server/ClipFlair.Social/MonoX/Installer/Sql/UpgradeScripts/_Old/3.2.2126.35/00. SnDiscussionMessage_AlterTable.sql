Begin transaction
BEGIN TRY

	ALTER TABLE dbo.SnDiscussionMessage ADD IsDeleteRequested bit NOT NULL CONSTRAINT DF_SnDiscussionMessage_IsDeleteRequested DEFAULT 0
	ALTER TABLE dbo.SnDiscussionMessage ADD	DeleteRequestedOn DateTime NULL 
	ALTER TABLE dbo.SnDiscussionMessage ADD	DeleteRequestedByUserId uniqueidentifier NULL 
	
	ALTER TABLE [dbo].[SnDiscussionMessage]  WITH CHECK ADD  CONSTRAINT [FK__SnDiscussionMessage_DeleteRequest_AspnetUsers] FOREIGN KEY([DeleteRequestedByUserId])
	REFERENCES [dbo].[aspnet_Users] ([UserId])


	ALTER TABLE dbo.SnDiscussionMessage ADD IsStickyPost bit NOT NULL CONSTRAINT DF_SnDiscussionMessage_IsSticky DEFAULT 0
	ALTER TABLE dbo.SnDiscussionMessage ADD	StickyPostMadeOn DateTime NULL 
	ALTER TABLE dbo.SnDiscussionMessage ADD	StickyPostMadeByUserId uniqueidentifier NULL 
	
	ALTER TABLE [dbo].[SnDiscussionMessage]  WITH CHECK ADD  CONSTRAINT [FK__SnDiscussionMessage_Sticky_AspnetUsers] FOREIGN KEY([StickyPostMadeByUserId])
	REFERENCES [dbo].[aspnet_Users] ([UserId])

	DELETE sne FROM dbo.SnEvent as sne
	INNER Join SnFile snf on snf.Id = sne.SnFileId
	INNER Join SnRelationship snr on snr.Id = snf.RelationshipId
	INNER Join SnDiscussionMessage sndm on sndm.Id = snr.DiscussionMessageId
	WHERE sndm.SnDiscussionTopicId is null

	DELETE snf FROM dbo.SnFile as snf
	INNER Join SnRelationship snr on snr.Id = snf.RelationshipId
	INNER Join SnDiscussionMessage sndm on sndm.Id = snr.DiscussionMessageId
	WHERE sndm.SnDiscussionTopicId is null

	DELETE snr FROM dbo.SnRelationship as snr	
	INNER Join SnDiscussionMessage sndm on sndm.Id = snr.DiscussionMessageId
	WHERE sndm.SnDiscussionTopicId is null

	DELETE FROM dbo.SnDiscussionMessage WHERE SnDiscussionTopicId is null

	ALTER TABLE dbo.SnDiscussionMessage ALTER Column SnDiscussionTopicId uniqueidentifier NOT NULL 
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