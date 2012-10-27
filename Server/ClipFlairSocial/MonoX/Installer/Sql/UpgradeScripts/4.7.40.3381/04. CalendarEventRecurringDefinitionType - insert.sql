Begin transaction
BEGIN TRY

	INSERT INTO CalendarEventRecurringDefinitionType
           ([Id]
           ,[Name]
		   ,[Abrv])
     VALUES
           (NEWID()
           ,'Weekly'
		   ,'Weekly')

	INSERT INTO CalendarEventRecurringDefinitionType
           ([Id]
           ,[Name]
		   ,[Abrv])
     VALUES
           (NEWID()
           ,'Monthly'
		   ,'Monthly')

	INSERT INTO CalendarEventRecurringDefinitionType
           ([Id]
           ,[Name]
		   ,[Abrv])
     VALUES
           (NEWID()
           ,'Yearly'
		   ,'Yearly')

	
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




