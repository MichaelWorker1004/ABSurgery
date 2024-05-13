CREATE PROCEDURE [dbo].[get_userexamsactive_byuserid]
	@UserId INT
AS
    SELECT
        e.UserId,
        e.Id ExaminationId,
        esp.Name + ' ' + et.Name ExaminationName,
        eh.ExamStartDate GoodThroughStartDate, --goodThroughStartDate
        eh.ExamEndDate GoodThroughEndDate, --goodThroughEndDate
        es.Name status,
        t.app_receive ApplicationPacketReceivedDate --applicationPacketReceivedDate
    FROM 
        exam e 
        LEFT JOIN track t ON t.examcode=e.trackcode AND t.UserId=e.UserId
        LEFT JOIN Exam_Directory eh ON eh.ExamCode=e.examcode
        LEFT JOIN exam_statuses es ON es.Code=e.status
        LEFT JOIN exam_specialties esp ON esp.Id=e.ExamSpecialtyId
        LEFT JOIN exam_types et ON et.Id=e.ExamTypeId
    WHERE 
        e.status IN ('T', 'R', 'W') 
        AND GETUTCDATE() <= eh.ExamEndDate AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC'
        AND e.UserId=@UserId
    
    UNION

    SELECT TOP 1 
        @UserId UserId,
        NULL ExaminationId,
        esp.Name + ' ' + et.Name ExaminationName,
        eh.ExamStartDate GoodThroughStartDate, --goodThroughStartDate
        eh.ExamEndDate GoodThroughEndDate, --goodThroughEndDate
        'No Action Taken To Date' status,
        NULL ApplicationPacketReceivedDate --applicationPacketReceivedDate
    FROM 
        Exam_Directory eh
        INNER JOIN exam egw ON egw.UserId=@UserId AND egw.exam='G' AND egw.type='W' AND egw.result='P' --passwd a GW exam
        INNER JOIN exam_eligibility eego ON eego.UserId=@UserId AND eego.exam='G' and eego.type='O' and eego.admissible='Y' --admissible to GO
        LEFT JOIN exam ego ON ego.UserId=@UserId AND ego.exam='G' AND ego.type='O' AND ego.result IS NOT NULL and ego.year=dbo.udf_get_academic_year('E') --taken GO this year?
        LEFT JOIN exam_specialties esp ON esp.Code='G'
        LEFT JOIN exam_types et ON et.Code='C'
    WHERE 
        eh.ExamYear=dbo.udf_get_academic_year('E')
        AND eh.ExamSpecialtyId=esp.Id
        AND ego.UserId IS NULL
        AND GETUTCDATE() <= eh.ExamEndDate AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC'
        AND egw.UserId=@UserId 