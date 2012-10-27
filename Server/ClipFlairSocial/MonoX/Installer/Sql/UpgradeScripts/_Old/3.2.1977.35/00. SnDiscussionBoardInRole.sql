CREATE TABLE [dbo].[SnDiscussionBoardInRole](
	[BoardId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BoardId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SnDiscussionBoardInRole]  WITH CHECK ADD  CONSTRAINT [FK__SnDiscussionBoardInRole__Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO

ALTER TABLE [dbo].[SnDiscussionBoardInRole] CHECK CONSTRAINT [FK__SnDiscussionBoardInRole__Roles]
GO

ALTER TABLE [dbo].[SnDiscussionBoardInRole]  WITH CHECK ADD  CONSTRAINT [FK__SnDiscussionBoardInRole__Board] FOREIGN KEY([BoardId])
REFERENCES [dbo].[SnDiscussionBoard] ([Id])
GO

ALTER TABLE [dbo].[SnDiscussionBoardInRole] CHECK CONSTRAINT [FK__SnDiscussionBoardInRole__Board]
GO

/****** Object:  Index [aspnet_UsersInRoles_index]    Script Date: 11/19/2010 14:44:32 ******/
CREATE NONCLUSTERED INDEX [SnDiscussionBoardInRole_index] ON [dbo].[SnDiscussionBoardInRole] 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO



