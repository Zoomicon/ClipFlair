INSERT INTO SnRelationship (Id, BlogPostId) 
select NEWID(), Temp.BlogPostId from 
(Select distinct BlogPostId from BlogPostTag) as Temp;

INSERT INTO SnTag 
(Id, RelationshipId, Tag, Slug, SortOrder)
Select Id, (select Id from SnRelationship where BlogPostTag.BlogPostId = SnRelationship.BlogPostId), Tag, Slug, 0 from BlogPostTag  