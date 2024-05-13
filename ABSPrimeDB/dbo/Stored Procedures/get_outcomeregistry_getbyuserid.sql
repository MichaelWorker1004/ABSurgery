/*
 Mocked Get stored procedure for the YtgIM tool in the design phase
*/
CREATE PROCEDURE get_outcomeregistry_getbyuserid
@UserId INT
AS
    WITH Mockedget_outcomeregistry_getbyuserid AS (
        SELECT TOP(1)
        SurgeonSpecificRegistry = 0,
RegistryComments = 'RegistryComments990c425b-1b0b-40d7-922b-9d88a55ec6',
RegisteredWithACHQC = 1,
RegisteredWithCESQIP = 1,
RegisteredWithMBSAQIP = 1,
RegisteredWithABA = 0,
RegisteredWithASBS = 1,
RegisteredWithStatewideCollaboratives = 0,
RegisteredWithABMS = 0,
RegisteredWithNCDB = 1,
RegisteredWithRQRS = 0,
RegisteredWithNSQIP = 0,
RegisteredWithNTDB = 0,
RegisteredWithSTS = 1,
RegisteredWithTQIP = 0,
RegisteredWithUNOS = 1,
RegisteredWithNCDR = 1,
RegisteredWithSVS = 0,
RegisteredWithELSO = 0,
UserConfirmed = 0,
UserConfirmedDateUtc = '04/19/2023',
UserId = ABS(CHECKSUM(NEWID()))
        FROM sys.objects AS o1
        CROSS JOIN sys.objects AS o2
        CROSS JOIN sys.objects AS o3
    )

    SELECT * FROM Mockedget_outcomeregistry_getbyuserid
