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

PRINT N'Deleting existing values from [dbo].[Version]';
DELETE FROM [dbo].[Version];
GO

PRINT N'Inserting values into [dbo].[Version]';

INSERT INTO [dbo].[Version] ([Id],[Version],[Comment],[UpgradeLog],[DateEntered]) VALUES ('6658DDE6-9FC7-4BE5-B12D-3BBC2C80B07A',N'4.5.3103',N'http://monox.mono-software.com/ContentPage/ChangeLog/',N'','2011-11-10T11:25:35.000');
GO
INSERT INTO [dbo].[Version] ([Id],[Version],[Comment],[UpgradeLog],[DateEntered]) VALUES ('9B7CAF10-2DFF-43B9-AB31-5FE58717CBDB',N'4.5.3017',N'http://monox.mono-software.com/ContentPage/ChangeLog/',N'','2011-09-27T12:13:37.000');
GO
INSERT INTO [dbo].[Version] ([Id],[Version],[Comment],[UpgradeLog],[DateEntered]) VALUES ('996B68A6-48BF-41FE-924B-C4DC65A18B2C',N'4.5.2897',N'http://monox.mono-software.com/ContentPage/ChangeLog/',N'','2011-08-20T13:00:11.000');
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

