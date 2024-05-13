CREATE FUNCTION [dbo].[udf_get_special_accommodations_status](@UserId INT, @ExamHeaderId INT=0)
RETURNS INT
AS
BEGIN
    --  Just needs to see if the value is populated 
    DECLARE @status INT
    SELECT @status =  IIF(EXISTS(SELECT Id FROM User_Accommodations WHERE UserId=@UserId AND ExamId=@ExamHeaderId), 1, 0)
    RETURN @status
END
