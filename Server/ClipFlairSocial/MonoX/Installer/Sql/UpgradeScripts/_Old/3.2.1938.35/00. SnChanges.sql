Begin transaction 
BEGIN TRY

	ALTER TABLE SnRelationship ADD
	[BlogId] [uniqueidentifier] NULL
	
	ALTER TABLE [dbo].SnRelationship  WITH CHECK ADD  CONSTRAINT [FK_SnRelationship_Blog] FOREIGN KEY([BlogId])
	REFERENCES [dbo].[Blog] ([Id])



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

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
COMMIT
BEGIN TRANSACTION
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.SnSubscriber
	(
	Id uniqueidentifier NOT NULL,
	RelationshipId uniqueidentifier NOT NULL,
	UserId uniqueidentifier NULL,
	Email nvarchar(250) NULL,
	SubscriptionDate datetime NOT NULL,
	Active bit NOT NULL,
	SubscriptionOrder int NOT NULL IDENTITY (1, 1),
	DateModified datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.SnSubscriber ADD CONSTRAINT
	DF_SnSubscriber_Active DEFAULT 1 FOR Active
GO
ALTER TABLE dbo.SnSubscriber ADD CONSTRAINT
	PK_SnSubscriber PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SnSubscriber ADD CONSTRAINT
	FK_SnSubscriber_SnRelationship FOREIGN KEY
	(
	RelationshipId
	) REFERENCES dbo.SnRelationship
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SnSubscriber ADD CONSTRAINT
	FK_SnSubscriber_aspnet_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT


Begin transaction 
BEGIN TRY

	ALTER TABLE NewsItem DROP COLUMN Rating
	ALTER TABLE NewsItem DROP COLUMN RatingCount
	
	UPDATE NewsItemLocalization 
	SET PublishStart = DateModified
	Where PublishStart is null

	/****** Object:  Index [IX_FilterPeriod]    Script Date: 11/11/2010 22:09:50 ******/
	IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[NewsItemLocalization]') AND name = N'IX_FilterPeriod')
	DROP INDEX [IX_FilterPeriod] ON [dbo].[NewsItemLocalization] WITH ( ONLINE = OFF )

	ALTER TABLE NewsItemLocalization ALTER COLUMN PublishStart datetime NOT NULL

	/****** Object:  Index [IX_FilterPeriod]    Script Date: 11/11/2010 22:10:20 ******/
	CREATE NONCLUSTERED INDEX [IX_FilterPeriod] ON [dbo].[NewsItemLocalization] 
	(
		[PublishStart] ASC,
		[PublishEnd] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


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

