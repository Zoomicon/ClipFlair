
BEGIN TRANSACTION
GO
/****** Object:  Table [dbo].[PrivacyLevel]    Script Date: 07/15/2009 09:29:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PrivacyLevel](
	[Id] [uniqueidentifier] NOT NULL,
	[PrivacyLevel] [nvarchar](100) NOT NULL,
	[Sort] [int] NOT NULL,
	[DateEntered] [datetime] NULL,
	[DateModified] [datetime] NULL,
 CONSTRAINT [PK_PrivacyLevel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PrivacyLevel] ADD  CONSTRAINT [DF_PrivacyLevel_Sort]  DEFAULT ((0)) FOR [Sort]
GO

ALTER TABLE [dbo].[PrivacyLevel] ADD  CONSTRAINT [DF_PrivacyLevel_DateEntered]  DEFAULT (getdate()) FOR [DateEntered]
GO

ALTER TABLE [dbo].[PrivacyLevel] ADD  CONSTRAINT [DF_PrivacyLevel_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO


COMMIT