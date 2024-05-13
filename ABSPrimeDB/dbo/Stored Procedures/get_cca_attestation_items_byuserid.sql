CREATE PROCEDURE [dbo].[get_cca_attestation_items_byuserid]
	@UserId INT
AS
    SELECT 
        'I hereby authorize any hospital or medical staff where I now have, have had, or have applied for medical staff privileges, and any medical organization of which I am a member or to which I have applied for membership, and any person who may have information (including medical records, patient records, and reports of committees) which is deemed by ABS to be material to its evaluation of this application, to provide such information to representatives of the ABS. I agree that communications of any nature made to the ABS regarding this application may be made in confidence and shall not be made available to me under any circumstances. I hereby release from liability any hospital, medical staff, medical organization or person, and ABS and its representatives, for acts performed in connection with this application.' label
        , 'sig_receive' name
        ,IIF(ISNULL(sig_receive, '')='', 0, 1) checked
    FROM track
    WHERE track.UserId=@UserId and examcode=dbo.udf_get_academic_year('M')+'MC'
    UNION
    -- notice for folks already enrolled in CC
    SELECT 
        'I understand that the certificate I will be issued upon successful completion of the biennial Continuous Certification Assessment will be contingent upon my on-going active participation in the Continuous Certification Program as a whole. I recognize that 10-year certificates are no longer offered by the ABS, and that the biennial Continuous Certification Assessment is replacing the traditional 10-year recertification examination.' label
        , 'certnotice_receive' name
        ,IIF(ISNULL(certnotice_receive, '')='', 0, 1) checked
    FROM track
    WHERE track.UserId=@UserId and examcode=dbo.udf_get_academic_year('M')+'MC'
        AND EXISTS (
            SELECT TOP 1 UserId FROM exam_pass where exam_pass.UserId=@UserId and durationtype='C' and isactive=1
        )   --active CC cert
    UNION
    -- notice for folks coming into CC from TL 1st time
    SELECT 
        'I understand that, as a time-limited certificate holder, I may be required to have all ongoing requirements completed by January 20, 2024 to be compliant with the certification program.  I have referred to the View Status section of the ABS Continuous Certification page to determine whether reporting is required this year.  I acknowledge that Diplomates with time-limited certificates are required to report at least every five years and that Diplomates who are due to report and have outstanding requirements as of January 20, 2024 will subsequently be reported as Not Meeting Requirements on the ABS website.  I also understand that I may enter the Continuous Certification Program at any time by registering for an assessment.  In doing so, I understand that my time-limited certificate will no longer be in effect and I will be participating in the Continuous Certification Program effective the date I register for the assessment.  I recognize that 10-year certificates are no longer offered by the ABS, and that the biennial Continuous Certification Assessment is replacing the traditional 10-year recertification examination.' label
        , 'certnotice_receive' name
        ,IIF(ISNULL(certnotice_receive, '')='', 0, 1) checked
    FROM track
    WHERE track.UserId=@UserId and examcode=dbo.udf_get_academic_year('M')+'MC'
        AND EXISTS (
            SELECT top 1 UserId from exam_pass where exam_pass.UserId=@UserId and durationtype='T' and isactive=1
        )   --active TL cert
        AND NOT EXISTS (
            SELECT TOP 1 UserId FROM exam_pass where exam_pass.UserId=@UserId and durationtype='C' and isactive=1
        )   --no active CC cert, which would put them in CCA automatically
    -- UNION
    -- TODO: notice for lapsed folks
    -- SELECT 
    --     'I understand that my certificate will be issued upon successful completion of the first assessment in a five-year period.  I understand I will be required to take and pass the assessment each year FOR FIVE CONSECUTIVE YEARS; if I do not pass this assessment in the five consecutive years as required, I understand that I may re-enter the Continuous Certification Program by successfully completing a secure readmission examination at a testing center, and that no exceptions will be made to this policy. After the five-year period is complete, I understand that I will then be required to participate in the biennial Continuous Certification Assessment to maintain my certification.   My certification will be contingent upon my on-going active participation in the Continuous Certification Program as a whole.  I recognize that 10-year certificates are no longer offered by the ABS, and that the biennial Continuous Certification Assessment is replacing the traditional 10-year recertification examination.' label
    --     , 'certnotice_receive' name
    --     ,IIF(ISNULL(certnotice_receive, '')='', 0, 1) checked
    -- FROM track
    -- WHERE track.UserId=@UserId and examcode=dbo.udf_get_academic_year('M')+'MC' and ep.UserId IS NOT NULL
        -- add clauses here when lapsed table is created

    