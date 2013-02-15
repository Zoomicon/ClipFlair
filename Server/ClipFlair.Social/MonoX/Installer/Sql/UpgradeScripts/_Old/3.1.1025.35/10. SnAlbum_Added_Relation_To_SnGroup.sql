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
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnAlbum
	DROP CONSTRAINT FK_SnAlbum_PrivacyLevel
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnAlbum
	DROP CONSTRAINT FK_SnAlbum_aspnet_Users
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SnAlbum
	(
	Id uniqueidentifier NOT NULL,
	UserId uniqueidentifier NULL,
	SnGroupId uniqueidentifier NULL,
	Name nvarchar(1000) NOT NULL,
	Description nvarchar(MAX) NULL,
	PrivacyLevelId uniqueidentifier NULL,
	AlbumCoverSnFileId uniqueidentifier NULL,
	DateCreated datetime NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.SnAlbum)
	 EXEC('INSERT INTO dbo.Tmp_SnAlbum (Id, UserId, Name, Description, PrivacyLevelId, AlbumCoverSnFileId, DateCreated)
		SELECT Id, UserId, Name, Description, PrivacyLevelId, AlbumCoverSnFileId, DateCreated FROM dbo.SnAlbum WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.SnEvent
	DROP CONSTRAINT FK_SnEvent_SnAlbum
GO
ALTER TABLE dbo.SnComment
	DROP CONSTRAINT FK_SnComment_SnAlbum
GO
ALTER TABLE dbo.SnFile
	DROP CONSTRAINT FK_SnFile_SnAlbum
GO
ALTER TABLE dbo.SnAlbum
	DROP CONSTRAINT FK_SnAlbum_SnFile
GO
DROP TABLE dbo.SnAlbum
GO
EXECUTE sp_rename N'dbo.Tmp_SnAlbum', N'SnAlbum', 'OBJECT' 
GO
ALTER TABLE dbo.SnAlbum ADD CONSTRAINT
	PK_SnAlbum PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.SnAlbum ADD CONSTRAINT
	FK_SnAlbum_aspnet_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SnAlbum ADD CONSTRAINT
	FK_SnAlbum_PrivacyLevel FOREIGN KEY
	(
	PrivacyLevelId
	) REFERENCES dbo.PrivacyLevel
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SnAlbum ADD CONSTRAINT
	FK_SnAlbum_SnGroup FOREIGN KEY
	(
	SnGroupId
	) REFERENCES dbo.SnGroup
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnFile ADD CONSTRAINT
	FK_SnFile_SnAlbum FOREIGN KEY
	(
	AlbumId
	) REFERENCES dbo.SnAlbum
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
ALTER TABLE dbo.SnAlbum ADD CONSTRAINT
	FK_SnAlbum_SnFile FOREIGN KEY
	(
	AlbumCoverSnFileId
	) REFERENCES dbo.SnFile
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnComment ADD CONSTRAINT
	FK_SnComment_SnAlbum FOREIGN KEY
	(
	AlbumId
	) REFERENCES dbo.SnAlbum
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnAlbum FOREIGN KEY
	(
	SnAlbumId
	) REFERENCES dbo.SnAlbum
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT