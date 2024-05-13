CREATE PROCEDURE [dbo].[get_forgotten_username]
    @Email [varchar](100)
AS
    SELECT
        [UserName],
        [user_profiles].[FirstName],
        [user_profiles].[LastName]
    FROM
        [Users]
    LEFT JOIN [user_profiles] ON [Users].[UserId] = [user_profiles].[UserId]
    WHERE
        [EmailAddress] = @Email
