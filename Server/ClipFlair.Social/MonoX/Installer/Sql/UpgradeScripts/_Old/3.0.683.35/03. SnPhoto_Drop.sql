
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnComment_SnPhoto]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnComment]'))
ALTER TABLE [dbo].[SnComment] DROP CONSTRAINT [FK_SnComment_SnPhoto]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnPhoto_aspnet_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnPhoto]'))
ALTER TABLE [dbo].[SnPhoto] DROP CONSTRAINT [FK_SnPhoto_aspnet_Users]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SnPhoto_SnAlbum]') AND parent_object_id = OBJECT_ID(N'[dbo].[SnPhoto]'))
ALTER TABLE [dbo].[SnPhoto] DROP CONSTRAINT [FK_SnPhoto_SnAlbum]
GO

/****** Object:  Table [dbo].[SnPhoto]    Script Date: 12/24/2009 11:59:14 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SnPhoto]') AND type in (N'U'))
DROP TABLE [dbo].[SnPhoto]
GO


 