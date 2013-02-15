UPDATE 
	CalendarEvent 
SET 
	OwnerCalendarId =
(SELECT TOP 1 c.Id FROM CalendarEventEntry ce INNER JOIN Calendar c on ce.CalendarId = c.Id WHERE ce.CalendarEventId = CalendarEvent.Id ORDER BY CASE WHEN CalendarEvent.AuthorId = c.OwnerId THEN 1 ELSE 0 END)

UPDATE 
	CalendarEvent 
SET 
	OwnerCalendarId = (SELECT TOP 1 c.Id FROM Calendar c)
WHERE OwnerCalendarId IS NULL

