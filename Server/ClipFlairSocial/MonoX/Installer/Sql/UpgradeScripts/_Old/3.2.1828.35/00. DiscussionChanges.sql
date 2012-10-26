Begin transaction 
BEGIN TRY

	ALTER TABLE SnDiscussionMessage ADD
	[AdminAttentionReportedByUserId] [uniqueidentifier] NULL,
	[AdminAttentionReportedOn] [datetime] NULL,
	[AdminAttentionApproved] [bit] NULL
	
	ALTER TABLE [dbo].SnDiscussionMessage  WITH CHECK ADD  CONSTRAINT [FK_SnDiscussionMessage_aspnet_Users_AdminAttentionReportedByUserId] FOREIGN KEY([AdminAttentionReportedByUserId])
	REFERENCES [dbo].[aspnet_Users] ([UserId])

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

