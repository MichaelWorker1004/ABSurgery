CREATE PROCEDURE [dbo].[get_cc_refLetters]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT
    AppRefLet.id,
    AppRefLet.UserId,
    Hosp,
    Official,
    Title,
    refLet_let_type_picklist.Role RoleName,
    refLet_altRole_picklist.Role AltRoleName,
    refLet_let_type_picklist.Id RoleId,
    refLet_altRole_picklist.Id AltRoleId,
    Explain,
    Email,
    Phone,
    Let_sent as LetterSent,
    IIF(let_approve IS NULL AND let_rcvd IS NULL,1,IIF(let_approve IS NULL AND let_rcvd IS NOT NULL,2,3 )) Status

FROM
    dbo.AppRefLet
LEFT JOIN refLet_let_type_picklist
    ON AppRefLet.let_type = refLet_let_type_picklist.id
LEFT JOIN refLet_altRole_picklist
    ON AppRefLet.alternaterole = refLet_altRole_picklist.id and AppRefLet.alternaterole!=''
WHERE
    userId = @UserId AND exam_type = ''
END