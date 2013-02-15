
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
CREATE TABLE dbo.SnInvitation
	(
	Id uniqueidentifier NOT NULL,
	UserId uniqueidentifier NOT NULL,
	Email nvarchar(250) NOT NULL,
	DateCreated datetime NOT NULL,
	BecameUserId uniqueidentifier NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.SnInvitation ADD CONSTRAINT
	PK_SnInvitation PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SnInvitation ADD CONSTRAINT
	FK_SnInvitation_aspnet_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.SnInvitation ADD CONSTRAINT
	FK_SnInvitation_aspnet_Users1 FOREIGN KEY
	(
	BecameUserId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO


COMMIT
