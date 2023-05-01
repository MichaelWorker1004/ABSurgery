DROP PROCEDURE ins_additionaltraining_bytrainingid;
GO
/*
 Mocked INSERT stored procedure for the YtgIM tool in the design phase
*/
CREATE PROCEDURE ins_additionaltraining_bytrainingid

    @DateEnded datetime,
    @DateStarted datetime,
    @Other varchar(50),
    @InstitutionId INT,
    @City varchar(50),
    @StateId INT,
    @TypeOfTraining varchar(50)
AS
BEGIN 
    
    //The insert statement goes here...

    EXEC get_additionaltraining_bytrainingid @TrainingId
END
