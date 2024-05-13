CREATE FUNCTION [dbo].[udf_get_gme_status](@UserId INT, @ExamSpecialtyId INT=0)
RETURNS INT
AS
BEGIN
    --  For **G/H/O/C** (different for V & P):
    --      report at least 48 weeks of rotations in the "chief" category (rot_type 4,5,6,7)
    DECLARE @status INT
    DECLARE @PDAttStatus DATETIME
    DECLARE @ExamSpecialtyCode VARCHAR(10)
    SELECT @ExamSpecialtyCode = Code FROM dbo.exam_specialties WHERE Id = @ExamSpecialtyId
    SELECT @PDAttStatus =  sig_receive from dbo.track where UserId=@UserId and CAST(track.year as VARCHAR )+track.exam+track.type = dbo.udf_get_academic_year('M')+@ExamSpecialtyCode+'Q'
    SELECT @status= ISNULL((SELECT 
        IIF(Sum(ClinicalWeeks)>48,IIF(@PDAttStatus IS NOT NULL,1,2),0)
        FROM
        (
            SELECT
                case when ca.IsCredit = 1 then DATEDIFF(WEEK, gme.StartDate, gme.EndDate) + 1 else 0 end as ClinicalWeeks
            FROM
                gme_rotations gme
                inner join clinical_level cl on cl.Id = gme.ClinicalLevelId
                inner join clinical_activity ca on ca.Id = gme.ClinicalActivityId
            WHERE
                UserId = @UserId AND
                gme.ClinicalLevelId IN (4,5,6,7)
        ) AS ClinicalWeeks), 0)
    RETURN @status
END