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
ALTER TABLE dbo.UserProfile
	DROP CONSTRAINT FK_UserProfile_aspnet_Users
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.UserProfile
	DROP CONSTRAINT FK_UserProfile_TermsAndConditions
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.UserProfile
	DROP CONSTRAINT DF_UserProfile_EMailVerified
GO
CREATE TABLE dbo.Tmp_UserProfile
	(
	Id uniqueidentifier NOT NULL,
	FirstName nvarchar(250) NULL,
	LastName nvarchar(250) NULL,
	BirthDate datetime NULL,
	Address nvarchar(250) NULL,
	City nvarchar(250) NULL,
	ZipCode nvarchar(250) NULL,
	Country nvarchar(250) NULL,
	AboutMySelf nvarchar(500) NULL,
	WebSites nvarchar(500) NULL,
	EMailVerified bit NOT NULL,
	MyStatus nvarchar(500) NULL,
	TermsAndConditionsId uniqueidentifier NULL,
	TermsAndConditionsAgreedDate datetime NULL,
	DateEntered datetime NULL,
	DateModified datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_UserProfile ADD CONSTRAINT
	DF_UserProfile_EMailVerified DEFAULT ((0)) FOR EMailVerified
GO
IF EXISTS(SELECT * FROM dbo.UserProfile)
	 EXEC('INSERT INTO dbo.Tmp_UserProfile (Id, FirstName, LastName, BirthDate, Address, City, ZipCode, Country, AboutMySelf, WebSites, EMailVerified, TermsAndConditionsId, TermsAndConditionsAgreedDate, DateEntered, DateModified)
		SELECT Id, FirstName, LastName, BirthDate, Address, City, ZipCode, Country, AboutMySelf, WebSites, EMailVerified, TermsAndConditionsId, TermsAndConditionsAgreedDate, DateEntered, DateModified FROM dbo.UserProfile WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.UserProfile
GO
EXECUTE sp_rename N'dbo.Tmp_UserProfile', N'UserProfile', 'OBJECT' 
GO
ALTER TABLE dbo.UserProfile ADD CONSTRAINT
	PK_UserProfile PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.UserProfile ADD CONSTRAINT
	FK_UserProfile_TermsAndConditions FOREIGN KEY
	(
	TermsAndConditionsId
	) REFERENCES dbo.TermsAndConditions
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.UserProfile ADD CONSTRAINT
	FK_UserProfile_aspnet_Users FOREIGN KEY
	(
	Id
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT
 