CREATE PROCEDURE [dbo].[get_surgeon_certificate_ByKey]
     @UserId INT
AS
BEGIN

  SELECT
    NPI,
    user_profiles.FirstName+IIF(user_profiles.MiddleName IS NULL,' ',' '+user_profiles.MiddleName+' ')+user_profiles.LastName as FullName,
    StartDateDisplay          =
        CASE
            WHEN ISNULL(certificationoccurrence, '') = 'FPD' THEN 'Initial Recognition Date: '  + FORMAT(initial_cert_date, 'MMMM dd, yyyy')
            WHEN ISNULL(certificationoccurrence, '') != 'FPD'
                and ISNULL(initial_cert_date, '') != '' THEN 'Initial Certification Date: '  + FORMAT(initial_cert_date, 'MMMM dd, yyyy')
            WHEN certificationOccurrence='Initial'
                and ISNULL(initial_cert_date, '') = ''
                and admissible = 'Y'
                THEN 'In The Examination Process'
        END,
    EndDateDisplay            =
        CASE
          WHEN ISNULL(Revoked, '')               = 'Y' THEN 'Certificate Revoked'
          WHEN ISNULL(Suspended, '')             = 'Y' THEN 'Certificate Suspended'
          WHEN (ISNULL(CertificationDuration, '') = 'Time-limited' OR (ISNULL(CertificationDuration, '') = 'Continuous' AND ISNULL(Expired, 0)<=0)) THEN 'Expiration Date: '   + FORMAT(cast(CertificationExpireDate as datetime), 'MMMM dd, yyyy')
          WHEN ISNULL(CertificationDuration, '') = 'Continuous' AND ISNULL(reverification_date,'')='' THEN ''
          WHEN ISNULL(CertificationDuration, '') = 'Continuous' THEN 'Reverification Date: '  + FORMAT(cast(reverification_date as datetime), 'MMMM dd, yyyy')
          WHEN ISNULL(CertificationDuration, '') = 'Lifetime' THEN ''
          ELSE ''
        END,
    CertificationOccurrence   = isnull(certificationoccurrence,''),
    IsAdmissible              = isnull(admissible,''),
    user_profiles.UserId,
    CertificateId             = isnull(certificate,'') ,
    Duration                  = '',
    SpecialityCode            = isnull(exam,''),
    Speciality                = isnull(descr,'Current Status: Not Certified'),
    IsRevoked                 = revoked,
    StartDate                 = CONVERT(VARCHAR(10), CONVERT(DATETIME, initial_cert_date), 120),
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
          END,
    --type       = '',
    ClinicallyInactive        = isnull(clinically_inactive,''),
    Probation                 = IIF(isnull(probation, '') = 'Y'and isnull(dscpl_action_date, '') > '2019-06-01', 'Y', ''),
    IsRetired                 = ISNULL(retired, '') ,
    RetiredValue              = ISNULL(retired_value, ''),
    CertificationMaintenance,
    CertificationMaintenanceRequired,
    ExamSort                  =
            CASE
              WHEN ISNULL(certificationoccurrence, '')='FPD' THEN 'ZZ'
              WHEN exam='G' THEN 'A'
              ELSE descr
            END,
    IsCertified               = isnull(iscertified,'')
  FROM
    user_profiles
    LEFT JOIN V_ABMS_CERT cert on cert.UserId = user_profiles.UserId
    LEFT JOIN  mcodes ON code = IIF(CertificationOccurrence='FPD',cert.type,exam) AND
            grp = IIF(CertificationOccurrence='FPD','TY','EX')
  WHERE
            user_profiles.UserId = @UserId
   order by IsCertified DESC
END