CREATE PROCEDURE [dbo].[ins_user_exam]
    @UserId int,
    @ExamHeaderId int
AS
    INSERT INTO exam
    (
        UserId
        ,candidate
        ,ExamHeaderId
        ,exam
        ,ExamSpecialtyId
        ,type
        ,ExamFormatId
        ,ExamTypeId
        ,year
        ,area
        ,status
        ,ExamStatusId
    )
    SELECT 
        @UserId
        ,ABSId
        ,@ExamHeaderId
        ,esp.Code exam
        ,esp.Id ExamSpecialtyId
        ,ef.Code type
        ,ef.Id ExamFormatId
        ,et.Id ExamTypeId
        ,ed.ExamYear year
        ,ISNULL(ed.ExamArea, '') area
        ,est.Code status
        ,est.Id ExamStatusId
    FROM Exam_Directory ed
    LEFT JOIN exam_formats ef ON ef.Id=ed.ExamFormatId
    LEFT JOIN exam_types et ON et.Id=ed.ExamTypeId
    LEFT JOIN exam_specialties esp ON esp.Id=ed.ExamSpecialtyId
    LEFT JOIN exam_statuses est ON est.Code='T'
    LEFT JOIN user_profiles up ON up.UserId=@UserId
    WHERE ed.Id=@ExamHeaderId
    
    UPDATE
        track 
    SET
        app_receive=GETUTCDATE()
    FROM track t
    INNER JOIN exam e ON e.UserId=t.UserId AND e.examHeaderId=@ExamHeaderId
    WHERE
        t.UserId=@UserId AND t.examcode=e.trackcode

    RETURN 0
