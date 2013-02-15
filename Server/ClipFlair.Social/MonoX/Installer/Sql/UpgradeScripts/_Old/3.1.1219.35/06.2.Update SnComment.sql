UPDATE SnComment
SET 
	RelationshipId
	=
	(SELECT 
			TOP 1 Id
		FROM
			SnRelationship
		WHERE
			(SnComment.NoteId IS NULL OR SnRelationship.NoteId  = SnComment.NoteId) 
			AND
			(SnComment.FileId IS NULL OR  SnRelationship.FileId = SnComment.FileId)
			AND
			(SnComment.AlbumId IS NULL OR  SnRelationship.AlbumId = SnComment.AlbumId)
			AND
			(SnComment.BlogPostId IS NULL OR  SnRelationship.BlogPostId = SnComment.BlogPostId)
			AND
			(SnComment.CustomId1 IS NULL OR  SnRelationship.CustomId1 = SnComment.CustomId1)
			AND
			(SnComment.CustomId2 IS NULL OR  SnRelationship.CustomId2 = SnComment.CustomId2)
			AND
			(SnComment.CustomId3 IS NULL OR  SnRelationship.CustomId3 = SnComment.CustomId3)
		)

if exists ( select * from INFORMATION_SCHEMA.COLUMNS 
where TABLE_NAME='SnComment' 
and COLUMN_NAME='RelationshipId' )
	ALTER TABLE dbo.SnComment ALTER COLUMN RelationshipId uniqueidentifier not null
else	
	ALTER TABLE SnComment ADD RelationshipId uniqueidentifier not null