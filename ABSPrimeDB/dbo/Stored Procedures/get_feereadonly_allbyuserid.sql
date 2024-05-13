/*
 Mocked Get All stored procedure for the YtgIM tool in the design phase
*/
CREATE PROCEDURE get_feereadonly_allbyuserid
@UserId INT
AS
    WITH Mockedget_feereadonly_allbyuserid AS (
        SELECT TOP(5)
        TotalAmountDue = ABS(CHECKSUM(NEWID())),
TotalAmountPaid = ABS(CHECKSUM(NEWID())),
AsOfDate = '04/28/2023',
CurrentBalance = ABS(CHECKSUM(NEWID()))
        FROM sys.objects AS o1
        CROSS JOIN sys.objects AS o2
        CROSS JOIN sys.objects AS o3
    )

    SELECT * FROM Mockedget_feereadonly_allbyuserid
