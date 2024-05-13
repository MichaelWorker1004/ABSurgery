CREATE PROCEDURE [dbo].[get_user_clinically_active_byuserid]
	@UserId INT
AS
DECLARE @ClinicallyActive AS BIT
SET @ClinicallyActive=0
IF @UserId IN 
    (SELECT DISTINCT ep.UserId 
        FROM exam_pass ep 
        LEFT JOIN user_clinically_inactive uci ON uci.UserId=ep.UserId and ep.ExamSpecialtyId=uci.ExamSpecialtyId 
        WHERE uci.UserId IS NULL and ep.expiration>=YEAR(getdate()))
BEGIN
   SET @ClinicallyActive = 1
END

SELECT UserId=@UserId, ClinicallyActive=@ClinicallyActive