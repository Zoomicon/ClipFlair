BEGIN TRANSACTION
GO

/****** Object:  Table [dbo].[PrivacyLevelDefinition]    Script Date: 07/16/2009 15:20:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PrivacyLevelDefinition](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ObjectName] [nvarchar](150) NOT NULL,
	[FieldName] [nvarchar](150) NOT NULL,
	[PrivacyLevelId] [uniqueidentifier] NOT NULL,
	[DateEntered] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_PrivacyLevelDefinition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PrivacyLevelDefinition]  WITH CHECK ADD  CONSTRAINT [FK_PrivacyLevelDefinition_aspnet_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PrivacyLevelDefinition] CHECK CONSTRAINT [FK_PrivacyLevelDefinition_aspnet_Users]
GO

ALTER TABLE [dbo].[PrivacyLevelDefinition]  WITH CHECK ADD  CONSTRAINT [FK_PrivacyLevelDefinition_PrivacyLevel] FOREIGN KEY([PrivacyLevelId])
REFERENCES [dbo].[PrivacyLevel] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PrivacyLevelDefinition] CHECK CONSTRAINT [FK_PrivacyLevelDefinition_PrivacyLevel]
GO

ALTER TABLE [dbo].[PrivacyLevelDefinition] ADD  CONSTRAINT [DF_PrivacyLevelDefinition_DateEntered]  DEFAULT (getdate()) FOR [DateEntered]
GO

ALTER TABLE [dbo].[PrivacyLevelDefinition] ADD  CONSTRAINT [DF_PrivacyLevelDefinition_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO

/****** Object:  Index [IX_PrivacyLevelDefinition]    Script Date: 07/16/2009 15:22:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_PrivacyLevelDefinition] ON [dbo].[PrivacyLevelDefinition] 
(
	[UserId] ASC,
	[ObjectName] ASC,
	[FieldName] ASC,
	[PrivacyLevelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


COMMIT