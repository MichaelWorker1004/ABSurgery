CREATE PROCEDURE [dbo].[update_user_professional_standing_byuserid]
    @UserId int,
    @PrimaryPracticeID int =NULL,
    @OrganizationTypeId int=NULL,
    @ExplanationOfNonPrivileges VARCHAR(MAX)=NULL,
    @ExplanationOfNonClinicalActivities VARCHAR(MAX) =NULL,
    @LastUpdatedByUserId int
AS
	SET NOCOUNT ON
	Update [dbo].[user_professional_standing] SET
		PrimaryPracticeId = @PrimaryPracticeID,
		OrganizationTypeId = @OrganizationTypeId,
		ExplanationOfNonPrivileges = @ExplanationOfNonPrivileges,
		ExplanationOfNonClinicalActivities = @ExplanationOfNonClinicalActivities,
		LastUpdatedAtUtc = GetUtcDate(),
		LastUpdatedByUserId = @LastUpdatedByUserId
	WHERE
		UserId = @UserId

EXEC [dbo].[get_user_professional_standing_byuserid] @UserId