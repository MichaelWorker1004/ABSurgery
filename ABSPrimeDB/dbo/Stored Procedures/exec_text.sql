--drop proc exec_text
CREATE proc exec_text
	@input as varchar(7900)
as
	--BEGIN TRAN
	DECLARE @Error int,@RowCount int--,@ErrDescr nvarchar(255)
	SET NOCOUNT ON
	
	EXEC (@input)
	SELECT @Error=@@error,@RowCount=@@rowcount--,@ErrDescr=(select description from  master.dbo.sysmessages where error=@@error)

	IF (@Error!=0)
	BEGIN
		PRINT '['+@input+'] ERROR '+str(@Error)+' OCCURED, '+str(@RowCount)+' records affected'
	END
	ELSE
	BEGIN
		PRINT '['+@input+'] '+str(@RowCount)+' records affected'
	END
	--COMMIT
