Begin transaction
BEGIN TRY

CREATE TABLE dbo.CalendarEventEntry
	(
	CalendarId uniqueidentifier NOT NULL,
	CalendarEventId uniqueidentifier NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.CalendarEventEntry ADD CONSTRAINT
	PK_CalendarEventEntry PRIMARY KEY CLUSTERED 
	(
	CalendarId,
	CalendarEventId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.CalendarEventEntry ADD CONSTRAINT
	FK_CalendarEventEntry_Calendar FOREIGN KEY
	(
	CalendarId
	) REFERENCES dbo.Calendar
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
ALTER TABLE dbo.CalendarEventEntry ADD CONSTRAINT
	FK_CalendarEventEntry_CalendarEvent FOREIGN KEY
	(
	CalendarEventId
	) REFERENCES dbo.CalendarEvent
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 

INSERT INTO CalendarEventEntry
           ([CalendarId]
           ,[CalendarEventId])
SELECT CalendarId, Id FROM CalendarEvent

ALTER TABLE dbo.CalendarEvent
	DROP CONSTRAINT FK_CalendarEvent_Calendar

ALTER TABLE dbo.CalendarEvent
	DROP COLUMN CalendarId

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