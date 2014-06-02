/****** Object:  StoredProcedure [dbo].[FriendSuggestion]    Script Date: 10/15/2012 16:19:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Mono-Software>
-- Create date: <02.06.2012>
-- Description:	<Getting users connections determined by the parameter level od connections.>
-- =============================================
Create PROCEDURE [dbo].[FriendSuggestion]
	-- Add the parameters for the stored procedure here
	@UserId uniqueidentifier,
	@NumberOfLevels int,
	@PageNumber int,
	@PageSize int,
	@IsRandom bit = 0,
	@UserName nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @UserTable TABLE (FriendUserId uniqueidentifier, ConnectionLevel int)
	INSERT INTO @UserTable
	SELECT
		SnFriend.FriendUserId,
		1
	FROM
		SnFriend
	WHERE
		SnFriend.UserId = @UserId
	
	UNION ALL
	
	SELECT
		DISTINCT SnFriend.UserId,
		1
	FROM
		SnFriend
	WHERE
		SnFriend.FriendUserId = @UserId AND SnFriend.FriendUserId NOT IN (SELECT FriendUserId FROM @UserTable)


	DECLARE @Counter int
	SET @Counter = 2

	WHILE @Counter <= @NumberOfLevels
	BEGIN
	INSERT INTO @UserTable
	SELECT
		t.FriendUserId,
		t.ConnectionLevel
	FROM
	(
		SELECT
			SnFriend.UserId AS FriendUserId,
			@Counter as ConnectionLevel
		FROM
			SnFriend
			INNER JOIN @UserTable u ON SnFriend.FriendUserId = u.FriendUserId 
			AND 
			u.ConnectionLevel = @Counter - 1
	      
		UNION ALL
		
		SELECT
			SnFriend.FriendUserId AS FriendUserId,
			@Counter as ConnectionLevel
		FROM
			SnFriend
			INNER JOIN @UserTable u ON SnFriend.UserId = u.FriendUserId 
			AND 
			u.ConnectionLevel = @Counter - 1
	)t
	WHERE
		t.FriendUserId NOT IN (SELECT FriendUserId FROM @UserTable)
		
	SET @Counter = @Counter + 1

	END
	
	DECLARE @RecordCount int
	SET @RecordCount = 
		(
			SELECT 
				COUNT(DISTINCT u.FriendUserId) 
			FROM 
				@UserTable u 
				INNER JOIN aspnet_Users ON aspnet_Users.UserId = u.FriendUserId
				LEFT JOIN UserProfile ON UserProfile.Id = u.FriendUserId
			WHERE 
				u.ConnectionLevel > 1 
				AND 
				u.FriendUserId != @UserId 
				AND 
				u.FriendUserId 
				NOT IN (SELECT FriendUserId FROM SnFriendRequest WHERE UserId = @UserId)
				AND
				(
					@UserName = ''
					OR
					(
						aspnet_Users.UserName LIKE @UserName + '%' 
						OR
						UserProfile.FirstName LIKE @UserName + '%' 
						OR
						UserProfile.LastName LIKE @UserName + '%' 
					)
				)
		)
	
	SELECT
		t.UserId,
		t.UserName,
		t.FirstName,
		t.LastName,
		t.ConnectionLevel,
		@RecordCount AS RecordCount
	FROM
	(
		SELECT 
			DISTINCT ut.FriendUserId as UserId, 
			aspnet_Users.UserName, 
			UserProfile.FirstName, 
			UserProfile.LastName, 
			MIN(ut.ConnectionLevel) as ConnectionLevel,
			(CASE WHEN @IsRandom = 1 THEN DENSE_RANK() OVER (ORDER BY ut.ConnectionLevel, NEWID()) ELSE DENSE_RANK() OVER (ORDER BY ut.ConnectionLevel, ut.FriendUserId) END) AS RowNumber
		FROM @UserTable ut
		INNER JOIN aspnet_Users ON aspnet_Users.UserId = ut.FriendUserId
		LEFT JOIN UserProfile ON UserProfile.Id = ut.FriendUserId
		WHERE 
			ut.FriendUserId NOT IN (SELECT u.FriendUserId FROM @UserTable u WHERE u.ConnectionLevel =  1) 
			AND
			ut.FriendUserId != @UserId
			AND
			ut.FriendUserId NOT IN (SELECT FriendUserId FROM SnFriendRequest WHERE UserId = @UserId)
			AND
			(
				@UserName = ''
				OR
				(
					aspnet_Users.UserName LIKE @UserName + '%' 
					OR
					UserProfile.FirstName LIKE @UserName + '%' 
					OR
					UserProfile.LastName LIKE @UserName + '%' 
				)
			)
		GROUP BY
			ut.FriendUserId,
			aspnet_Users.UserName,
			UserProfile.FirstName,
			UserProfile.LastName,
			ut.ConnectionLevel
	)t
	WHERE
	t.RowNumber > @PageNumber * @PageSize
    AND
    RowNumber <= ((@PageNumber * @PageSize) + @PageSize)
	ORDER BY
	t.RowNumber
	
END

GO


