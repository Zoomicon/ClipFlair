Begin transaction
BEGIN TRY

	--SQL goes here 
	CREATE TABLE dbo.oaConsumer
	(
	Id uniqueidentifier NOT NULL,
	Name nvarchar(50) NOT NULL,
	Secret nvarchar(50) NOT NULL,
	CertificateRaw varbinary(MAX) NULL,
	VerificationCodeTypeValue int NOT NULL,
	VerificationCodeLength int NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
	
	ALTER TABLE dbo.oaConsumer ADD CONSTRAINT
		DF_oaConsumer_VerificationCodeTypeValue DEFAULT 0 FOR VerificationCodeTypeValue
	
	ALTER TABLE dbo.oaConsumer ADD CONSTRAINT
		DF_oaConsumer_VerificationCodeLength DEFAULT 0 FOR VerificationCodeLength
	
	ALTER TABLE dbo.oaConsumer ADD CONSTRAINT
		PK_oaConsumer PRIMARY KEY CLUSTERED 
		(
		Id
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,			ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	
	
	ALTER TABLE dbo.oaConsumer ADD
	Version nvarchar(50) NULL
	
	
	CREATE TABLE dbo.oaToken
	(
	Id uniqueidentifier NOT NULL,
	Token nvarchar(MAX) NOT NULL,
	TokenSecret nvarchar(50) NOT NULL,
	CreatedOnUtc datetime NOT NULL,
	Scope nvarchar(MAX) NULL,
	ConsumerId uniqueidentifier NOT NULL,
	UserId uniqueidentifier NULL,
	VerificationCode nvarchar(50) NULL,
	ExpirationDate datetime NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE dbo.oaToken ADD CONSTRAINT
		PK_oaToken PRIMARY KEY CLUSTERED 
		(
		Id
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


	ALTER TABLE dbo.oaToken ADD CONSTRAINT
		FK_oaToken_oaConsumer FOREIGN KEY
		(
		ConsumerId
		) REFERENCES dbo.oaConsumer
		(
		Id
		) ON UPDATE  NO ACTION 
		 ON DELETE  NO ACTION 
		
	ALTER TABLE dbo.oaToken ADD CONSTRAINT
		FK_oaToken_aspnet_Users FOREIGN KEY
		(
		Id
		) REFERENCES dbo.aspnet_Users
		(
		UserId
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