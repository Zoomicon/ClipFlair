DECLARE @PrivacyLevelFriends uniqueidentifier
SET @PrivacyLevelFriends = (select top 1 Id from PrivacyLevel where PrivacyLevel = 'Friends')


Update UserProfile
SET MyStatusPrivacyLevelId = @PrivacyLevelFriends
where MyStatusPrivacyLevelId is null 