Begin transaction
BEGIN TRY

	DECLARE @PrivacyLevelFriendsId uniqueidentifier
	SET @PrivacyLevelFriendsId = (select Top 1 Id from PrivacyLevel where PrivacyLevel = 'Friends')

	DECLARE @PrivacyLevelPublicId uniqueidentifier
	SET @PrivacyLevelPublicId = (select Top 1 Id from PrivacyLevel where PrivacyLevel = 'Public')

	UPDATE SnFile	
	SET [PrivacyLevelId] = @PrivacyLevelPublicId      		
	FROM SnFile f
	INNER JOIN SnRelationship r on f.RelationshipId = r.Id
	WHERE 
		[PrivacyLevelId] = @PrivacyLevelFriendsId
	AND
		r.AlbumId is not null
	
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