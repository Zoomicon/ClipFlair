Begin transaction
BEGIN TRY

ALTER TABLE dbo.SnRelationship ADD
	GroupId uniqueidentifier NULL,
	UserId uniqueidentifier NULL,
	CampaignId uniqueidentifier NULL,
	NewsCategoryId uniqueidentifier NULL,
	NewsletterId uniqueidentifier NULL,
	PageId uniqueidentifier NULL,
	PollId uniqueidentifier NULL

ALTER TABLE dbo.SnRelationship ADD CONSTRAINT
	FK_SnRelationship_SnGroup FOREIGN KEY
	(
	GroupId
	) REFERENCES dbo.SnGroup
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

ALTER TABLE dbo.SnRelationship ADD CONSTRAINT
	FK_SnRelationship_aspnet_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

ALTER TABLE dbo.SnRelationship ADD CONSTRAINT
	FK_SnRelationship_Campaign FOREIGN KEY
	(
	CampaignId
	) REFERENCES dbo.Campaign
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

ALTER TABLE dbo.SnRelationship ADD CONSTRAINT
	FK_SnRelationship_NewsCategory FOREIGN KEY
	(
	NewsCategoryId
	) REFERENCES dbo.NewsCategory
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

ALTER TABLE dbo.SnRelationship ADD CONSTRAINT
	FK_SnRelationship_Newsletter FOREIGN KEY
	(
	NewsletterId
	) REFERENCES dbo.Newsletter
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

ALTER TABLE dbo.SnRelationship ADD CONSTRAINT
	FK_SnRelationship_Page FOREIGN KEY
	(
	PageId
	) REFERENCES dbo.Page
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

ALTER TABLE dbo.SnRelationship ADD CONSTRAINT
	FK_SnRelationship_Poll FOREIGN KEY
	(
	PollId
	) REFERENCES dbo.Poll
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

	
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



