Begin transaction
BEGIN TRY

--SQL goes here 

CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Opt1] ON [dbo].[SnDiscussionMessage] 
(
	[DateCreated] ASC,
	[IsSpam] ASC,
	[IsApproved] ASC,
	[SnDiscussionTopicId] ASC,
	[Id] ASC
)
INCLUDE ( [UserId],
[Message],
[LastModifiedDate],
[Ip],
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
[DeleteDisapprovedReason],
[IsPinned],
[PinnedOn],
[PinnedByUserId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Answer_Opt1] ON [dbo].[SnDiscussionMessage] 
(
	[SnDiscussionTopicId] ASC,
	[IsApproved] ASC,
	[IsAnswer] ASC
)
INCLUDE ( [Id]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Approval_Opt1] ON [dbo].[SnDiscussionMessage] 
(
	[IsApproved] ASC,
	[IsSpam] ASC,
	[SnDiscussionTopicId] ASC
)
INCLUDE ( [Message]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_User_Opt1] ON [dbo].[SnDiscussionMessage] 
(
	[UserId] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionMessage_Id_Opt1] ON [dbo].[SnDiscussionMessage] 
(
	[Id] ASC
)
INCLUDE ( [Message]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]



CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionMessage_Opt1] ON [dbo].[SnRelationship] 
(
	[DiscussionMessageId] ASC
)
INCLUDE ( [Id],
[NoteId],
[FileId],
[AlbumId],
[BlogPostId],
[MessageId],
[CustomId1],
[CustomId2],
[CustomId3],
[DiscussionBoardId],
[DiscussionTopicId],
[DocumentId],
[NewsItemId],
[ListItemId],
[BlogId],
[ApplicationId],
[CalendarEventId],
[GroupId],
[UserId],
[CampaignId],
[NewsCategoryId],
[NewsletterId],
[PageId],
[PollId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnRelationship_DiscussionTopic_Opt1] ON [dbo].[SnRelationship] 
(
	[DiscussionTopicId] ASC,
	[Id] ASC
)
INCLUDE ( [NoteId],
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
[CalendarEventId],
[GroupId],
[UserId],
[CampaignId],
[NewsCategoryId],
[NewsletterId],
[PageId],
[PollId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnRelationship_BlogPost_Opt1] ON [dbo].[SnRelationship] 
(
	[BlogPostId] ASC
)
INCLUDE ( [Id],
[NoteId],
[FileId],
[AlbumId],
[MessageId],
[DiscussionMessageId],
[CustomId1],
[CustomId2],
[CustomId3],
[DiscussionBoardId],
[DiscussionTopicId],
[DocumentId],
[NewsItemId],
[ListItemId],
[BlogId],
[ApplicationId],
[CalendarEventId],
[GroupId],
[UserId],
[CampaignId],
[NewsCategoryId],
[NewsletterId],
[PageId],
[PollId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnRelationship_BlogPost2_Opt1] ON [dbo].[SnRelationship] 
(
	[BlogPostId] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]



CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_DateCreated_Opt1] ON [dbo].[SnDiscussionTopic] 
(
	[DateCreated] DESC
)
INCLUDE ( [Id],
[SnDiscussionBoardId],
[Title],
[UserId],
[Ip],
[Referrer],
[UserAgent],
[IsApproved],
[IsSpam],
[Spaminess],
[Signature],
[TimesViewed],
[IsClosed],
[IsPinned],
[PinnedOn],
[PinnedByUserId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Board_Opt1] ON [dbo].[SnDiscussionTopic] 
(
	[SnDiscussionBoardId] ASC
)
INCLUDE ( [Id],
[Title],
[UserId],
[DateCreated],
[Ip],
[Referrer],
[UserAgent],
[IsApproved],
[IsSpam],
[Spaminess],
[Signature],
[TimesViewed],
[IsClosed],
[IsPinned],
[PinnedOn],
[PinnedByUserId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Board2_Opt1] ON [dbo].[SnDiscussionTopic] 
(
	[SnDiscussionBoardId] ASC
)
INCLUDE ( [Id],
[Title],
[UserId],
[IsApproved],
[IsSpam]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Pinned_Opt1] ON [dbo].[SnDiscussionTopic] 
(
	[SnDiscussionBoardId] ASC,
	[IsApproved] ASC,
	[IsPinned] ASC
)
INCLUDE ( [Id],
[IsSpam]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_ApproveSpam_Opt1] ON [dbo].[SnDiscussionTopic] 
(
	[IsApproved] ASC,
	[SnDiscussionBoardId] ASC,
	[IsSpam] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_Spam_Opt1] ON [dbo].[SnDiscussionTopic] 
(
	[SnDiscussionBoardId] ASC,
	[IsSpam] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_SnDiscussionTopic_User_Opt1] ON [dbo].[SnDiscussionTopic] 
(
	[UserId] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]



CREATE NONCLUSTERED INDEX [IX_Document_Page_Opt1] ON [dbo].[Document] 
(
	[LanguageId] ASC,
	[BackupVersion] ASC,
	[RevisionVersion] ASC,
	[PageId] ASC
)
INCLUDE ( [Id],
[ControlId],
[ContentId],
[Title],
[TextContent],
[UserId],
[DateModified]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]



CREATE NONCLUSTERED INDEX [IX_aspnet_Membership_EMail_Opt1] ON [dbo].[aspnet_Membership] 
(
	[IsLockedOut] ASC,
	[UserId] ASC,
	[ApplicationId] ASC,
	[LoweredEmail] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]



CREATE NONCLUSTERED INDEX [IX_SnTag_Slug_Opt1] ON [dbo].[SnTag] 
(
	[Slug] ASC,
	[RelationshipId] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]



CREATE NONCLUSTERED INDEX [IX_BlogPost_Id_Opt1] ON [dbo].[BlogPost] 
(
	[Id] ASC,
	[BlogId] ASC,
	[UserId] ASC,
	[IsPublished] ASC,
	[DatePublished] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]


CREATE NONCLUSTERED INDEX [IX_BlogPost_User_Opt1] ON [dbo].[BlogPost] 
(
	[BlogId] ASC,
	[DatePublished] ASC,
	[UserId] ASC,
	[IsPublished] ASC
)WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]



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