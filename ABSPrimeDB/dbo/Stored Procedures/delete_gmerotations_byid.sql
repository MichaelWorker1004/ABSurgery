CREATE PROCEDURE [dbo].[delete_gmerotations_byid]
	@Id INT
AS
    EXEC dbo.delete_gme_from_education @Id
	DELETE 
	FROM
		gme_rotations
	WHERE 
		id = @Id
RETURN 0
