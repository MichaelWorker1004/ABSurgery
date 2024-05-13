CREATE PROCEDURE [dbo].[ins_user_case_comments]
    @UserId int,
    @CaseContentId int,
    @Comments varchar(5000),
    @CreatedByUserId int
AS
    INSERT INTO user_case_comments (UserId,CaseContentId, Comments, CreatedByUserId,LastUpdatedByUserId)
    VALUES (@UserId, @CaseContentId, @Comments, @CreatedByUserId,@CreatedByUserId)

    DECLARE @Id INT
    SET @Id = @@IDENTITY
    EXEC [dbo].[get_case_comments_byid] @Id

