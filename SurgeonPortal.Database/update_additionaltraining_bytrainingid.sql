DROP PROCEDURE update_additionaltraining_bytrainingid;
GO
/*
 Mocked UPDATE stored procedure for the YtgIM tool in the design phase
*/
CREATE PROCEDURE update_additionaltraining_bytrainingid

    @TrainingId INT,
    @DateEnded datetime,
    @DateStarted datetime,
    @Other varchar(50),
    @InstitutionId INT,
    @City varchar(50),
    @StateId INT,
    @TypeOfTraining varchar(50)
AS
BEGIN 
    
    //The update statement goes here...

    EXEC get_additionaltraining_bytrainingid @TrainingId
END
