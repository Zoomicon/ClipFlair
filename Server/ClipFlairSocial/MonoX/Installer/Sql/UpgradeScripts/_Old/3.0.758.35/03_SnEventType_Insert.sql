INSERT INTO SnEventType 
	  SELECT newid(), 'signed up'
UNION SELECT newid(), 'updated profile'
UNION SELECT newid(), 'became friends with'
UNION SELECT newid(), 'joined the group'
UNION SELECT newid(), 'posted a blog post'
UNION SELECT newid(), 'published an album'
UNION SELECT newid(), 'uploaded a file'