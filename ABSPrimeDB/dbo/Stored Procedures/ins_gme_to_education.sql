CREATE PROCEDURE [dbo].[ins_gme_to_education]
    @UserId INT,
    @date_from DATETIME,
    @date_to DATETIME,
    @ClinicalLevelId INT,
    @ClinicalActivityId INT,
    @prg_name VARCHAR(500),
    @exam_type VARCHAR(10),
    @institution VARCHAR(500),
    @IsInternationalRotation BIT,
    @other VARCHAR(500),
    @refId INT
AS
    DECLARE @Candidate CHAR(10)
    SELECT @Candidate = ABSID FROM User_Profiles WHERE UserId = @UserId
    DECLARE @rot_type INT
    SELECT @rot_type = ClinicalLevel FROM clinical_level WHERE Id = @ClinicalLevelId
    DECLARE @subj_area VARCHAR(500)
    SELECT @subj_area = Name FROM clinical_activity WHERE Id = @ClinicalActivityId
        DECLARE @rot_intl VARCHAR(10)
    IF @IsInternationalRotation = 1
        SET @rot_intl = 'Yes'
    ELSE
        SET @rot_intl = 'No'
    INSERT INTO education
    (
        UserId,
        candidate,
        date_from,
        date_to,
        rot_type,
        subj_area,
        prg_name,
        exam_type,
        institution,
        rot_intl,
        other,
        refId
    )
    VALUES
    (
        @UserId,
        @candidate,
        @date_from,
        @date_to,
        @rot_type,
        @subj_area,
        @prg_name,
        @exam_type,
        @institution,
        @rot_intl,
        @other,
        @refId
    )
    EXEC dbo.get_gmerotations_byid @refId
