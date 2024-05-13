CREATE FUNCTION [dbo].[udf_get_qe_certificates_status](@UserId INT, @ExamSpecialtyId INT=0)
RETURNS INT
AS
BEGIN
    --  GQE: ECFMG, ACLS, ATLS, FLS, FES
    --  VQE: ECFMG, ACLS, ATLS, RPVI
    --  PQ:  PALS
    --  CC:  ECFMG
    DECLARE @status INT, @SpecialtyCode VARCHAR
    SET @status=1
    SELECT @SpecialtyCode=ISNULL((SELECT Code FROM exam_specialties es WHERE es.Id=@ExamSpecialtyId), '')

    BEGIN
        IF @SpecialtyCode='G'
        BEGIN
            IF ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='ECFMG'), 0)=0
                    OR
                ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='ACLS'), 0)=0
                    OR
                ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='ATLS'), 0)=0
                    OR
                ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='FLS'), 0)=0
                    OR
                ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='FES'), 0)=0
            BEGIN
                SET @status=0
            END
        END
        ELSE IF @SpecialtyCode='V'
        BEGIN
            IF ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='ECFMG'), 0)=0
                    OR
                ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='ACLS'), 0)=0
                    OR
                ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='ATLS'), 0)=0
                    OR
                ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='RPVI'), 0)=0
            BEGIN
                SET @status=0
            END
        END
        ELSE IF @SpecialtyCode='P'
        BEGIN
            IF ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='PALS'), 0)=0
            BEGIN
                SET @status=0
            END
        END
        ELSE IF @SpecialtyCode='C'
        BEGIN
            IF ISNULL(
                (SELECT
                    TOP 1 1
                FROM user_certificates uc
                INNER JOIN certificate_types ct ON ct.Id=uc.CertificateTypeId
                WHERE UserId=@UserId 
                AND ct.Name='ECFMG'), 0)=0
            BEGIN
                SET @status=0
            END
        END
    END
    RETURN @status
END