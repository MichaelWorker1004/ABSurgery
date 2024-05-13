CREATE PROCEDURE [dbo].[update_user_accommodations]
    @UserId INT,
    @Id INT,
    @AccommodationID INT,
    @DocumentId INT = NULL,
    @ExamID INT,
    @LastUpdatedByUserId INT
AS
	SET NOCOUNT ON

	Update [dbo].[User_Accommodations] SET
		AccommodationID = @AccommodationID,
		DocumentId = @DocumentId,
		examID = @examID,
		LastUpdatedByUserId = @LastUpdatedByUserId,
		LastUpdatedAtUtc = GetUtcDate()
	WHERE
		Id = @Id
EXEC get_user_accommodations_byId @UserId,@ExamID
