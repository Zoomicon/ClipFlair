

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnComment_aspnet_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnComment]'))
ALTER TABLE [dbo].[SnComment] DROP CONSTRAINT [FK_SnComment_aspnet_Users]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnComment_SnAlbum]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnComment]'))
ALTER TABLE [dbo].[SnComment] DROP CONSTRAINT [FK_SnComment_SnAlbum]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnComment_SnNote]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnComment]'))
ALTER TABLE [dbo].[SnComment] DROP CONSTRAINT [FK_SnComment_SnNote]
GO

USE [sbaOpen]
GO

/****** Object:  Table [dbo].[SnComment]    Script Date: 12/24/2009 12:12:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SnComment]') AND type in (N'U'))
DROP TABLE [dbo].[SnComment]
GO


 