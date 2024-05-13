CREATE FUNCTION [dbo].[udf_get_acgme_status](@UserId INT)
RETURNS INT
AS
BEGIN
    --  ACGME type certificate in user_documents
    DECLARE @status INT
    SELECT @status=ISNULL(
        (SELECT
            TOP 1 1
        FROM user_certificates uc
        INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
        WHERE UserId=@UserId 
            AND ct.Name='ACGME'), 0)

    RETURN @status
END