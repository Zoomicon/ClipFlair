Begin transaction
BEGIN TRY

	ALTER TABLE SnEvent ADD PlainEventTitle  nvarchar(150) NULL
	ALTER TABLE SnEvent ADD PlainEventBody  nvarchar(500) NULL	
	ALTER TABLE SnEvent ADD PlainEventUrl  nvarchar(500) NULL
	INSERT INTO [SnEventType]
           ([Id]
           ,[Name])
     VALUES
           (NewId()
           ,'plain event')

	ALTER TABLE SnRelationship ADD
	[ApplicationId] [uniqueidentifier] NULL
	
	ALTER TABLE [dbo].SnRelationship  WITH CHECK ADD  CONSTRAINT [FK_SnRelationship_aspnet_Applications] FOREIGN KEY([ApplicationId])
	REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])

	ALTER TABLE dbo.SnDiscussionBoard ADD
	IsClosed bit NOT NULL CONSTRAINT DF_SnDiscussionBoard_IsClosed DEFAULT 0

CREATE TABLE dbo.[Version]
	(
	Id uniqueidentifier NOT NULL,
	[Version] nvarchar(50) NOT NULL,
	Comment nvarchar(500) NULL,
	UpgradeLog nvarchar(MAX) NULL,
	DateEntered datetime NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]

ALTER TABLE dbo.[Version] ADD CONSTRAINT
	DF_Version_DateEntered DEFAULT getdate() FOR DateEntered

ALTER TABLE dbo.[Version] ADD CONSTRAINT
	PK_Version PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	
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