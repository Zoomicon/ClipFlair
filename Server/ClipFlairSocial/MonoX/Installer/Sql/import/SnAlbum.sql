/*
 *
 * Automatically generated for MonoX installation
 *
 */

USE MonoX2;
GO

CREATE TABLE #RecoveryModel ( [Model] nvarchar(150) NOT NULL )
INSERT INTO #RecoveryModel ([Model]) Select recovery_model_desc from sys.databases Where name='MonoX2'
ALTER DATABASE [MonoX2] SET RECOVERY BULK_LOGGED
GO

SET NOCOUNT ON
/* ======================================================================= */

PRINT N'Deleting existing values from [dbo].[SnAlbum]';
DELETE FROM [dbo].[SnAlbum];
GO

PRINT N'Inserting values into [dbo].[SnAlbum]';

INSERT INTO [dbo].[SnAlbum] ([Id],[UserId],[SnGroupId],[Name],[Description],[PrivacyLevelId],[DateCreated],[ApplicationId],[LanguageId]) VALUES ('9B1772A8-3FB5-4879-ACD9-9DAD014EB1B3',NULL,'D0C404A2-AF59-4ED4-B2AD-9DA501859B92',N'Mono portfolio',N'Some of the Web sites and applications we designed.','9762CB50-0FF3-4DC5-9942-56033CD2D021','2010-07-08T18:16:44.000','67C919E2-8DF4-476A-B312-C26F82A36CFB','0543FD17-141B-4C40-BB35-B57F9EEC6EE0');
GO
INSERT INTO [dbo].[SnAlbum] ([Id],[UserId],[SnGroupId],[Name],[Description],[PrivacyLevelId],[DateCreated],[ApplicationId],[LanguageId]) VALUES ('94276E2A-457F-4C0E-ACB6-9E5A016CB530','67C919E2-8DF4-476A-B312-C26F82A36CFB',NULL,N'Mono portfolio',N'Some of the Web sites and applications we designed.','9762CB50-0FF3-4DC5-9942-56033CD2D021','2010-12-28T19:55:04.000','67C919E2-8DF4-476A-B312-C26F82A36CFB','0543FD17-141B-4C40-BB35-B57F9EEC6EE0');
GO
GO

SET NOCOUNT OFF
/* ======================================================================= */

IF ((SELECT TOP 1 [Model] FROM #RecoveryModel) = 'SIMPLE')
BEGIN
	ALTER DATABASE [MonoX2] SET RECOVERY SIMPLE
END
IF ((SELECT TOP 1 [Model] FROM #RecoveryModel) = 'BULK_LOGGED')
BEGIN
	ALTER DATABASE [MonoX2] SET RECOVERY BULK_LOGGED
END
IF ((SELECT TOP 1 [Model] FROM #RecoveryModel) = 'FULL')
BEGIN
	ALTER DATABASE [MonoX2] SET RECOVERY FULL
END
GO
DROP TABLE #RecoveryModel

PRINT N'Done.';

