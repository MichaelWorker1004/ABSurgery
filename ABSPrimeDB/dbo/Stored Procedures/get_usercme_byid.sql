CREATE PROCEDURE [dbo].[get_usercme_byid]
	@CmeId INT
AS
	SELECT
        Id AS CmeId,
        UserId, 
        ReleaseDate AS Date,
        ActivityName AS Description,
        Hours As CreditsTotal,
        credits_sa AS CreditsSA,
        credit_exp_date AS CreditExpDate,
        IIF(ModuleID IS NULL, 0, 1) AS CMEDirect
	FROM
		dbo.cme 
	WHERE
		cme.Id = @CmeId
