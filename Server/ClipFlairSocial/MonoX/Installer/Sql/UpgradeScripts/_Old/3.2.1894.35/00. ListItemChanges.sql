Begin transaction 
BEGIN TRY

	ALTER TABLE ListItem ADD
	[DateCreated] [datetime] NULL
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

