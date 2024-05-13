CREATE PROCEDURE [dbo].[get_Joint_Commission]
AS
	SELECT
		 ID as OrganizationId,
		 OrganizationName,
		 state as StateCode
	FROM
		Joint_Commission
	WHERE
		AccreditationDecision = 1
	ORDER BY
        [OrganizationName]