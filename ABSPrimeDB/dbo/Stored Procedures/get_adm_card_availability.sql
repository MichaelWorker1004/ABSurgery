CREATE PROCEDURE [get_adm_card_availability]
    @UserID int,
    @ExamID int
AS
    SELECT
        CASE 
            WHEN exam_formats.Code='W' AND exam.pearson_sent IS NOT NULL AND exam.pearson_auth_id IS NOT NULL THEN 1
            WHEN exam_formats.Code='O' AND exam.adms_sent IS NOT NULL THEN 1
            ELSE 0 
        END AdmCardAvailable,
        exam.adms_sent AS DatePosted,
        Exam_Directory.ExamCode AS ExamCode,
        CASE 
            WHEN exam_formats.Code='W' AND exam.pearson_sent IS NOT NULL AND exam.pearson_auth_id IS NOT NULL THEN 'adm_auth'
            WHEN exam_formats.Code='O' AND exam.adms_sent IS NOT NULL THEN 'ce_adm_card'
            ELSE NULL
        END AdmCardReport
    FROM
        exam
    LEFT JOIN
        Exam_Directory ON exam.examHeaderId = Exam_Directory.Id
    LEFT JOIN
        exam_formats ON Exam_Directory.ExamFormatId=exam_formats.Id
    WHERE
        exam.UserId=@UserId AND exam.examHeaderId=@ExamID