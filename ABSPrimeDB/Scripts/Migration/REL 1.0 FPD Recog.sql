INSERT INTO FPD_Recog (UserId,validityDate,RecognitionIssuanceID,RecognitionIssuanceDate,RecognitionIssuanceScheduledUpdate,RecognitionIssuanceStatus,CreatedByUserId,LastUpdatedByUserId)
SELECT
    CertificateIssuance.UserId,
    CertificateIssuance.modified,
    CertificateIssuance.id,
    CertificateIssuance.CertificationIssueDate,
    CertificateIssuance.reverification_date,
    CASE
        WHEN CertificateIssuance.Expired<0 THEN 'Expired'
        WHEN CertificateIssuance.Revoked='Y' THEN 'Revoked'
        WHEN CertificateIssuance.Suspended='Y' THEN 'Suspended'
        ELSE 'Active'
    END,
   @UserId,
   @UserId
FROM
    V_ABMS_CERTALL CertificateIssuance
WHERE
    CertificationOccurrence='FPD'