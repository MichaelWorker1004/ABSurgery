CREATE PROCEDURE [dbo].[insert_user_accommodations]
    @UserId INT,
    @AccommodationID INT,
    @DocumentId INT = NULL,
    @ExamID INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[User_Accommodations]
        ([UserId], [AccommodationID],[DocumentId], [examID], [CreatedByUserId], [LastUpdatedByUserId])
    VALUES
        (@UserId, @AccommodationID, @DocumentId, @ExamID, @UserId, @UserId);
EXEC get_user_accommodations_byId @UserId,@ExamID
END
go