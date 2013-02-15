Begin transaction
BEGIN TRY

	ALTER TABLE dbo.SnDiscussionTopic ADD
	IsClosed bit NOT NULL CONSTRAINT DF_SnDiscussionTopic_IsClosed DEFAULT 0
	
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