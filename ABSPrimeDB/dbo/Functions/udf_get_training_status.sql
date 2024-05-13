CREATE FUNCTION [dbo].[udf_get_training_status](@UserId INT, @ExamSpecialtyId INT=0)
RETURNS INT
AS
BEGIN
    -- incomplete if:
    --      Required fields not populated:
    --          (med school)
    --          school type
    --          school
    --          school city
    --          school country
    --          degree
    --          year

    --          (residency program)
    --          trained in more than one program y/n    (TODO)
    --          additional approval documentation y/n   (TODO)
    --          candidate program (on file)
    --              Other program (if above not exist)
    --          year of completion
    DECLARE @Status INT 
    SELECT @Status=ISNULL(
        (SELECT 
            CASE
                WHEN ISNULL(mt.GraduateProfileId, '')=''                                    THEN 0
                WHEN ISNULL(mt.MedicalSchoolName, '')=''                                    THEN 0
                WHEN ISNULL(mt.MedicalSchoolCity, '')=''                                    THEN 0
                WHEN ISNULL(mt.MedicalSchoolCountryId, '')=''                               THEN 0
                WHEN ISNULL(mt.DegreeId, '')=''                                             THEN 0
                WHEN ISNULL(mt.MedicalSchoolCompletionYear, '')=''                          THEN 0
                WHEN ISNULL(mt.ResidencyProgramName, ISNULL(ResidencyProgramOther, ''))=''  THEN 0
                WHEN ISNULL(mt.ResidencyCompletionYear, '')=''                              THEN 0
                ELSE 1
            END
        FROM medical_training mt
        WHERE UserId=@UserId), 0)
        
    RETURN @Status
END