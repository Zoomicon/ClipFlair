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
CREATE TABLE dbo.NewsletterLog
	(
	Id uniqueidentifier NOT NULL,
	NewsletterId uniqueidentifier NOT NULL,
	Email nvarchar(200) NOT NULL,
	SentOn datetime NOT NULL,
	Success bit NOT NULL,
	Message nvarchar(500) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.NewsletterLog ADD CONSTRAINT
	PK_NewsletterLog PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.NewsletterLog ADD CONSTRAINT
	FK_NewsletterLog_Newsletter FOREIGN KEY
	(
	NewsletterId
	) REFERENCES dbo.Newsletter
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
GO
COMMIT
