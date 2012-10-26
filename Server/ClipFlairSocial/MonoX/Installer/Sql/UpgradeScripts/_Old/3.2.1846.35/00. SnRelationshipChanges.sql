Begin transaction 
BEGIN TRY

	ALTER TABLE SnRelationship ADD
	[DiscussionBoardId] [uniqueidentifier] NULL,
	[DiscussionTopicId] [uniqueidentifier] NULL,
	[DocumentId] [uniqueidentifier] NULL,
	[NewsItemId] [uniqueidentifier] NULL
	
	ALTER TABLE [dbo].SnRelationship  WITH CHECK ADD  CONSTRAINT [FK_SnRelationship_SnDiscussionBoard] FOREIGN KEY([DiscussionBoardId])
	REFERENCES [dbo].[SnDiscussionBoard] ([Id])

	ALTER TABLE [dbo].SnRelationship  WITH CHECK ADD  CONSTRAINT [FK_SnRelationship_SnDiscussionTopic] FOREIGN KEY([DiscussionTopicId])
	REFERENCES [dbo].[SnDiscussionTopic] ([Id])

	ALTER TABLE [dbo].SnRelationship  WITH CHECK ADD  CONSTRAINT [FK_SnRelationship_Document] FOREIGN KEY([DocumentId])
	REFERENCES [dbo].[Document] ([Id])

	ALTER TABLE [dbo].SnRelationship  WITH CHECK ADD  CONSTRAINT [FK_SnRelationship_NewsItem] FOREIGN KEY([NewsItemId])
	REFERENCES [dbo].[NewsItem] ([Id])


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

