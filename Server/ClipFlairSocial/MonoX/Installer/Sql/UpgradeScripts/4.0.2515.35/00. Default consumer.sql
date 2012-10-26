Begin transaction
BEGIN TRY

	INSERT INTO oaConsumer
           ([Id]
           ,[Name]
           ,[Secret]
           ,[CallbackUrl]
           ,[CertificateRaw]
           ,[VerificationCodeFormatValue]
           ,[VerificationCodeLength]
           ,[Version])
     VALUES
           ('8608DE51-B2A7-4920-841C-4980A4E58DF8'
           ,'Anonymous'
           ,'anonymous'
           ,NULL
           ,NULL
           ,0
           ,0
           ,1.0)
	
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