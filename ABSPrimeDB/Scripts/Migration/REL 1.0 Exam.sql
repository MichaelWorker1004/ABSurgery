UPDATE 
    exam
SET
    ExamSpecialtyId = CASE 
            WHEN type='B' THEN esb.Id
            ELSE es.Id
        END
FROM exam
LEFT JOIN exam_specialties es ON es.Code=exam.exam AND exam.type!='B'
LEFT JOIN exam_specialties esb ON esb.Name='Metabolic Bariatric Surgery' AND exam.type='B';

UPDATE 
    exam
SET
    ExamTypeId = CASE
            WHEN SUBSTRING(trackcode, 6, 1)='R' AND exam.year>2020 and ISNULL(RTRIM(area), '')='' and exam.exam!='Z' THEN etcca.Id
            WHEN SUBSTRING(trackcode, 6, 1)='R' AND (exam.year<=2019 OR exam.exam='Z' OR exam.exam='H' or ISNULL(RTRIM(area), '')!='') THEN etr.Id
            ELSE et.Id
        END
FROM exam
LEFT JOIN exam_types et ON et.Code=SUBSTRING(trackcode, 6, 1) and SUBSTRING(trackcode, 6, 1)!='R'
LEFT JOIN exam_types etcca ON etcca.Code=SUBSTRING(trackcode, 6, 1) and etcca.Name='Continuous Certification Readmission Examination' and SUBSTRING(trackcode, 6, 1)='R' and ISNULL(RTRIM(area), '')='' and exam.year>2020 and exam.exam!='Z'
LEFT JOIN exam_types etr ON etr.Code=SUBSTRING(trackcode, 6, 1) and etr.Name='Recertification Examination' and SUBSTRING(trackcode, 6, 1)='R' and (exam.year<=2019 or exam.exam='Z' or exam.exam='H' or ISNULL(RTRIM(area), '')!='');

UPDATE 
    exam
SET
    ExamStatusId = es.Id
FROM exam
LEFT JOIN exam_statuses es ON es.Code=exam.status
WHERE ExamStatusId IS NULL