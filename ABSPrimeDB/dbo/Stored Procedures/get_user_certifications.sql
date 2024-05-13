CREATE PROCEDURE [dbo].[get_user_certifications]
    @UserId INT
AS
SELECT
    Speciality                = isnull(mcodes.descr,'Current Status: Not Certified'),
    CertificateId             = isnull(cert.certificate,''),
    InitialCertificationDate =
        CASE
            WHEN ISNULL(cert.certificationoccurrence, '') = 'FPD' THEN 'Initial Recognition: '  + FORMAT(cert.initial_cert_date, 'MMMM dd, yyyy')
            WHEN ISNULL(cert.certificationoccurrence, '') != 'FPD'
                and ISNULL(cert.initial_cert_date, '') != '' THEN 'Initial Certification: '  + FORMAT(cert.initial_cert_date, 'MMMM dd, yyyy')
            WHEN cert.certificationOccurrence='Initial'
                and ISNULL(cert.initial_cert_date, '') = ''
                and cert.admissible = 'Y'
                THEN 'In The Examination Process'
        END,
    EndDateDisplay            =
        CASE
          WHEN ISNULL(cert.Revoked, '')               = 'Y' THEN 'Certificate Revoked'
          WHEN ISNULL(cert.Suspended, '')             = 'Y' THEN 'Certificate Suspended'
          WHEN (ISNULL(cert.CertificationDuration, '') = 'Time-limited' OR (ISNULL(cert.CertificationDuration, '') = 'Continuous' AND ISNULL(cert.Expired, 0)<=0)) THEN 'Expiration: '   + FORMAT(cast(cert.CertificationExpireDate as datetime), 'MMMM dd, yyyy')
          WHEN ISNULL(cert.CertificationDuration, '') = 'Continuous' AND ISNULL(cert.reverification_date,'')='' THEN ''
          WHEN ISNULL(cert.CertificationDuration, '') = 'Continuous' AND ISNULL(cert.Expired, 0)>0 THEN 'Next Assessment Due: Fall '  + FORMAT(IIF(DATEADD(YEAR, -1, cast(cert.CertificationExpireDate as datetime))<getdate(), cast(cert.CertificationExpireDate as datetime), DATEADD(YEAR, -1, cast(cert.CertificationExpireDate as datetime))), 'yyyy')
          WHEN ISNULL(cert.CertificationDuration, '') = 'Time-limited' AND ISNULL(cert.Expired, 0)>0 THEN 'Expiration Date: '  + FORMAT(cast(cert.CertificationExpireDate as datetime), 'MMMM dd, yyyy')
          WHEN ISNULL(cert.CertificationDuration, '') = 'Lifetime' THEN ''
          ELSE ''
        END,
    IsClinicallyInactive     = IIF(ISNULL(uci.UserId, '')!='', 1, 0),
    Status                    =
        CASE
            WHEN ISNULL(Revoked, '')               = ''
                AND  ISNULL(Suspended, '')             = ''
                AND  ISNULL(Expired, 0)                > 0
            THEN 'Certified'

            WHEN (ISNULL(CertificationDuration, '') = 'Time-limited' OR (ISNULL(CertificationDuration, '') = 'Continuous' AND ISNULL(Expired, 0)<=0)) THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, CertificationExpireDate), 120)
            WHEN ISNULL(CertificationDuration, '') = 'Continuous' THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, reverification_date), 120)
            WHEN ISNULL(CertificationDuration, '') = 'Lifetime' THEN ''
            ELSE ''
    END
  FROM
    user_profiles u
    LEFT JOIN V_ABMS_CERT cert on cert.UserId = u.UserId
    LEFT JOIN exam_specialties es on es.Code = cert.exam
    LEFT JOIN user_clinically_inactive uci on uci.UserId = u.UserId and uci.ExamSpecialtyId=es.Id and ISNULL(uci.enddate, '9999')>=getdate()
    LEFT JOIN  mcodes ON mcodes.code = IIF(CertificationOccurrence='FPD',cert.type,exam) AND 
            grp = IIF(CertificationOccurrence='FPD','TY','EX')
  WHERE 
        u.UserId = @UserId
   order by 
        cert.IsCertified DESC