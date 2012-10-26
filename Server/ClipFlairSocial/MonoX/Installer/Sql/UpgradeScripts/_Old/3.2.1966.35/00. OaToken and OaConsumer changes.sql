Begin transaction
BEGIN TRY

	--SQL goes here 
	EXECUTE sp_rename N'dbo.oaConsumer.VerificationCodeTypeValue', N'Tmp_VerificationCodeFormatValue',	'COLUMN'
EXECUTE sp_rename N'dbo.oaConsumer.Tmp_VerificationCodeFormatValue', N'VerificationCodeFormatValue', 'COLUMN'

ALTER TABLE dbo.oaToken
	DROP CONSTRAINT FK_oaToken_oaConsumer

ALTER TABLE dbo.oaToken
	DROP CONSTRAINT FK_oaToken_aspnet_Users

CREATE TABLE dbo.Tmp_oaToken
	(
	Id uniqueidentifier NOT NULL,
	Token nvarchar(MAX) NOT NULL,
	TokenSecret nvarchar(50) NOT NULL,
	CreatedOnUtc datetime NOT NULL,
	CallbackUrl nvarchar(MAX) NULL,
	Scope nvarchar(MAX) NULL,
	ConsumerId uniqueidentifier NOT NULL,
	UserId uniqueidentifier NULL,
	VerificationCode nvarchar(50) NULL,
	ExpirationDate datetime NULL,
	IsAccess bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]

ALTER TABLE dbo.Tmp_oaToken ADD CONSTRAINT
	DF_oaToken_IsAccess DEFAULT 0 FOR IsAccess

IF EXISTS(SELECT * FROM dbo.oaToken)
	 EXEC('INSERT INTO dbo.Tmp_oaToken (Id, Token, TokenSecret, CreatedOnUtc, Scope, ConsumerId, UserId, VerificationCode, ExpirationDate)
		SELECT Id, Token, TokenSecret, CreatedOnUtc, Scope, ConsumerId, UserId, VerificationCode, ExpirationDate FROM dbo.oaToken WITH (HOLDLOCK TABLOCKX)')

DROP TABLE dbo.oaToken

EXECUTE sp_rename N'dbo.Tmp_oaToken', N'oaToken', 'OBJECT' 

ALTER TABLE dbo.oaToken ADD CONSTRAINT
	PK_oaToken PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE dbo.oaToken ADD CONSTRAINT
	FK_oaToken_aspnet_Users FOREIGN KEY
	(
	Id
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
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