CREATE PROCEDURE [dbo].[get_user_qe_all_status_byuserid]
    @UserId INT,
    @ExamheaderId INT
AS

    DECLARE @ExamRegOpen BIT
    SELECT @ExamRegOpen=IIF(UserId IS NULL, 0, 1) FROM exam WHERE exam.UserId=@UserId AND exam.examHeaderId=@ExamheaderId

    SELECT
        'Personal_Profile' AS 'StatusType'
        ,dbo.udf_get_personal_profile_status(@UserId, DEFAULT) as Status
        ,0 disabled
    UNION
    SELECT
        'Training' Id 
        ,dbo.udf_get_training_status(@UserId, ExamSpecialtyId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId
    UNION
    SELECT
        'Professional_Standing' Id
        ,dbo.udf_get_prof_standing_status(@UserId, ExamSpecialtyId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId
    UNION
    SELECT
        'Medical_Licenses' Id
        ,dbo.udf_get_medical_licenses_status(@UserId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId
    UNION
    SELECT
        'ACGME' Id 
        ,dbo.udf_get_acgme_status(@UserId) Status
        ,0 disabled
    UNION
    SELECT
        'GME' Id 
        ,dbo.udf_get_gme_status(@UserId, ExamSpecialtyId) Status
        ,IIF(dbo.udf_get_pd_attestation_status(@UserId, ExamSpecialtyId) IN (1, 2), 1, 0) disabled --if PD attestation complete or in prog, then disable from editing
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId
    UNION
    SELECT
        'PD_Attestation' Id 
        ,dbo.udf_get_pd_attestation_status(@UserId, ExamSpecialtyId) Status
        ,IIF(dbo.udf_get_pd_attestation_status(@UserId, ExamSpecialtyId)=1 AND @ExamRegOpen=1, 1, 0) disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId
    UNION
    SELECT
        'QE_Certificates' Id 
        ,dbo.udf_get_qe_certificates_status(@UserId, ExamSpecialtyId) Status
        ,0 disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId
    UNION
    SELECT
        'Application_Fee' Id  
        ,dbo.udf_get_qe_application_fee_status(@UserId, @ExamHeaderId) Status
        ,0 disabled
    UNION
    SELECT
        'Special_Accommodations' Id  
        ,dbo.udf_get_special_accommodations_status(@UserId, @ExamHeaderId) Status
        ,0 disabled
    UNION
    SELECT
        'Attestation' Id  
        ,dbo.udf_get_qe_attestation_status(@UserId, ExamSpecialtyId) Status
        ,@ExamRegOpen disabled
    FROM Exam_Directory
    WHERE Id=@ExamHeaderId