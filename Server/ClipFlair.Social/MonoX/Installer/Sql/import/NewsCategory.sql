/*
 *
 * Automatically generated for MonoX installation
 *
 */

USE MonoX2;
GO


SET NOCOUNT ON
/* ======================================================================= */

PRINT N'Deleting existing values from [dbo].[NewsCategory]';
DELETE FROM [dbo].[NewsCategory];
GO

PRINT N'Inserting values into [dbo].[NewsCategory]';

INSERT INTO [dbo].[NewsCategory] ([Id],[ApplicationId],[NewsCategoryId],[Image],[Order],[DateEntered],[DateModified]) VALUES ('CDF073F2-9404-4B09-83AA-9E6300D5794E','67C919E2-8DF4-476A-B312-C26F82A36CFB',NULL,NULL,0,'2011-01-06T11:39:31.000','2011-01-06T16:00:45.000');
GO
GO

SET NOCOUNT OFF
/* ======================================================================= */

PRINT N'Done.';

