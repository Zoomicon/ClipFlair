Begin transaction
BEGIN TRY

ALTER TABLE dbo.CalendarEvent
	DROP CONSTRAINT FK_CalendarEvent_aspnet_Users

ALTER TABLE dbo.CalendarEvent
	DROP CONSTRAINT FK_CalendarEvent_Calendar

CREATE TABLE dbo.Tmp_CalendarEvent
	(
	Id uniqueidentifier NOT NULL,
	CalendarId uniqueidentifier NOT NULL,
	AuthorId uniqueidentifier NOT NULL,
	DateCreated datetime NOT NULL,
	DateModified datetime NULL,
	StartTime datetime NOT NULL,
	EndTime datetime NOT NULL,
	AllDay bit NOT NULL,
	Title nvarchar(150) NULL,
	Description nvarchar(500) NULL,
	Place nvarchar(250) NULL,
	RecurringType int NULL,
	RepeatFactor int NOT NULL,
	WeekDays int NULL,
	MonthlyOption int NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.Tmp_CalendarEvent ADD CONSTRAINT
	DF_CalendarEvent_AllDay DEFAULT 0 FOR AllDay

IF EXISTS(SELECT * FROM dbo.CalendarEvent)
	 EXEC('INSERT INTO dbo.Tmp_CalendarEvent (Id, CalendarId, AuthorId, DateCreated, DateModified, StartTime, EndTime, Title, Description, Place, RecurringType, RepeatFactor, WeekDays, MonthlyOption)
		SELECT Id, CalendarId, AuthorId, DateCreated, DateModified, StartTime, EndTime, Title, Description, Place, RecurringType, RepeatFactor, WeekDays, MonthlyOption FROM dbo.CalendarEvent WITH (HOLDLOCK TABLOCKX)')

ALTER TABLE dbo.SnRelationship
	DROP CONSTRAINT FK_SnRelationship_CalendarEvent

DROP TABLE dbo.CalendarEvent

EXECUTE sp_rename N'dbo.Tmp_CalendarEvent', N'CalendarEvent', 'OBJECT' 

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
	
ALTER TABLE dbo.SnRelationship ADD CONSTRAINT
	FK_SnRelationship_CalendarEvent FOREIGN KEY
	(
	CalendarEventId
	) REFERENCES dbo.CalendarEvent
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
EXECUTE sp_rename N'dbo.CalendarInRole', N'CalendarViewRole', 'OBJECT'

CREATE TABLE dbo.CalendarEditRole
	(
	CalendarId uniqueidentifier NOT NULL,
	RoleId uniqueidentifier NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.CalendarEditRole ADD CONSTRAINT
	PK_CalendarEditRole PRIMARY KEY CLUSTERED 
	(
	CalendarId,
	RoleId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.CalendarEditRole ADD CONSTRAINT
	FK_CalendarEditRole_Calendar FOREIGN KEY
	(
	CalendarId
	) REFERENCES dbo.Calendar
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
ALTER TABLE dbo.CalendarEditRole ADD CONSTRAINT
	FK_CalendarEditRole_aspnet_Roles FOREIGN KEY
	(
	RoleId
	) REFERENCES dbo.aspnet_Roles
	(
	RoleId
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
ALTER TABLE dbo.CalendarViewRole
	DROP CONSTRAINT FK_CalendarInRole_Calendar

ALTER TABLE dbo.CalendarViewRole
	DROP CONSTRAINT FK_CalendarInRole_aspnet_Roles

ALTER TABLE dbo.CalendarViewRole ADD CONSTRAINT
	FK_CalendarInRole_aspnet_Roles FOREIGN KEY
	(
	RoleId
	) REFERENCES dbo.aspnet_Roles
	(
	RoleId
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
ALTER TABLE dbo.CalendarViewRole ADD CONSTRAINT
	FK_CalendarInRole_Calendar FOREIGN KEY
	(
	CalendarId
	) REFERENCES dbo.Calendar
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
ALTER TABLE dbo.CalendarEvent
	DROP CONSTRAINT FK_CalendarEvent_Calendar

ALTER TABLE dbo.CalendarEvent ADD CONSTRAINT
	CK_CalendarEvent CHECK (([StartTime]<=[EndTime]))

ALTER TABLE dbo.CalendarEvent ADD CONSTRAINT
	FK_CalendarEvent_Calendar FOREIGN KEY
	(
	CalendarId
	) REFERENCES dbo.Calendar
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
ALTER TABLE dbo.PageEditRole
	DROP CONSTRAINT FK_PageEditRole_Page

ALTER TABLE dbo.PageEditRole ADD CONSTRAINT
	FK_PageEditRole_Page FOREIGN KEY
	(
	PageId
	) REFERENCES dbo.Page
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  CASCADE 
	
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