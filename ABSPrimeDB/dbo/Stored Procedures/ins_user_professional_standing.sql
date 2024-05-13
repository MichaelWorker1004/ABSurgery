CREATE PROCEDURE [dbo].[ins_user_professional_standing]
    @UserId int,
    @PrimaryPracticeID int =NULL,
    @OrganizationTypeId int=NULL,
    @ExplanationOfNonPrivileges VARCHAR(MAX)=NULL,
    @ExplanationOfNonClinicalActivities VARCHAR(MAX) =NULL,
    @CreatedByUserId int ,
    @LastUpdatedByUserId int
AS
    INSERT INTO user_professional_standing
    (
        UserId,
        PrimaryPracticeID,
        OrganizationTypeId,
        ExplanationOfNonPrivileges,
        ExplanationOfNonClinicalActivities,
        CreatedByUserId,
        LastUpdatedByUserId)
    VALUES
    (
        @UserId,
        @PrimaryPracticeID,
        @OrganizationTypeId,
        @ExplanationOfNonPrivileges,
        @ExplanationOfNonClinicalActivities,
        @CreatedByUserId,
        @CreatedByUserId
    )
    EXEC [get_user_professional_standing_byuserid] @UserId

