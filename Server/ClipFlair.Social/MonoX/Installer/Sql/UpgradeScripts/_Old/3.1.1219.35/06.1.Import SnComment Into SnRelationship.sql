INSERT INTO SnRelationship (Id, NoteId, FileId, AlbumId, BlogPostId, MessageId, DiscussionMessageId, CustomId1, CustomId2, CustomId3)
SELECT   NEWID() AS Id,  NoteId, FileId, AlbumId, BlogPostId, null AS MessageId, null AS DiscussionMessageId, CustomId1, CustomId2, CustomId3
FROM SnComment

if exists ( select * from INFORMATION_SCHEMA.COLUMNS 
where TABLE_NAME='SnComment' 
and COLUMN_NAME='RelationshipId' )
	ALTER TABLE dbo.SnComment ALTER COLUMN RelationshipId uniqueidentifier null
else	
	ALTER TABLE SnComment ADD RelationshipId uniqueidentifier null