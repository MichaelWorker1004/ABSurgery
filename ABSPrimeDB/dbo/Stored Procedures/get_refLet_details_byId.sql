CREATE PROCEDURE [dbo].[get_refLet_details_byId]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT
    AppRefLet.UserId,
    Official,
    refLet_let_type_picklist.Role RoleName,
    refLet_altRole_picklist.Role AltRoleName,
    refLet_let_type_picklist.Id RoleId,
    refLet_altRole_picklist.Id AltRoleId,
    Explain,
    Title,
    Email,
    Phone,
    Hosp,
    City,
    State,
    UP.FirstName+' '+UP.LastName + ' ' + UP.Suffix AS FullName
FROM
    dbo.AppRefLet
LEFT JOIN refLet_let_type_picklist
    ON AppRefLet.let_type = refLet_let_type_picklist.id
LEFT JOIN refLet_altRole_picklist
    ON AppRefLet.alternaterole = refLet_altRole_picklist.id and AppRefLet.alternaterole!=''
LEFT JOIN user_profiles UP
    ON AppRefLet.UserId = UP.UserId
WHERE AppRefLet.id = @Id
END