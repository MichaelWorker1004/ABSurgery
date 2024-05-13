CREATE FUNCTION [dbo].[udf_get_CME_status](@UserId INT)
RETURNS INT
AS
BEGIN
    DECLARE @status BIT
    SELECT @status =  1
    RETURN @status
END