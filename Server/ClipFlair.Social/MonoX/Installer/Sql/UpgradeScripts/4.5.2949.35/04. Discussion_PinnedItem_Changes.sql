 
Begin transaction
BEGIN TRY

	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__SnDiscussionMessage_PinnedBy_AspnetUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnDiscussionMessage]'))
	ALTER TABLE [dbo].[SnDiscussionMessage] DROP CONSTRAINT [FK__SnDiscussionMessage_PinnedBy_AspnetUsers]

	ALTER TABLE [dbo].[SnDiscussionMessage]  WITH CHECK ADD  CONSTRAINT [FK__SnDiscussionMessage_PinnedBy_AspnetUsers] FOREIGN KEY([PinnedByUserId])
	REFERENCES [dbo].[aspnet_Users] ([UserId])

	ALTER TABLE [dbo].[SnDiscussionMessage] CHECK CONSTRAINT [FK__SnDiscussionMessage_PinnedBy_AspnetUsers]	

	UPDATE [dbo].[SnDiscussionMessage]	
	SET [IsPinned] = [IsStickyPost]
      ,[PinnedOn] = [StickyPostMadeOn]
      ,[PinnedByUserId] = [StickyPostMadeByUserId]
	FROM [dbo].[SnDiscussionMessage]	

	/****** Object:  Index [IX_SnDiscussionMessage_Modified]    Script Date: 09/07/2011 11:54:46 ******/
	DROP INDEX [IX_SnDiscussionMessage_Modified] ON [dbo].[SnDiscussionMessage] WITH ( ONLINE = OFF )
	CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Modified] ON [dbo].[SnDiscussionMessage] 
	(
		[DateCreated] ASC,
		[IsSpam] ASC,
		[IsApproved] ASC,
		[SnDiscussionTopicId] ASC,
		[Id] ASC,
		[UserId] ASC,
		[LastModifiedDate] ASC,
		[Ip] ASC
	)
	INCLUDE ( [Message],
	[Referrer],
	[UserAgent],
	[Spaminess],
	[Signature],
	[IsAnswer],
	[AdminAttentionRequired],
	[AdminAttentionReportedByUserId],
	[AdminAttentionReportedOn],
	[AdminAttentionApproved],
	[AdminAttentionReason],
	[IsDeleteRequested],
	[DeleteRequestedOn],
	[DeleteRequestedByUserId],
	[IsDeleteApproved],
	[DeleteDisapprovedReason]) WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]


	ALTER TABLE dbo.SnDiscussionMessage DROP CONSTRAINT FK__SnDiscussionMessage_Sticky_AspnetUsers	
	IF EXISTS (SELECT name FROM sys.stats WHERE name = N'_WA_Sys_00000019_797DF6D1' AND object_id = OBJECT_ID(N'SnDiscussionMessage'))
		DROP STATISTICS dbo.SnDiscussionMessage._WA_Sys_00000019_797DF6D1 
	IF EXISTS (SELECT name FROM sys.stats WHERE name = N'_WA_Sys_0000001A_797DF6D1' AND object_id = OBJECT_ID(N'SnDiscussionMessage'))
		DROP STATISTICS dbo.SnDiscussionMessage._WA_Sys_0000001A_797DF6D1 
	IF EXISTS (SELECT name FROM sys.stats WHERE name = N'_WA_Sys_0000001B_797DF6D1' AND object_id = OBJECT_ID(N'SnDiscussionMessage'))
		DROP STATISTICS dbo.SnDiscussionMessage._WA_Sys_0000001B_797DF6D1 
	ALTER TABLE dbo.SnDiscussionMessage DROP CONSTRAINT DF_SnDiscussionMessage_IsSticky
	ALTER TABLE dbo.SnDiscussionMessage	DROP COLUMN IsStickyPost
	ALTER TABLE dbo.SnDiscussionMessage	DROP COLUMN StickyPostMadeOn
	ALTER TABLE dbo.SnDiscussionMessage	DROP COLUMN StickyPostMadeByUserId
	
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