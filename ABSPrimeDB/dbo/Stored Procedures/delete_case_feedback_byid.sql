CREATE PROCEDURE [dbo].[delete_case_feedback_byid]
	@Id int
AS
	DELETE
	FROM
		user_case_feedback
    WHERE
		Id = @Id