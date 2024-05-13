/*
DECLARE @ApplicationId INT,
		@ApplicationClaimId INT,
		@UserId INT;

DECLARE @salt VARCHAR(36) = CONVERT(VARCHAR(36), NEWID())
insert into users(FirstName, LastName, EmailAddress, Title, Password, CreatedByUserId, LastUpdatedByUserId)
values ('Test', 'User', 'test@something.com', '', HASHBYTES('SHA2_256', 'pass@word123'), 0, 0)

SET @UserId = SCOPE_IDENTITY()
GO

insert into applications(ApplicationName, CreatedByUserId, LastUpdatedByUserId)
values('Surgeon Portal', @UserId, @UserId)

SET @ApplicationId = SCOPE_IDENTITY()
GO

insert into application_claims(ApplicationId, ClaimName, CreatedByUserId, LastUpdatedByUserId)
values(@ApplicationId, 'User', @UserId, @UserId)

SET @ApplicationClaimId = SCOPE_IDENTITY()
GO

insert into user_claims(ApplicationClaimId, UserId, CreatedByUserId, LastUpdatedByUserId)
values(@ApplicationClaimId, @UserId, @UserId, @UserId)

GO
*/
