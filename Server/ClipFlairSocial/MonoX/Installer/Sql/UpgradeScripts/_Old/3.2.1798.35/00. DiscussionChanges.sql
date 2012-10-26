Begin transaction 
BEGIN TRY


	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnDiscussionBoardModerator_aspnet_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnDiscussionBoardModerator]'))
	ALTER TABLE [dbo].[SnDiscussionBoardModerator] DROP CONSTRAINT [FK_SnDiscussionBoardModerator_aspnet_Users]
	
	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnDiscussionBoardModerator_SnDiscussionBoard]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnDiscussionBoardModerator]'))
	ALTER TABLE [dbo].[SnDiscussionBoardModerator] DROP CONSTRAINT [FK_SnDiscussionBoardModerator_SnDiscussionBoard]
	
	/****** Object:  Table [dbo].[SnDiscussionBoardModerator]    Script Date: 10/06/2010 20:26:41 ******/
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SnDiscussionBoardModerator]') AND type in (N'U'))
	DROP TABLE [dbo].[SnDiscussionBoardModerator]
	
	/****** Object:  Table [dbo].[SnDiscussionBoardModerator]    Script Date: 10/06/2010 20:26:41 ******/
	SET ANSI_NULLS ON
	
	SET QUOTED_IDENTIFIER ON
	

	CREATE TABLE [dbo].[SnDiscussionBoardModerator](
		[Id] [uniqueidentifier] NOT NULL,
		[SnDiscussionBoardId] [uniqueidentifier] NOT NULL,
		[UserId] [uniqueidentifier] NOT NULL,
	 CONSTRAINT [PK_SnDiscussionBoardModerator] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	

	ALTER TABLE [dbo].[SnDiscussionBoardModerator]  WITH CHECK ADD  CONSTRAINT [FK_SnDiscussionBoardModerator_aspnet_Users] FOREIGN KEY([UserId])
	REFERENCES [dbo].[aspnet_Users] ([UserId])
	

	ALTER TABLE [dbo].[SnDiscussionBoardModerator] CHECK CONSTRAINT [FK_SnDiscussionBoardModerator_aspnet_Users]
	

	ALTER TABLE [dbo].[SnDiscussionBoardModerator]  WITH CHECK ADD  CONSTRAINT [FK_SnDiscussionBoardModerator_SnDiscussionBoard] FOREIGN KEY([SnDiscussionBoardId])
	REFERENCES [dbo].[SnDiscussionBoard] ([Id])
	ON DELETE CASCADE
	

	ALTER TABLE [dbo].[SnDiscussionBoardModerator] CHECK CONSTRAINT [FK_SnDiscussionBoardModerator_SnDiscussionBoard]
	

	INSERT INTO [SnDiscussionBoardModerator]
           ([Id]
           ,[SnDiscussionBoardId]
           ,[UserId])
     Select NewId() as Id, Id as SnDiscussionBoardId, UserId from SnDiscussionBoard

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

	
	
ALTER TABLE SnDiscussionTopic ADD
	[Ip] [nvarchar](50) NULL,
	[Referrer] [nvarchar](500) NULL,
	[UserAgent] [nvarchar](255) NULL,
	[IsApproved] [bit] NULL,
	[IsSpam] [bit] NULL,
	[Spaminess] [decimal](18, 0) NULL,
	[Signature] [nvarchar](max) NULL
GO

	UPDATE SnDiscussionTopic
	SET [IsApproved] = '1',
	[IsSpam] = '0', 
	[Spaminess] = '0'
GO

	ALTER TABLE dbo.SnDiscussionTopic ALTER COLUMN	[IsApproved] [bit] NOT NULL
	ALTER TABLE dbo.SnDiscussionTopic ALTER COLUMN	[IsSpam] [bit] NOT NULL
	ALTER TABLE dbo.SnDiscussionTopic ALTER COLUMN	[Spaminess] [decimal](18, 0) NOT NULL	
GO

	ALTER TABLE dbo.SnDiscussionMessage ADD
	[Ip] [nvarchar](50) NULL,
	[Referrer] [nvarchar](500) NULL,
	[UserAgent] [nvarchar](255) NULL,
	[IsApproved] [bit] NULL,
	[IsSpam] [bit] NULL,
	[Spaminess] [decimal](18, 0) NULL,
	[Signature] [nvarchar](max) NULL,
	[IsAnswer] [bit] NULL,
	[AdminAttentionRequired] [bit] NULL
GO
	

	UPDATE dbo.SnDiscussionMessage
	SET [IsApproved] = '1',
	[IsSpam] = '0', 
	[Spaminess] = '0'
GO

	ALTER TABLE dbo.SnDiscussionMessage ALTER COLUMN	[IsApproved] [bit] NOT NULL
	ALTER TABLE dbo.SnDiscussionMessage ALTER COLUMN	[IsSpam] [bit] NOT NULL
	ALTER TABLE dbo.SnDiscussionMessage ALTER COLUMN	[Spaminess] [decimal](18, 0) NOT NULL	
	
	ALTER TABLE dbo.SnDiscussionBoard ADD [DailyReportSentOn] [datetime] NULL
		
	ALTER TABLE dbo.SnDiscussionTopic ADD [TimesViewed] [int] NULL
	
	ALTER TABLE dbo.UserProfile ADD [ReputationScore] [int] NULL