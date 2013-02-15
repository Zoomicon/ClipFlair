Begin transaction
BEGIN TRY

	CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_TopicIdDateCreated] ON [dbo].[SnDiscussionMessage] 
	(
		[SnDiscussionTopicId] ASC,
		[DateCreated] ASC
	)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
	
	CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Approve] ON [dbo].[SnDiscussionMessage] 
	(
		[DateCreated] ASC,
		[SnDiscussionTopicId] ASC,
		[IsApproved] ASC,
		[IsSpam] ASC
	) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
	
	CREATE NONCLUSTERED INDEX [IX_aspnet_Users_UserId] ON [dbo].[aspnet_Users] 
	(
		[UserId] ASC,
		[ApplicationId] ASC,
		[LoweredUserName] ASC
	)
	INCLUDE ( [UserName],
	[MobileAlias],
	[IsAnonymous],
	[LastActivityDate]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Answer] ON [dbo].[SnDiscussionMessage] 
	(
		[SnDiscussionTopicId] ASC,
		[IsApproved] ASC,
		[IsAnswer] ASC,
		[Id] ASC
	)
	INCLUDE ( [Message],
	[DateCreated],
	[IsSpam],
	[AdminAttentionRequired],
	[AdminAttentionApproved],
	[IsDeleteRequested],
	[IsDeleteApproved]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

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
	[IsStickyPost],
	[StickyPostMadeOn],
	[StickyPostMadeByUserId],
	[IsDeleteApproved],
	[DeleteDisapprovedReason]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionMessage] ON [dbo].[SnRelationship] 
	(
		[DiscussionMessageId] ASC,
		[Id] ASC
	)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionTopic] ON [dbo].[SnRelationship] 
	(
		[DiscussionTopicId] ASC
	)
	INCLUDE ( [Id],
	[NoteId],
	[FileId],
	[AlbumId],
	[BlogPostId],
	[MessageId],
	[DiscussionMessageId],
	[CustomId1],
	[CustomId2],
	[CustomId3],
	[DiscussionBoardId],
	[DocumentId],
	[NewsItemId],
	[ListItemId],
	[BlogId],
	[ApplicationId],
	[CalendarEventId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [IX_SnTag_RelationshipSortOrder] ON [dbo].[SnTag] 
	(
		[RelationshipId] ASC,
		[SortOrder] ASC
	)
	INCLUDE ( [Id],
	[Tag],
	[Slug]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [IX_SnTag_RelationShip] ON [dbo].[SnTag] 
	(
		[RelationshipId] ASC
	)
	INCLUDE ( [Id],
	[Tag],
	[Slug],
	[SortOrder]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionBoard] ON [dbo].[SnRelationship] 
	(
		[Id] ASC,
		[DiscussionBoardId] ASC
	)
	INCLUDE ( [DiscussionTopicId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Approved] ON [dbo].[SnDiscussionTopic] 
	(
		[DateCreated] DESC,
		[IsSpam] ASC,
		[IsApproved] ASC,
		[UserId] ASC,
		[Id] ASC,
		[SnDiscussionBoardId] ASC
	)
	INCLUDE ( [Title]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]

	CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_AdminAttention] ON [dbo].[SnDiscussionMessage] 
	(
		[SnDiscussionTopicId] ASC,
		[AdminAttentionRequired] ASC,
		[AdminAttentionApproved] ASC,
		[IsSpam] ASC,
		[IsApproved] ASC,
		[UserId] ASC,
		[Id] ASC
	)
	INCLUDE ( [Message]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]	
	
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

 