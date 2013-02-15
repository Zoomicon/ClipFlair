Begin transaction
BEGIN TRY

	--SQL goes here 
	ALTER TABLE dbo.oaConsumer
	DROP CONSTRAINT DF_oaConsumer_VerificationCodeTypeValue
	
	ALTER TABLE dbo.oaConsumer
		DROP CONSTRAINT DF_oaConsumer_VerificationCodeLength
	
	CREATE TABLE dbo.Tmp_oaConsumer
		(
		Id uniqueidentifier NOT NULL,
		Name nvarchar(50) NOT NULL,
		Secret nvarchar(50) NOT NULL,
		CallbackUrl nvarchar(MAX) NULL,
		CertificateRaw varbinary(MAX) NULL,
		VerificationCodeTypeValue int NOT NULL,
		VerificationCodeLength int NOT NULL,
		Version nvarchar(50) NULL
		)  ON [PRIMARY]
		 TEXTIMAGE_ON [PRIMARY]
	
	ALTER TABLE dbo.Tmp_oaConsumer ADD CONSTRAINT
		DF_oaConsumer_VerificationCodeTypeValue DEFAULT ((0)) FOR VerificationCodeTypeValue
	
	ALTER TABLE dbo.Tmp_oaConsumer ADD CONSTRAINT
		DF_oaConsumer_VerificationCodeLength DEFAULT ((0)) FOR VerificationCodeLength
	
	IF EXISTS(SELECT * FROM dbo.oaConsumer)
		 EXEC('INSERT INTO dbo.Tmp_oaConsumer (Id, Name, Secret, CertificateRaw, VerificationCodeTypeValue, VerificationCodeLength, Version)
			SELECT Id, Name, Secret, CertificateRaw, VerificationCodeTypeValue, VerificationCodeLength, Version FROM dbo.oaConsumer WITH (HOLDLOCK TABLOCKX)')
	
	ALTER TABLE dbo.oaToken
		DROP CONSTRAINT FK_oaToken_oaConsumer
	
	DROP TABLE dbo.oaConsumer
	
	EXECUTE sp_rename N'dbo.Tmp_oaConsumer', N'oaConsumer', 'OBJECT' 
	
	ALTER TABLE dbo.oaConsumer ADD CONSTRAINT
		PK_oaConsumer PRIMARY KEY CLUSTERED 
		(
		Id
		) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

	COMMIT
	BEGIN TRANSACTION
	
	ALTER TABLE dbo.oaToken ADD CONSTRAINT
		FK_oaToken_oaConsumer FOREIGN KEY
		(
		ConsumerId
		) REFERENCES dbo.oaConsumer
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