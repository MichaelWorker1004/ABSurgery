CREATE PROCEDURE [dbo].[update_cca_attestation_byuserid]
	@UserId INT,
    @sigReceive DATETIME,
    @certnoticeReceive DATETIME
AS
    UPDATE
        track
    SET
        sig_receive=@sigReceive
        ,certnotice_receive=@certnoticeReceive
        ,modifier='dbo'
        ,modified=GETUTCDATE()
        ,modifications=modifications+1
    WHERE
        track.UserId=@UserId and examcode=dbo.udf_get_academic_year('M')+'MC'