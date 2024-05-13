CREATE FUNCTION [dbo].[udf_get_med_training_status](@UserId INT)
RETURNS INT
AS
    BEGIN
        DECLARE @status BIT
        SELECT @status =  1
        RETURN @status
    END