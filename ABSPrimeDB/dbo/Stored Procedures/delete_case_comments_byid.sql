CREATE PROCEDURE [dbo].[delete_case_comments_byid]
	@Id int
AS
	DELETE
	FROM 
		user_case_comments
    WHERE
		Id = @Id