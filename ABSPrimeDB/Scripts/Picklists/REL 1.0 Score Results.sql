INSERT INTO [dbo].[score_picklist] (Score, Result, CreatedByUserId, LastUpdatedByUserId)
VALUES 
    (1,'Pass',@UserId,@UserId),
    (2,'Equivocal',@UserId,@UserId),
    (3,'Fail',@UserId,@UserId)