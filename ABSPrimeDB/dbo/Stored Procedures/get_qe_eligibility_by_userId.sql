CREATE PROCEDURE [dbo].[get_qe_eligibility_by_userId]
    @UserId int
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ExamId,ExamCode,ExamName,AppOpenDate,AppCloseDate,AppLateDate,AppDelayDate,ExamStartDate,ExamEndDate,ApplicationApproved,ExamRegistrationAvailable,RegistrationOpen,AdmissionCardReport,UserApplicationExists
    FROM (
        SELECT
            CASE
                WHEN
                    track.UserId IS NOT NULL OR
                    surgeon_st.UserId IS NOT NULL OR
                    current_exam.UserId IS NOT NULL OR
                    (V_ABMS_CERT.UserId IS NOT NULL AND FPD_CERT.UserId IS NULL) OR-- MBS available to all certified surgeons
                    (exam_specialties.Code='N' AND SCC_CERT.UserId IS NOT NULL)-- NCC available to all SCC certified surgeons
                    THEN
                        1
                WHEN
                    exam_eligibility.admissible IN ('I','Y') AND
                    exam.UserId IS NULL AND
                    exam_pass.UserId IS NULL AND
                    candidate_program.UserId IS NOT NULL AND
                    dscpl_action.UserId IS NULL
                    THEN
                        1
            END AS Eligible,
            Exam_Directory.Id AS ExamId,
            Exam_Directory.ExamCode AS ExamCode,
            CAST(Exam_Directory.ExamYear AS VARCHAR) +' ' + Exam_Directory.ExamName AS ExamName,
            Exam_Directory.RegOpenDate AS AppOpenDate,
            Exam_Directory.RegEndDate AS AppCloseDate,
            Exam_Directory.RegLateDate AS AppLateDate,
            Exam_Directory.RegDelayDate AS AppDelayDate,
            IIF(current_exam.day IS NULL, exam_directory.ExamStartDate, DATEADD(day, current_exam.day-1, Exam_Directory.ExamStartDate)) AS ExamStartDate,
            Exam_Directory.ExamEndDate AS ExamEndDate,
            IIF(track.app_approve IS NOT NULL, 1, 0) AS ApplicationApproved,
            IIF(current_exam.UserId IS NOT NULL, 1, 0) ExamRegistrationAvailable,
            IIF(Exam_Directory.RegDelayDate IS NULL OR GETDATE()>Exam_Directory.RegDelayDate,1,0) AS RegistrationOpen,
            CASE
                WHEN current_exam.pearson_sent IS NOT NULL AND exam_formats.code='W' AND current_exam.pearson_auth_id IS NOT NULL THEN 'adm_auth'
                WHEN current_exam.adms_sent IS NOT NULL AND exam_formats.code='O' THEN 'ce_adm_card'
                ELSE NULL
            END AdmissionCardReport,
            Exam_Directory.ExamTypeId,
            IIF(track.UserId IS NULL,0,1) AS UserApplicationExists
        FROM User_Profiles
        LEFT JOIN Exam_Directory ON
            GETDATE() BETWEEN Exam_Directory.RegOpenDate AND Exam_Directory.ExamEndDate
        LEFT JOIN exam_types ON
            exam_types.Id = Exam_Directory.ExamTypeId
        LEFT JOIN exam_specialties ON
            exam_specialties.Id = Exam_Directory.ExamSpecialtyId
        LEFT JOIN exam_formats ON
            exam_formats.Id=Exam_Directory.ExamFormatId
        LEFT JOIN track ON
            track.UserId = User_Profiles.UserId AND
            track.exam+track.type=exam_specialties.Code+exam_types.Code AND
            track.year=Exam_Directory.ExamYear
        LEFT JOIN exam_eligibility ON
            exam_eligibility.UserId = User_Profiles.UserId AND
            exam_eligibility.exam+exam_eligibility.type='GW'
        LEFT JOIN exam ON
            User_Profiles.UserId = exam.UserId AND
            exam.ExamSpecialtyId+exam.ExamTypeId=exam_specialties.Id+exam_types.Id AND
            exam.result='P'
        LEFT JOIN exam_pass ON
            exam_pass.UserId=User_Profiles.UserId AND
            exam_pass.ExamSpecialtyId=exam_specialties.Id AND
            exam_pass.type='W'
        LEFT JOIN candidate_program ON
            candidate_program.UserId=User_Profiles.UserId AND
            candidate_program.exam=exam_specialties.Code AND
            cast(candidate_program.compyear as INT) > (Exam_Directory.ExamYear-4)
        LEFT JOIN dscpl_action ON
            dscpl_action.UserId = User_Profiles.UserId AND
            dscpl_action.effective=1 AND
            dscpl_action.dscpl_code in ('CR','CS')
        LEFT JOIN surgeon_st ON
            surgeon_st.UserId=User_Profiles.UserId AND
            surgeon_st.status_code='AO' AND
            CHARINDEX(exam_specialties.Code+exam_types.Code, surgeon_st.st_val)>0 AND
            start_date IS NOT NULL AND
            end_date IS NOT NULL AND
            GETDATE() BETWEEN start_date AND end_date
        LEFT JOIN exam current_exam ON                          --check if exam record exists with Tentative or Registered status (therefore exam registration available)
            current_exam.UserId = User_Profiles.UserId AND
            current_exam.ExamHeaderId=Exam_Directory.Id AND
            current_exam.status IN ('T', 'R')
        LEFT JOIN V_ABMS_CERT as V_ABMS_CERT ON
            V_ABMS_CERT.UserId = User_Profiles.UserId and Exam_Directory.ExamCode like '%GB%' and V_ABMS_CERT.exam = 'G' and V_ABMS_CERT.iscertified='Y' and V_ABMS_CERT.CertificationMaintenance='Maintained' AND V_ABMS_CERT.CertificationOccurrence!='FPD'
        LEFT JOIN V_ABMS_CERT FPD_CERT ON
            FPD_CERT.UserId = User_Profiles.UserId and Exam_Directory.ExamCode like '%GB%' and FPD_CERT.exam = 'G' and FPD_CERT.iscertified='Y' and FPD_CERT.CertificationMaintenance='Maintained' and FPD_CERT.CertificationOccurrence='FPD'
        LEFT JOIN V_ABMS_CERT SCC_CERT ON
            SCC_CERT.UserId = User_Profiles.UserId and Exam_Directory.ExamCode like '%CN%' and SCC_CERT.exam = 'C' and SCC_CERT.iscertified='Y' and SCC_CERT.CertificationMaintenance='Maintained'
        WHERE
            User_Profiles.UserId = @UserId ) EligibleExams
    WHERE EligibleExams.Eligible=1
END