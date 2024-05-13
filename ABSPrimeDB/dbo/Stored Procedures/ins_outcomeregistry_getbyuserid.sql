/*
 Mocked INSERT stored procedure for the YtgIM tool in the design phase
*/
CREATE PROCEDURE ins_outcomeregistry_getbyuserid
    @UserId INT,
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
    @UserConfirmedDateUtc datetime
AS
    
    --The insert statement goes here...
    
    EXEC get_outcomeregistry_getbyuserid @UserId
