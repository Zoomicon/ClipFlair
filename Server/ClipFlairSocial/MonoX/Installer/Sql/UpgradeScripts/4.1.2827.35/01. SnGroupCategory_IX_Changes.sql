Begin transaction
BEGIN TRY

	/****** Object:  Index [IX_SnGroupCategory]    Script Date: 08/04/2011 17:14:10 ******/
	IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SnGroupCategory]') AND name = N'IX_SnGroupCategory')
	ALTER TABLE [dbo].[SnGroupCategory] DROP CONSTRAINT [IX_SnGroupCategory]


	/****** Object:  Index [IX_SnGroupCategory]    Script Date: 08/04/2011 17:14:10 ******/
	ALTER TABLE [dbo].[SnGroupCategory] ADD  CONSTRAINT [IX_SnGroupCategory] UNIQUE NONCLUSTERED 
	(
		[ApplicationId] ASC,
		[LanguageId] ASC,
		[Name] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	
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