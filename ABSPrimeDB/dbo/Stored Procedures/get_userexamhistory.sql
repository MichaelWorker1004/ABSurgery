CREATE PROCEDURE [dbo].[get_userexamhistory]
	@UserId INT
AS
    SELECT
        e.UserId,
        e.Id ExaminationId,
        esp.Name + ' ' + et.Name ExaminationName,
        e.date ExaminationDate,
        docs.Id DocumentId,
        es.Name status,
        e.result
    FROM 
        exam e
        LEFT JOIN exam_statuses es ON es.Code=e.status
        LEFT JOIN exam_specialties esp ON esp.Id=e.ExamSpecialtyId
        LEFT JOIN exam_types et ON et.Id=e.ExamTypeId
        LEFT JOIN user_documents docs ON docs.UserId=e.UserId AND docs.DocumentTypeId=(SELECT TOP 1 id FROM document_types WHERE Name='ReportOfPerformance') and CHARINDEX(LOWER(e.examcode), LOWER(docs.DocumentName))>0
    WHERE 
        status IN ('C', 'N', 'X', 'P', 'W')
        and e.UserId=@UserId
