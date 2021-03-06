Begin transaction
BEGIN TRY

	CREATE TABLE dbo.CalendarEventRecurringDefinition
	(
	Id uniqueidentifier NOT NULL,
	RecurringTypeId uniqueidentifier NOT NULL,
	RepeatFactor int NOT NULL,
	Sunday bit NOT NULL,
	Monday bit NOT NULL,
	Tuesday bit NOT NULL,
	Wednesday bit NOT NULL,
	Thursday bit NOT NULL,
	Friday bit NOT NULL,
	Saturday bit NOT NULL,
	DayOfMonth int NULL,
	MonthOfYear int NULL,
	StartsOn datetime NOT NULL,
	EndsOn datetime NOT NULL
	)  ON [PRIMARY]

	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	DF_CalendarEventRecurringDefinition_Sunday DEFAULT 0 FOR Sunday

	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	DF_CalendarEventRecurringDefinition_Monday DEFAULT 0 FOR Monday

	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	DF_CalendarEventRecurringDefinition_Tuesday DEFAULT 0 FOR Tuesday

	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	DF_CalendarEventRecurringDefinition_Wednesday DEFAULT 0 FOR Wednesday

	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	DF_CalendarEventRecurringDefinition_Thursday DEFAULT 0 FOR Thursday

	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	DF_CalendarEventRecurringDefinition_Friday DEFAULT 0 FOR Friday

	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	DF_CalendarEventRecurringDefinition_Saturday DEFAULT 0 FOR Saturday

	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	DF_CalendarEventRecurringDefinition_DayOfMonth DEFAULT 0 FOR DayOfMonth

	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	PK_CalendarEventRecurringDefinition PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


	ALTER TABLE dbo.CalendarEventRecurringDefinition ADD CONSTRAINT
	FK_CalendarEventRecurringDefinition_CalendarEventRecurringDefinitionType FOREIGN KEY
	(
	RecurringTypeId
	) REFERENCES dbo.CalendarEventRecurringDefinitionType
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
