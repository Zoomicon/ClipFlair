Begin transaction
BEGIN TRY

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
CREATE TABLE dbo.Calendar
	(
	Id uniqueidentifier NOT NULL,
	OwnerId uniqueidentifier NOT NULL,
	DateCreated datetime NOT NULL,
	DateModified datetime NULL,
	Name nvarchar(255) NULL,
	Slug nvarchar(255) NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.Calendar ADD CONSTRAINT
	PK_Calendar PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.Calendar ADD CONSTRAINT
	FK_Calendar_aspnet_Users FOREIGN KEY
	(
	OwnerId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
CREATE TABLE dbo.CalendarInRole
	(
	CalendarId uniqueidentifier NOT NULL,
	RoleId uniqueidentifier NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.CalendarInRole ADD CONSTRAINT
	PK_CalendarInRole PRIMARY KEY CLUSTERED 
	(
	CalendarId,
	RoleId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.CalendarInRole ADD CONSTRAINT
	FK_CalendarInRole_aspnet_Roles FOREIGN KEY
	(
	RoleId
	) REFERENCES dbo.aspnet_Roles
	(
	RoleId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.CalendarInRole ADD CONSTRAINT
	FK_CalendarInRole_Calendar FOREIGN KEY
	(
	CalendarId
	) REFERENCES dbo.Calendar
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
CREATE TABLE dbo.CalendarModerator
	(
	CalendarId uniqueidentifier NOT NULL,
	UserId uniqueidentifier NOT NULL
	)  ON [PRIMARY]
	
ALTER TABLE dbo.CalendarModerator ADD CONSTRAINT
	PK_CalendarModerator PRIMARY KEY CLUSTERED 
	(
	CalendarId,
	UserId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.CalendarModerator ADD CONSTRAINT
	FK_CalendarModerator_aspnet_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.CalendarModerator ADD CONSTRAINT
	FK_CalendarModerator_Calendar FOREIGN KEY
	(
	CalendarId
	) REFERENCES dbo.Calendar
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
CREATE TABLE dbo.CalendarEvent
	(
	Id uniqueidentifier NOT NULL,
	CalendarId uniqueidentifier NOT NULL,
	AuthorId uniqueidentifier NOT NULL,
	DateCreated datetime NOT NULL,
	DateModified datetime NULL,
	StartTime datetime NOT NULL,
	EndTime datetime NOT NULL,
	Title nvarchar(150) NULL,
	Description nvarchar(500) NULL,
	Place nvarchar(250) NULL,
	RecurringType int NULL,
	RepeatFactor int NOT NULL,
	WeekDays int NULL,
	MonthlyOption int NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.CalendarEvent ADD CONSTRAINT
	PK_CalendarEvent PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.CalendarEvent ADD CONSTRAINT
	FK_CalendarEvent_Calendar FOREIGN KEY
	(
	CalendarId
	) REFERENCES dbo.Calendar
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.CalendarEvent ADD CONSTRAINT
	FK_CalendarEvent_aspnet_Users FOREIGN KEY
	(
	AuthorId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
ALTER TABLE dbo.SnRelationship ADD
	CalendarEventId uniqueidentifier NULL

ALTER TABLE dbo.SnRelationship ADD CONSTRAINT
	FK_SnRelationship_CalendarEvent FOREIGN KEY
	(
	CalendarEventId
	) REFERENCES dbo.CalendarEvent
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

COMMIT TRANSACTION                              
END TRY

BEGIN CATCH
       SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;     
       ROLLBACK TRANSACTION
END CATCH   	