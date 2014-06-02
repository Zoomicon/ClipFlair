/*
 *
 * Automatically generated for MonoX installation
 *
 */

USE MonoX2;
GO


SET NOCOUNT ON
/* ======================================================================= */

PRINT N'Deleting existing values from [dbo].[NavigationRole]';
DELETE FROM [dbo].[NavigationRole];
GO

PRINT N'Inserting values into [dbo].[NavigationRole]';

INSERT INTO [dbo].[NavigationRole] ([Id],[NavigationId],[RoleId]) VALUES ('F5750F9D-6144-4BBF-B5F0-A10400C79768','83E79A5C-C83F-4CFA-A438-9D99013B1C85','EBAF7B92-BB12-40C3-B3E4-FD40B9932E3E');
GO
INSERT INTO [dbo].[NavigationRole] ([Id],[NavigationId],[RoleId]) VALUES ('9AD465E7-C2B7-4E86-BC8E-9D99019D03FE','9A7E61A5-D5F8-466A-ADDA-9D99019CDA2F','EBAF7B92-BB12-40C3-B3E4-FD40B9932E3E');
GO
INSERT INTO [dbo].[NavigationRole] ([Id],[NavigationId],[RoleId]) VALUES ('3ADAE2D8-2768-4B71-B40D-A10400D19302','444664DF-62AC-484E-8B2E-A0FD00F9E5FA','EBAF7B92-BB12-40C3-B3E4-FD40B9932E3E');
GO
GO

SET NOCOUNT OFF
/* ======================================================================= */

PRINT N'Done.';

