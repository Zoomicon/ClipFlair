IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnInvitation_aspnet_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnInvitation]'))
ALTER TABLE [dbo].[SnInvitation] DROP CONSTRAINT [FK_SnInvitation_aspnet_Users]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnInvitation_aspnet_Users1]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnInvitation]'))
ALTER TABLE [dbo].[SnInvitation] DROP CONSTRAINT [FK_SnInvitation_aspnet_Users1]
GO

/****** Object:  Table [dbo].[SnInvitation]    Script Date: 06/13/2013 14:07:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SnInvitation]') AND type in (N'U'))
DROP TABLE [dbo].[SnInvitation]
GO


