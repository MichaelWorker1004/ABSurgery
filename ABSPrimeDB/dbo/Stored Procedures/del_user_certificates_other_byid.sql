CREATE PROCEDURE [dbo].[del_user_certificates_other_byid]
    @Id int
AS
    DELETE FROM
        [dbo].[user_certificates_other]
    WHERE
        Id = @Id
RETURN 0