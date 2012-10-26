  
Begin transaction
BEGIN TRY

	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SnDiscussionBoard_PinnedBy_AspnetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnDiscussionBoard]'))
	ALTER TABLE [dbo].[SnDiscussionBoard] DROP CONSTRAINT [FK__SnDiscussionBoard_PinnedBy_AspnetUsers]

	ALTER TABLE [dbo].[SnDiscussionBoard]  WITH CHECK ADD  CONSTRAINT [FK__SnDiscussionBoard_PinnedBy_AspnetUsers] FOREIGN KEY([PinnedByUserId])
	REFERENCES [dbo].[aspnet_Users] ([UserId])

	ALTER TABLE [dbo].[SnDiscussionBoard] CHECK CONSTRAINT [FK__SnDiscussionBoard_PinnedBy_AspnetUsers]	
	
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