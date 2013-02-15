ALTER TABLE SnAlbum ADD ApplicationId uniqueidentifier NULL		
GO
ALTER TABLE SnAlbum ADD LanguageId uniqueidentifier NULL
GO

Begin transaction
BEGIN TRY
	
	ALTER TABLE dbo.SnAlbum ADD CONSTRAINT
	FK_SnAlbum_aspnet_Applications FOREIGN KEY
	(
	ApplicationId
	) REFERENCES dbo.aspnet_Applications
	(
	ApplicationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 
	 ALTER TABLE dbo.SnAlbum ADD CONSTRAINT
	FK_SnAlbum_Language FOREIGN KEY
	(
	LanguageId
	) REFERENCES dbo.Language
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 
	 DECLARE @ApplicationId uniqueidentifier
	 SET @ApplicationId = (select TOP 1 ApplicationId from aspnet_Applications)

	 DECLARE @LanguageId uniqueidentifier
	 SET @LanguageId = (select TOP 1 Id from [Language] where DefaultLanguage = 'true')

	UPDATE SnAlbum
	SET ApplicationId = @ApplicationId,
		LanguageId = @LanguageId
	
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

ALTER TABLE SnAlbum ALTER COLUMN ApplicationId uniqueidentifier NOT NULL
GO
ALTER TABLE SnAlbum ALTER COLUMN LanguageId uniqueidentifier NOT NULL
GO 