/*
 Mocked UPDATE stored procedure for the YtgIM tool in the design phase
*/
CREATE PROCEDURE update_outcomeregistry_getbyuserid
    @SurgeonSpecificRegistry bit,
    @RegistryComments varchar(50),
    @RegisteredWithACHQC bit,
    @RegisteredWithCESQIP bit,
    @RegisteredWithMBSAQIP bit,
    @RegisteredWithABA bit,
    @RegisteredWithASBS bit,
    @RegisteredWithStatewideCollaboratives bit,
    @RegisteredWithABMS bit,
    @RegisteredWithNCDB bit,
    @RegisteredWithRQRS bit,
    @RegisteredWithNSQIP bit,
    @RegisteredWithNTDB bit,
    @RegisteredWithSTS bit,
    @RegisteredWithTQIP bit,
    @RegisteredWithUNOS bit,
    @RegisteredWithNCDR bit,
    @RegisteredWithSVS bit,
    @RegisteredWithELSO bit,
    @UserConfirmed bit,
    @UserConfirmedDateUtc datetime,
    @UserId INT
AS
BEGIN 
    
    --The update statement goes here...

    EXEC get_outcomeregistry_getbyuserid @UserId
END
