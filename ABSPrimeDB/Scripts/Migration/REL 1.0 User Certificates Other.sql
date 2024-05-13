INSERT INTO user_certificates_other(UserId, CertificateTypeId,IssueDate, CertificateNumber,  CreatedByUserId, LastUpdatedByUserId)
SELECT
    user_profiles.UserId,
    6,
    CertDate.st_val,
    CertNumber.st_val,
    @UserId,
    @UserId
FROM
    user_profiles
    INNER JOIN surgeon_st CertDate ON CertDate.UserId = user_profiles.UserId AND CertDate.status_code ='rpvicertdate'
    INNER JOIN surgeon_st CertNumber ON CertNumber.UserId = user_profiles.UserId AND CertNumber.status_code ='rpvicert'
    left join user_certificates_other on user_certificates_other.UserId=user_profiles.UserId
WHERE
    (CertDate.st_val != ''
  OR
    CertNumber.st_val != '')
    AND user_certificates_other.UserId IS NULL