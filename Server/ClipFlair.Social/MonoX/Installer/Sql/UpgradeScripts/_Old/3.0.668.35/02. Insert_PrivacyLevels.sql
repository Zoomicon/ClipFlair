INSERT INTO [dbo].[PrivacyLevel]
           ([Id]
           ,[PrivacyLevel]
           ,[Sort]
           ,[DateEntered]
           ,[DateModified])
     VALUES
           (newid()
           ,'Public'
           ,0
           ,getdate()
           ,getdate())
GO


INSERT INTO [dbo].[PrivacyLevel]
           ([Id]
           ,[PrivacyLevel]
           ,[Sort]
           ,[DateEntered]
           ,[DateModified])
     VALUES
           (newid()
           ,'Private'
           ,1
           ,getdate()
           ,getdate())
GO