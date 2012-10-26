 
Begin transaction
BEGIN TRY

	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SnDiscussionTopic_PinnedBy_AspnetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnDiscussionTopic]'))
	ALTER TABLE [dbo].[SnDiscussionTopic] DROP CONSTRAINT [FK__SnDiscussionTopic_PinnedBy_AspnetUsers]

	ALTER TABLE [dbo].[SnDiscussionTopic]  WITH CHECK ADD  CONSTRAINT [FK__SnDiscussionTopic_PinnedBy_AspnetUsers] FOREIGN KEY([PinnedByUserId])
	REFERENCES [dbo].[aspnet_Users] ([UserId])

	ALTER TABLE [dbo].[SnDiscussionTopic] CHECK CONSTRAINT [FK__SnDiscussionTopic_PinnedBy_AspnetUsers]	
	
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