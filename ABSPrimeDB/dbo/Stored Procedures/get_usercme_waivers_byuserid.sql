CREATE PROCEDURE [dbo].[get_usercme_waivers_byuserid]
	@UserId INT
AS
	SELECT
        Id AS CmeId,
        UserId, 
        ReleaseDate AS Date,
        ActivityName AS Description,
        Hours As CreditsTotal,
        credits_sa AS CreditsSA,
        credit_exp_date AS CreditExpDate,
        ReportingOrganization AS IssuedBy
	FROM
		dbo.cme 
	WHERE
        ReleaseDate <= SYSDATETIMEOFFSET() AT TIME ZONE 'Eastern Standard Time'
        AND ReleaseDate >= DATEFROMPARTS(YEAR(SYSDATETIMEOFFSET() AT TIME ZONE 'Eastern Standard Time')-5, '01', '01')
        AND ReportingOrganization='ABS'
		AND cme.UserId = @UserId
