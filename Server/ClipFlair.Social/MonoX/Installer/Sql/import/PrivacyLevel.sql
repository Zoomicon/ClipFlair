/*
 *
 * Automatically generated for MonoX installation
 *
 */

USE MonoX2;
GO


SET NOCOUNT ON
/* ======================================================================= */

PRINT N'Deleting existing values from [dbo].[PrivacyLevel]';
DELETE FROM [dbo].[PrivacyLevel];
GO

PRINT N'Inserting values into [dbo].[PrivacyLevel]';

INSERT INTO [dbo].[PrivacyLevel] ([Id],[PrivacyLevel],[Sort],[DateEntered],[DateModified],[Abrv]) VALUES ('9762CB50-0FF3-4DC5-9942-56033CD2D021',N'Public',0,'2009-08-18T13:58:28.000','2009-08-18T13:58:28.000',N'Public');
GO
INSERT INTO [dbo].[PrivacyLevel] ([Id],[PrivacyLevel],[Sort],[DateEntered],[DateModified],[Abrv]) VALUES ('D4B9B8F6-00BF-480F-89F1-66E77CC883C9',N'Private',1,'2009-08-18T13:58:28.000','2009-08-18T13:58:28.000',N'Private');
GO
INSERT INTO [dbo].[PrivacyLevel] ([Id],[PrivacyLevel],[Sort],[DateEntered],[DateModified],[Abrv]) VALUES ('18E7EF31-8FEE-44DA-839C-B23FAA999893',N'Friends',2,'2009-12-17T10:20:15.000','2009-12-17T10:20:15.000',N'Friends');
GO
GO

SET NOCOUNT OFF
/* ======================================================================= */

PRINT N'Done.';

