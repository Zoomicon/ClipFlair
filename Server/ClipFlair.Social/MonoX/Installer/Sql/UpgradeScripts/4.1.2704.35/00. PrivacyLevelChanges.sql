
ALTER TABLE PrivacyLevel ADD Abrv nvarchar(50) NULL
GO	
UPDATE [dbo].[PrivacyLevel]
SET [Abrv] = [PrivacyLevel] 
GO
ALTER TABLE dbo.PrivacyLevel Alter Column Abrv nvarchar(50) NOT NULL
