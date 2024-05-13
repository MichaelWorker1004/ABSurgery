CREATE  PROCEDURE [dbo].[get_user_accommodations_byId]
	@UserId int,
	@ExamId int
AS
	SELECT
	 User_Accommodations.Id,
	 User_Accommodations.UserId,
	 User_Accommodations.AccommodationID,
	 AccommodationsId.Code AS AccommodationName,
	 User_Accommodations.DocumentId,
	 user_documents.DocumentName DocumentName,
	 User_Accommodations.ExamID,
	 User_Accommodations.CreatedByUserId,
	User_Accommodations.CreatedAtUtc,
	User_Accommodations.LastUpdatedAtUtc,
	User_Accommodations.LastUpdatedByUserId
	FROM
		User_Accommodations
	LEFT JOIN
	    AccommodationsId on AccommodationsId.AccommodationId = User_Accommodations.AccommodationID
	LEFT JOIN
	    user_documents on user_documents.Id = User_Accommodations.DocumentId
	WHERE
		User_Accommodations.UserId = @UserId AND
		User_Accommodations.examID = @ExamId

RETURN 0
go

