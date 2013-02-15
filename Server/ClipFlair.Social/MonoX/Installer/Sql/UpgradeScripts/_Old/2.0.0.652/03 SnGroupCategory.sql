BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE [dbo].[SnGroupCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Slug] [nvarchar](255) NULL,
 CONSTRAINT [PK_SnGroupType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SnGroupCategory]  WITH CHECK ADD  CONSTRAINT [FK_SnGroupCategory_aspnet_Applications] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
ALTER TABLE dbo.SnGroupCategory ADD CONSTRAINT
	IX_SnGroupCategory UNIQUE NONCLUSTERED 
	(
	ApplicationId,
	Name
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SnGroupCategory] CHECK CONSTRAINT [FK_SnGroupCategory_aspnet_Applications]
GO
ALTER TABLE [dbo].[SnGroupCategory]  WITH CHECK ADD  CONSTRAINT [FK_SnGroupCategory_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[SnGroupCategory] CHECK CONSTRAINT [FK_SnGroupCategory_Language]
GO
COMMIT
