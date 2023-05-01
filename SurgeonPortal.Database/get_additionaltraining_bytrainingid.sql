DROP PROCEDURE get_additionaltraining_bytrainingid;
GO
/*
 Mocked Get stored procedure for the YtgIM tool in the design phase
*/
CREATE PROCEDURE get_additionaltraining_bytrainingid
@TrainingId INT
AS
BEGIN 
    WITH Mockedget_additionaltraining_bytrainingid AS (
        SELECT TOP(1)
        TrainingId = ABS(CHECKSUM(NEWID())),
DateEnded = '04/19/2023',
DateStarted = '04/19/2023',
Other = 'Other9d19dfc2-776d-40fb-bfe7-21516c103953',
InstitutionId = ABS(CHECKSUM(NEWID())),
City = 'City5765b4d9-9d85-45bf-a38f-7dade3c080d6',
StateId = ABS(CHECKSUM(NEWID())),
TypeOfTraining = 'TypeOfTraining6db59776-444c-4dcc-a5f6-f7e54b04944c'
        FROM sys.objects AS o1
        CROSS JOIN sys.objects AS o2
        CROSS JOIN sys.objects AS o3
    )

    SELECT * FROM Mockedget_additionaltraining_bytrainingid
END
