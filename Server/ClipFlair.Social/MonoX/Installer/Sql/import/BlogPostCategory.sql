/*
 *
 * Automatically generated for MonoX installation
 *
 */

USE MonoX2;
GO


SET NOCOUNT ON
/* ======================================================================= */

PRINT N'Deleting existing values from [dbo].[BlogPostCategory]';
DELETE FROM [dbo].[BlogPostCategory];
GO

PRINT N'Inserting values into [dbo].[BlogPostCategory]';

INSERT INTO [dbo].[BlogPostCategory] ([Id],[BlogPostId],[BlogCategoryId]) VALUES ('4DAA51CC-CD00-477F-8D1B-9D9E0165AECA','C9C46C9C-A976-4421-AFEA-9D9E0165757F','4FFE4C16-DD0D-496D-9655-9C0D01420787');
GO
GO

SET NOCOUNT OFF
/* ======================================================================= */

PRINT N'Done.';

