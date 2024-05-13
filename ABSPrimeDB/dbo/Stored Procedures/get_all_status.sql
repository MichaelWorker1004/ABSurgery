CREATE PROCEDURE [dbo].[get_all_status]
    @UserId INT
AS
    SELECT
        'Prof_Standing' AS 'StatusType',
        dbo.udf_get_prof_standing_status(@UserId, DEFAULT) as Status
        UNION
        SELECT
        'Med_Training' AS 'StatusType',
        dbo.udf_get_med_training_status(@UserId)  as Status
        UNION
        SELECT
        'Outcomes' AS 'StatusType',
        dbo.udf_get_outcomes_status(@UserId) as Status
        UNION
        SELECT
        'CME' AS 'StatusType',
        dbo.udf_get_CME_status(@UserId)  as Status
        UNION
        SELECT
        'Pers_Info' AS 'StatusType',
        dbo.udf_get_pers_info_status(@UserId) as Status
        UNION
        SELECT
        'Ref_Let' AS 'StatusType',
        dbo.udf_get_ref_let_status(@UserId) as Status
        UNION
        SELECT
        'CC_Attestation' AS 'StatusType',
        dbo.udf_get_CC_attestation_status(@UserId) as Status
        UNION
        SELECT
        'CC_Fee' AS 'StatusType',
        dbo.udf_get_cc_application_fee_status(@UserId) as Status