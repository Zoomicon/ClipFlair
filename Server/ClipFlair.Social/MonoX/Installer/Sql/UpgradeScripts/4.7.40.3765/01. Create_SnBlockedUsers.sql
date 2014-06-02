/****** Object:  Table [dbo].[SnBlockUsers]    Script Date: 10/15/2012 17:17:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SnBlockUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[BlockedUserId] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.SnBlockUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SnBlockUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SnBlockUsers_aspnet_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SnBlockUsers] CHECK CONSTRAINT [FK_dbo.SnBlockUsers_aspnet_Users]
GO

ALTER TABLE [dbo].[SnBlockUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SnBlockUsers_aspnet_Users1] FOREIGN KEY([BlockedUserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO

ALTER TABLE [dbo].[SnBlockUsers] CHECK CONSTRAINT [FK_dbo.SnBlockUsers_aspnet_Users1]
GO


