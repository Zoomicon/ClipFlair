Begin transaction
BEGIN TRY

	INSERT INTO [dbo].[SnRelationship]
           ([Id]
           ,[NoteId]
           ,[FileId]
           ,[AlbumId]
           ,[BlogPostId]
           ,[MessageId]
           ,[DiscussionMessageId]
           ,[CustomId1]
           ,[CustomId2]
           ,[CustomId3]
           ,[DiscussionBoardId]
           ,[DiscussionTopicId]
           ,[DocumentId]
           ,[NewsItemId]
           ,[ListItemId]
           ,[BlogId])
     Select NewId()
           ,Null
           ,Null
           ,Null
           ,[dbo].[BlogPost].[Id]
           ,Null
           ,Null
           ,Null
           ,Null
           ,Null
           ,Null
           ,Null
           ,Null
           ,Null
           ,Null
           ,Null
	From [dbo].[BlogPost]
	Where [dbo].[BlogPost].[Id] Not IN (Select [BlogPostId] from [dbo].[SnRelationship] where ([BlogPostId] is not null))


	INSERT INTO [dbo].[SnSubscriber]
           ([Id]
           ,[RelationshipId]
           ,[UserId]
           ,[Email]
           ,[SubscriptionDate]
           ,[Active]
           ,[DateModified])
     Select
           NewId()
           ,(Select Top 1 [dbo].[SnRelationship].[Id] from [dbo].[SnRelationship] where ([dbo].[SnRelationship].[BlogPostId] = [dbo].[BlogPost].[Id]))
           ,[UserId]
           ,''
           ,GetDate()
           ,1
           ,GetDate()
	From [dbo].[BlogPost]

	INSERT INTO [dbo].[SnSubscriber]
           ([Id]
           ,[RelationshipId]
           ,[UserId]
           ,[Email]
           ,[SubscriptionDate]
           ,[Active]
           ,[DateModified])
     Select
           NewId()
           ,(Select Top 1 [dbo].[SnRelationship].[Id] from [dbo].[SnRelationship] where ([dbo].[SnRelationship].[BlogPostId] = [dbo].[BlogPostNotification].[BlogPostId]))
           ,NUll
           ,[Email]
           ,GetDate()
           ,1
           ,GetDate()
	From [dbo].[BlogPostNotification]

	IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BlogPostNotification_BlogPost]') AND parent_object_id = OBJECT_ID(N'[dbo].[BlogPostNotification]'))
	ALTER TABLE [dbo].[BlogPostNotification] DROP CONSTRAINT [FK_BlogPostNotification_BlogPost]

	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlogPostNotification]') AND type in (N'U'))
	DROP TABLE [dbo].[BlogPostNotification]

	
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