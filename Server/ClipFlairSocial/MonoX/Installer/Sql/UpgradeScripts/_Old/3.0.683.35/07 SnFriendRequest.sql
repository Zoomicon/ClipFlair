
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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
CREATE TABLE [dbo].[SnFriendRequest](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[FriendUserId] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateAccepted] [datetime] NULL,
 CONSTRAINT [PK_SnFriendRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SnFriendRequest]  WITH CHECK ADD  CONSTRAINT [FK_SnFriendRequest_aspnet_Users] FOREIGN KEY([FriendUserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[SnFriendRequest] CHECK CONSTRAINT [FK_SnFriendRequest_aspnet_Users]
GO
ALTER TABLE [dbo].[SnFriendRequest]  WITH CHECK ADD  CONSTRAINT [FK_SnFriendRequest_SnFriendRequest] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
ALTER TABLE [dbo].[SnFriendRequest] CHECK CONSTRAINT [FK_SnFriendRequest_SnFriendRequest]
Go
COMMIT
