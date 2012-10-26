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
COMMIT
BEGIN TRANSACTION
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SnEvent ADD
	SnDiscussionBoardId uniqueidentifier NULL,
	SnDiscussionTopicId uniqueidentifier NULL,
	CustomId1 uniqueidentifier NULL,
	CustomId2 uniqueidentifier NULL,
	CustomId3 uniqueidentifier NULL
GO
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnDiscussionBoard FOREIGN KEY
	(
	SnDiscussionBoardId
	) REFERENCES dbo.SnDiscussionBoard
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION  
	
GO
ALTER TABLE dbo.SnEvent ADD CONSTRAINT
	FK_SnEvent_SnDiscussionTopic FOREIGN KEY
	(
	SnDiscussionTopicId
	) REFERENCES dbo.SnDiscussionTopic
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION  
	
GO
COMMIT