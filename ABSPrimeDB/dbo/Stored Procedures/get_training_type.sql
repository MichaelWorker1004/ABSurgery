CREATE PROCEDURE [dbo].[get_training_type]
AS
	SELECT 
		Id,
		TrainingType
	FROM
		trainingtype
RETURN 0
