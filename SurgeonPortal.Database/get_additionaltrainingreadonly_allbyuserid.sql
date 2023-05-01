DROP PROCEDURE get_additionaltrainingreadonly_allbyuserid;
GO
/*
 Mocked Get All stored procedure for the YtgIM tool in the design phase
*/
CREATE PROCEDURE get_additionaltrainingreadonly_allbyuserid
@UserId INT
AS
BEGIN 
    WITH Mockedget_additionaltrainingreadonly_allbyuserid AS (
        SELECT TOP(5)
        TrainingId = ABS(CHECKSUM(NEWID())),
TypeOfTraining = 'TypeOfTraining3fe78b7c-f415-4110-97ed-d1e4c15bd869',
State = 'St',
City = 'Cityd490ba9b-a75b-4ddd-b7aa-ed4c2416211f',
InstitutionName = 'InstitutionNameea6f7772-9868-47ba-b0c8-2a398c47b5e',
Other = 'Other3e15c1e7-5f76-4216-9e2c-980e3165cec3',
DateStarted = '04/19/2023',
DateEnded = '04/19/2023'
        FROM sys.objects AS o1
        CROSS JOIN sys.objects AS o2
        CROSS JOIN sys.objects AS o3
    )

    SELECT * FROM Mockedget_additionaltrainingreadonly_allbyuserid
END
