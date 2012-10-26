Begin transaction
BEGIN TRY

	DECLARE @AllRolesRoleId uniqueidentifier
	SET @AllRolesRoleId = (Select Top 1 RoleId From aspnet_Roles where LoweredRoleName = 'all users')

	Delete From NewsCategoryInRole
	where RoleId = @AllRolesRoleId
	
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
